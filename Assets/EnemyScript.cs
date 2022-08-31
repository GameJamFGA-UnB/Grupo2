using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float speed = 3;
    private bool dirRight = true;
    private float timer;
    private float moveTime = 2;

    void Start()
    {
    //    rb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        
        timer += Time.deltaTime;

        if(timer >= moveTime){
            dirRight = !dirRight;
            timer = 0f;
        }
    }
}
