using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 5;
    public float health = 10;
    public int jumpForce = 30;
    public bool stopped = false;

    private bool dirRight = true;
    private SpriteRenderer renderer;
    private GameObject player;
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (!stopped) {
            if(transform.position.x < player.transform.position.x) {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                renderer.flipX = false;
            } else {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                renderer.flipX = true;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "PlayerProjectile") {
            Destroy(collider.gameObject);
            Damage();
        } else if (collider.tag == "EnemyJump") {
            if (transform.position.y < player.transform.position.y) {
                Jump();
            }
        }
    }

    void Damage() {
        health -= 5;

        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    void Jump() {
        rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

}
