using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    // Start is called before the first frame update

    float initialPos;
    void Start()
    {
        initialPos = transform.position.x ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(initialPos - transform.position.x ) > 10) {
            Destroy(gameObject);
        }
    }

}
