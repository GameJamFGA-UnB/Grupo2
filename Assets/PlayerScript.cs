using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer renderer;
    private int speed = 10;

    private int cakeSpeed = 20;
    private bool isJumping = false;
    private bool doubleJump = false;

    private GameObject cake;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        cake = GameObject.Find("Cake");
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
        spawnCake();
    }

    void FixedUpdate() {
        dieOutOfBounds();
    }

    void spawnCake() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3 cakePos = new Vector3(0.3f, 0, 0);
            Vector2 cakeVel = new Vector2(cakeSpeed, 0);
            if (renderer.flipX) {
                cakePos *= -1;
                cakeVel *= -1;
            }

            GameObject ck = Instantiate(cake, transform.position + cakePos, cake.transform.rotation);
            ck.GetComponent<Rigidbody2D>().velocity = cakeVel;
        }
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

        if (transform.position.x > 18.4f) {
            transform.position = new Vector3(-18.3f, transform.position.y, 0);
        } else if (transform.position.x < -18.4f) {
            transform.position = new Vector3(18.3f, transform.position.y, 0);
        } else {
            transform.position +=  movement * Time.deltaTime * speed;
        }
    }

    void jump(){
        isJumping = !rb2D.IsTouchingLayers();

        if (isJumping && !animator.GetBool("jumping")) {
            animator.SetBool("jumping", true);
        } else if (!isJumping && animator.GetBool("jumping")){
            animator.SetBool("jumping", false);
        }

        if (!isJumping && !doubleJump) {
            doubleJump = true;
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