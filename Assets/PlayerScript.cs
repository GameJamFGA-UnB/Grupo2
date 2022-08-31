using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer renderer;
    private int speed = 10;
    private bool isJumping = false;
    private bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
    }

    void FixedUpdate() {
        dieOutOfBounds();
    }

    void move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if (movement == Vector3.zero && animator.GetBool("walking")) {
            animator.SetBool("walking", false);
        } else if (movement != Vector3.zero && !animator.GetBool("walking")) {
            animator.SetBool("walking", true);
        }

        if (movement.x > 0) {
            renderer.flipX = false;
        } else if (movement.x < 0) {
            renderer.flipX = true;
        }
        transform.position +=  movement * Time.deltaTime * speed;
    }

    void jump(){
        isJumping = !rb2D.IsTouchingLayers();

        if (isJumping && !animator.GetBool("jumping")) {
            animator.SetBool("jumping", true);
        } else if (!isJumping && animator.GetBool("jumping")){
            animator.SetBool("jumping", false);
        }

        if(Input.GetKeyDown(KeyCode.W)){
            if(!isJumping){
                rb2D.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                doubleJump = true;
            }else{
                if(doubleJump){
                    rb2D.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                    doubleJump = false;
                } 
            }
        }
    }

    void dieOutOfBounds() {
        if (transform.position.y < -9.5) {
            transform.position = new Vector3(-4.455f, -2.421f, 0);
        }
    }


}