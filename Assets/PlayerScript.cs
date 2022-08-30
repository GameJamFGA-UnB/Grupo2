using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private int speed = 15;
    private bool isJumping = false;
    private bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
    }

    void move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position +=  movement * Time.deltaTime * speed;
    }

    void jump(){
        isJumping = !rb2D.IsTouchingLayers();

        if(Input.GetKeyDown(KeyCode.W)){
            if(!isJumping){
                rb2D.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                doubleJump = true;
            }else{
                if(doubleJump){
                    rb2D.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }


}