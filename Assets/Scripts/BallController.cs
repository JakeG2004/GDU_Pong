using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    public float angle = 0;
    public float speed = 10;

    public int leftScore = 0;
    public int rightScore = 0;
    public int winScore = 5;
    
    public float resetTime = 3.0f;

    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;
    public TextMeshProUGUI winText;
    
    public GameObject winContent;
    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //get the rb
        rb = GetComponent<Rigidbody2D>();

		// Start the game
        ResetGame();
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

		// Detect scoring
        if(transform.position.x < -10.0f)
        {
            rightScore++;
            rightScoreText.text = "Score: " + rightScore;
            CheckForWinner();
        }

        if(transform.position.x > 10.0f)
        {
            leftScore++;
            leftScoreText.text = "Score: " + leftScore;
            CheckForWinner();
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
    	// Detect collision with paddle
        if(collision.gameObject.tag == "paddle")
        {
            rb.velocity = new Vector2(rb.velocity.x * -1, rb.velocity.y);
        }
    }

    void ResetBall()
    {
    	// Get new angle
    	angle = ChooseAngle();
    	
    	// Center and stop
        transform.position = new Vector2(0, 0);
        rb.velocity = new Vector2(0, 0);
        
        // Start reset timer
        StartCoroutine(WaitOnReset());
    }
    
    IEnumerator WaitOnReset()
    {
    	// Wait before starting again
    	yield return new WaitForSeconds(resetTime);
    	
    	// Start moving again
    	rb.velocity = speed * DecomposeAngle(angle);
    }

    Vector2 DecomposeAngle(float angle)
    {
        Vector2 retVec = new Vector2();

        //fill values
        retVec.x = Mathf.Cos(angle);
        retVec.y = Mathf.Sin(angle);

        return retVec;
    }
    
    float ChooseAngle()
    {
    	// Choose random angle
    	float tmpAngle = Random.Range(-1f, 1f);
    	
    	// Chance to turn ball 180 degrees
    	if(Random.Range(0.0f, 1.0f) >= 0.5f)
    	{
    		tmpAngle += 3;
    	}
    	
    	return tmpAngle;
    }
    
    void CheckForWinner()
    {
    	// If left win
    	if(leftScore >= winScore)
    	{
    		winContent.SetActive(true);
    		
    		// Stop ball from moving and center
    		rb.velocity = new Vector2(0, 0);
    		transform.position = new Vector2(0, 0);
    		
    		winText.text = "Left Wins!";
    	}
    	
    	// If right win
    	else if(rightScore >= winScore)
    	{
    		winContent.SetActive(true);
    		
    		// Stop ball from moving and center
    		rb.velocity = new Vector2(0, 0);
    		transform.position = new Vector2(0, 0);
    		
    		winText.text = "Right Wins!";
    	}
    	
    	// Otherwise, normal score
    	else
    	{
    		ResetBall();
    	}
    }
    
    public void ResetGame()
    {
    	winContent.SetActive(false);
    	
    	leftScore = 0;
    	rightScore = 0;
    	
    	leftScoreText.text = "Score: 0";
    	rightScoreText.text = "Score: 0";
    	winText.text = "Wins!";
    	
    	ResetBall();
    }
}

/*
CHANGLEOG
10/21/24
* Added public void ResetGame()
* Added void CheckForWinner();
* Added float ChooseAngle()
* Added IENumerator WaitOnReset()
* added public winScore;
* added public winText
* added public GameObject winContent
* added public float ResetTime

* Simplified start to use ResetGame()
* CheckForWinner() when score made
*/
