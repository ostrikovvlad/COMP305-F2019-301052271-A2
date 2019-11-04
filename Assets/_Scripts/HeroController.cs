/* Filename: HeroController.cs
 * Author: Vladislav Ostrikov
 * Last modified by: Vladislav Ostrikov
 * Last modified: Nov 3, 2019
 * This script is used for player controller. It manages movements, picking items and getting hurt.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour
{
    [Header ("Player Objects")]
    public Animator anim; // Is used to store Animator component of the hero 
    public HeroAnimState heroAnimState; // Is used to manage hero animation state
    public SpriteRenderer spriteRenderer; // Is used to store SpriteRendere component of the hero
    [Header ("Movement Values")]
    public float speed; // Is used to store speed of the hero movement
    public float jumpForce; // Is used to store jump force of the hero
    [Header("Maximum Velocity")]
    public Vector2 maxVel = new Vector2(); // Is used to set maximum velocity of the hero(is used to manage jumping and moving of the hero)
    public GameController gameController; // Is used to store GameController object
    private Rigidbody2D rgb; // Is used to store Rigidbody2D component of the hero

    public bool grounded; // Is used to represent boolean(true or false) value to check if the hero is on the ground
    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>(); // Is used to get Rigidbody2D component
        heroAnimState = HeroAnimState.IDLE; // Sets animation of the hero to Idle
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.red);
        // Move is used to move player
        Move();
    }
    /// <summary>
    /// This method is responsible for moving character and checikng if it is on the ground
    /// </summary>
    public void Move()
    {
        grounded = Physics2D.BoxCast(transform.position, new Vector2(0.2f, 0.1f), 0.0f, Vector2.down, 0.3f, 1 << LayerMask.NameToLayer("Ground"));

        if ((Input.GetAxis("Horizontal") > 0) && (grounded))
        {
            spriteRenderer.flipX = false;
            heroAnimState = HeroAnimState.WALK;
            anim.SetInteger("AnimState", (int)HeroAnimState.WALK);
            rgb.AddForce(Vector2.right * speed);
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
            grounded = false;
        }
        rgb.velocity = new Vector2(Mathf.Clamp(rgb.velocity.x, -maxVel.x, maxVel.x), Mathf.Clamp(rgb.velocity.y, -maxVel.y, maxVel.y));
    }
    /// <summary>
    /// This method is used to reset player's position
    /// </summary>
    public void Reset()
    {
        this.transform.position = new Vector2(0f, 1f);
    }
    /// <summary>
    /// This method is used to do some stuff when the player trigger objects.
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Cherry":
                collision.GetComponent<Animator>().SetInteger("IsPicked", 1);
                collision.GetComponent<CircleCollider2D>().enabled = false;
                Destroy(collision.gameObject, 0.5f);
                gameController.Score += 1;
                break;
            case "Gem":
                collision.GetComponent<Animator>().SetInteger("IsPicked", 1);
                SceneManager.LoadScene("End");
                break;
            case "DeathPoint":
                gameController.GotHit();
                gameController.Lives--;
                Reset();
                break;
        }
    }
    /// <summary>
    /// This method is used to do some stuff when the user collides with objects
    /// </summary>
    /// <param name="collision"></param>
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
