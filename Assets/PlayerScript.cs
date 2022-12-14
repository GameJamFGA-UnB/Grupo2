using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer renderer;
    private AudioSource audioSource;
    private Collider2D groundCollider;

    private GameObject heart;
    private GameObject heart1;
    private GameObject heart2;

    private GameObject cake;

    public List<AudioClip> winSounds;
    public List<AudioClip> deathSounds;
    public List<AudioClip> fireSounds;

    private bool isJumping = false;
    private bool doubleJump = false;
    public int speed = 10;
    public int jumpForce = 15;
    public int cakeSpeed = 20;
    public Vector2 spawnPosition;
    public bool activeSounds = true;
    public int lifes = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        groundCollider = GameObject.Find("Ground").GetComponent<Collider2D>();
        cake = GameObject.Find("Cake");
        heart = GameObject.Find("Heart");
        heart1 = GameObject.Find("Heart1");
        heart2 = GameObject.Find("Heart2");
        transform.position = spawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
        SpawnCake();
    }

    void FixedUpdate() {
        CheckOutOfBounds();
    }

    void SpawnCake() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3 cakePos = new Vector3(0.3f, 0, 0);
            Vector2 cakeVel = new Vector2(cakeSpeed, 0);
            if (renderer.flipX) {
                cakePos *= -1;
                cakeVel *= -1;
            }

            GameObject ck = Instantiate(cake, transform.position + cakePos, cake.transform.rotation);
            ck.GetComponent<Rigidbody2D>().velocity = cakeVel;

            PlayRandomSound(fireSounds);
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

        if (transform.position.x > 34f) {
            transform.position = new Vector3(-34f, transform.position.y, 0);
        } else if (transform.position.x < -34f) {
            transform.position = new Vector3(34f, transform.position.y, 0);
        } else {
            transform.position +=  movement * Time.deltaTime * speed;
        }
    }

    void jump(){
        isJumping = !rb2D.IsTouchingLayers();
        isJumping = !rb2D.IsTouching(groundCollider);

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
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = true;
            }else{
                if(doubleJump){
                    rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                    rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void CheckOutOfBounds() {
        if (transform.position.y < -15) {
            KillPlayer();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            KillPlayer();
        }
    }

    void KillPlayer() {
        lifes--;
        print("lifes: " + lifes + "\n");
        if(lifes>=0){
            switch(lifes){
                case 2:
                    Destroy(heart2);
                    transform.position = new Vector3(5f, -0.1f, 0); 
                break;
                case 1:
                    Destroy(heart1);
                    transform.position = new Vector3(-22f, -7.63f, 0); 
                break;
                case 0:
                    Destroy(heart);
                    transform.position = new Vector3(17.74f, -4.63f, 0); 
                break;        
            }           

                
            PlayRandomSound(deathSounds);
        }
        else{           
            transform.position = new Vector3(-3.12f, 22.105f, 0);  
            GameController.instance.ShowGameOver();
            PlayRandomSound(deathSounds);
            //print("MORREU!\n");
        }
            
    }

    void PlayRandomSound(List<AudioClip> list) {
        if (list.Count > 0 && activeSounds) {
            audioSource.Stop();
            AudioClip selected = list[Random.Range(0, list.Count)];
            audioSource.clip = selected;
            audioSource.Play();
        }
    }

}