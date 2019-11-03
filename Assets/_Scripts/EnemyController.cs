using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    public Rigidbody2D rgb;
    public bool isGrounded;
    public bool hasGroundAhead;
    public bool hasWallAhead;
    public Transform lookAhead;
    public bool isFacingRight = false;
    public float speed;
    public bool isOpossum = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        isGrounded = Physics2D.BoxCast(transform.position, new Vector2(0.2f, 0.1f), 0.0f, Vector2.down, 0.3f, 1 << LayerMask.NameToLayer("Ground"));

        hasGroundAhead = Physics2D.Linecast(transform.position, lookAhead.position, 1 << LayerMask.NameToLayer("Ground"));
        hasWallAhead = Physics2D.Linecast(transform.position, lookAhead.position, 1 << LayerMask.NameToLayer("Wall"));
        if (isGrounded)
        {
            if (isFacingRight)
            {
                rgb.velocity = new Vector2(speed, 0.0f);
            }
            if (!isFacingRight)
            {
                rgb.velocity = new Vector2(-speed, 0.0f);
            }
            if (isOpossum)
            {
                if(hasGroundAhead || hasWallAhead)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
                    isFacingRight = !isFacingRight;
                }
            }
            if (!isOpossum)
            {
            if(!hasGroundAhead || hasWallAhead)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
                isFacingRight = !isFacingRight;
            }
            }

        }
    }
}
