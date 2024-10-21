using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    Rigidbody2D rb;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;

    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //get the rigid body of the paddle
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //set new velocity
        Vector2 newVector = new Vector2(0, 0);

        //handle up pressed
        if(Input.GetKey(up))
        {
            newVector.y = speed;
        }

        //handle down pressed
        else if(Input.GetKey(down))
        {
            newVector.y = -speed;
        }

        //set velocity vector of rb
        rb.velocity = newVector;
    }
}
