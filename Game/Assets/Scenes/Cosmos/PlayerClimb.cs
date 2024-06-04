using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public float climbSpeed = 3f;
    public bool isOnLadder = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isOnLadder)
        {
            float climb = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, climb * climbSpeed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isOnLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isOnLadder = false;
        }
    }
}



