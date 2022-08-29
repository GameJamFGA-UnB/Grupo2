using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private int speed = 600;
    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.A)) {
        //     // rb2D.MovePosition(rb2D.position + (Vector2.left * speed * Time.deltaTime) );
        // }

        // if (Input.GetKey(KeyCode.D)) {
        //     // rb2D.MovePosition(rb2D.position + (Vector2.right * speed * Time.deltaTime) );
        // }

        if (Input.GetKeyDown(KeyCode.W)) {
            jumping = true;
            rb2D.AddForce(Vector2.up * 300);
        }
    }
}
