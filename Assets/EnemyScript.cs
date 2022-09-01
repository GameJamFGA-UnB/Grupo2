using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyScript : MonoBehaviour
{
    public float speed = 5;
    public float health = 10;
    public int jumpForce = 30;
    public bool stopped = false;

    private bool zombieMode = false;
    private Vector3 zombieDir;
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
        if (!stopped && !zombieMode) {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) < 1.5f
                && Mathf.Abs(transform.position.y - player.transform.position.y) > 5f) {
                zombieMode = true;
                zombieDir = Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;
                renderer.flipX = !(zombieDir.x > 0);
                // print("is zombie w/ " + Mathf.Abs(transform.position.x - player.transform.position.x));
            }

            if(transform.position.x < player.transform.position.x) {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                renderer.flipX = false;
            } else {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                renderer.flipX = true;
            }
        } else if (zombieMode) {
            transform.Translate(zombieDir * speed * Time.deltaTime);
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

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "EnemyJump" && zombieMode) {
            zombieMode = false;
            transform.Translate(zombieDir * speed * 2 * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), 
                gameObject.GetComponent<Collider2D>());
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
