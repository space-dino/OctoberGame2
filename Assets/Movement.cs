using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float airSpeed;
    public float bubbleSpeed;

    public float airFric;
    public float bubbleFric;
    public float boost;

    public float gravity;

    private Vector2 movement;
    public LayerMask ground;
    public bool inBubble;

    public bool platformerBubble;
    public float platGravity;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!platformerBubble)
        {
            movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        if (Physics2D.OverlapCircle(transform.position, 0.3f, ground)) // in bubble
        {
            movement *= bubbleSpeed;
            rb.drag = bubbleFric;
            inBubble = true;

            if (platformerBubble)
            {
                movement = new Vector2(Input.GetAxis("Horizontal"), 0);
                rb.gravityScale = platGravity;

                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                rb.gravityScale = 0;
            }
        }
        else // in air
        {
            movement *= airSpeed;
            rb.drag = airFric;
            inBubble = false;
            rb.gravityScale = gravity;

            if (platformerBubble)
            {
                movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(movement);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("exit");

       // if (collision.gameObject.layer == ground)
        {
            rb.velocity *= boost;
        }
    }
}
