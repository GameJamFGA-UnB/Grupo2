using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private int speed = 15;
    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jumping = !rb2D.IsTouchingLayers();

        if (Input.GetKey(KeyCode.A)) {
            // rb2D.MovePosition(rb2D.position + (Vector2.left * speed * Time.deltaTime) );
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)) {
            // rb2D.MovePosition(rb2D.position + (Vector2.right * speed * Time.deltaTime) );
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.W) && !jumping) {
            jumping = true;
            rb2D.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        }
    }
}
