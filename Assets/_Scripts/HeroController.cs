using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class HeroController : MonoBehaviour
{
    [Header ("Player Objects")]
    public Animator anim;
    public HeroAnimState heroAnimState;
    public SpriteRenderer spriteRenderer;
    [Header ("Movement Values")]
    public float speed;
    public float jumpForce;
    [Header("Maximum Velocity")]
    public Vector2 maxVel = new Vector2();
    public GameController gameController;
    private Rigidbody2D rgb;

    //public Transform target;

    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        heroAnimState = HeroAnimState.IDLE;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.red);

        Move();
    }

    public void Move()
    {
        grounded = Physics2D.BoxCast(transform.position, new Vector2(0.2f, 0.1f), 0.0f, Vector2.down, 0.3f, 1 << LayerMask.NameToLayer("Ground"));

        if ((Input.GetAxis("Horizontal") > 0) && (grounded))
        {
            spriteRenderer.flipX = false;
            heroAnimState = HeroAnimState.WALK;
            anim.SetInteger("AnimState", (int)HeroAnimState.WALK);
            rgb.AddForce(Vector2.right * speed);
            print(rgb.velocity.ToString());
        }
        if ((Input.GetAxis("Horizontal") < 0) && (grounded))
        {
            spriteRenderer.flipX = true;
            heroAnimState = HeroAnimState.WALK;
            anim.SetInteger("AnimState", (int)HeroAnimState.WALK);
            rgb.AddForce(Vector2.left * speed);
        }

        if (Input.GetAxis("Horizontal") == 0 && (grounded))
        {
            heroAnimState = HeroAnimState.IDLE;
            anim.SetInteger("AnimState", (int)HeroAnimState.IDLE);
        }
        if ((Input.GetAxis("Jump") > 0) && (grounded))
        {
            anim.SetInteger("AnimState", (int)HeroAnimState.JUMP);
            heroAnimState = HeroAnimState.JUMP;
            rgb.AddForce(Vector2.up * jumpForce);
            print(rgb.velocity.ToString());
            grounded = false;
        }
        rgb.velocity = new Vector2(Mathf.Clamp(rgb.velocity.x, -maxVel.x, maxVel.x), Mathf.Clamp(rgb.velocity.y, -maxVel.y, maxVel.y));
    }

    public void Reset()
    {
        this.transform.position = new Vector2(0f, 1f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Cherry":
                Destroy(collision.gameObject);
                gameController.Score += 1;
                break;
            case "DeathPoint":
                gameController.GotHit();
                gameController.Lives--;
                Reset();
                break;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                gameController.GotHit();
                gameController.Lives--;
                Reset();
                break;
        }
    }
}
