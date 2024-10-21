using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    public float angle = 0;
    Rigidbody2D rb;

    public float speed = 10;

    public int leftScore = 0;
    public int rightScore = 0;

    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;

    // Start is called before the first frame update
    void Start()
    {
        //get the rb
        rb = GetComponent<Rigidbody2D>();

        //choose a random angle
        angle = Random.Range(-0.25f, 0.25f);

        //50% chance to flip angle to other side
        if(Random.Range(0.0f, 1.0f) >= 0.5f)
        {
            angle += 3;
        }

        //set initial velocity given an angle
        rb.velocity = speed * DecomposeAngle(angle);
    }

    // Update is called once per frame
    void Update()
    {
        //bounce off ceiling and floor
        if(Mathf.Abs(transform.position.y) > 4.75)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1);
            transform.position = new Vector2(transform.position.x, 4.74f * Mathf.Sign(transform.position.y));
        }

        if(transform.position.x < -10.0f)
        {
            rightScore++;
            rightScoreText.text = "Score: " + rightScore;
        }

        if(transform.position.x > 10.0f)
        {
            leftScore++;
            leftScoreText.text = "Score: " + leftScore;
        }
    }

    void ResetBall()
    {
        transform.position = new Vector2(0, 0);
        rb.velocity = new Vector2(0, 0);
    }

    Vector2 DecomposeAngle(float angle)
    {
        Vector2 retVec = new Vector2();

        //fill values
        retVec.x = Mathf.Cos(angle);
        retVec.y = Mathf.Sin(angle);

        return retVec;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "paddle")
        {
            rb.velocity = new Vector2(rb.velocity.x * -1, rb.velocity.y);
            Debug.Log("bounced!");
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncScoreOnClick : MonoBehaviour
{
public KeyCode eventKey = KeyCode.Space;

public TextMeshProUGUI scoreText;

public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(eventKey))
        {
        score++;
        scoreText.text = "Score: " + score;
        }
    }
}

/*
THE PLAN
* Fix paddle drift
* Dynamic rigidbodies with constraints on position
* Add public reset method to the ballController script
* Add colliders to left and right sides for score zones
* Upon collision, call an event that will increment the corresponding score
  and will also call the reset method on the ball
* Add text objects that the score zones will update
* Add a win screen (maybe)
*/

