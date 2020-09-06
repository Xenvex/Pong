using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private float ampInitial = 50;
    private float amp = 20;
    private float embark = -1;

    public Vector3 originalPos;

    private float LeftPoints = 0;
    private float RightPoints = 0;

    

    //private float direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Getting/Setting Original position to help reset later when the ball scores
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        rb = GetComponent<Rigidbody>();

        

        AddForce(embark);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //rb.AddExplosionForce(this.Vector3.right * amp);
    }

    private void AddForce(float direction)
    {
        rb.AddForce(new Vector3(ampInitial * direction, 0, 10));
    }

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("I hit a " + other.gameObject.name);

        //Change color of the ball
        gameObject.GetComponent<MeshRenderer>().material.color = NewColor();

        

    }

    void OnCollisionExit(Collision other)
    {
        amp += 1;
        embark = embark * -1;
        //rb.velocity = new Vector2((amp-=1) * embark, 0);

    }

    private Color NewColor()
    {
        Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        return color;
    }

    void OnTriggerEnter(Collider collider)
    {
        //For Direction (-1) = left, (1) = right
        if(collider.tag == "LeftGoal")
        {
            Debug.Log("Right Paddle has scored in the left goal!");

            RightPoints += 1;//Adds a Point to Right Player since the scored

            Debug.Log("Current Score:" + LeftPoints + " - " + RightPoints); //Display Current Score

            //Checking if it's Game Over
            if(RightPoints == 3)
            {
                Debug.Log("Game Over, Right Paddle Wins");
                LeftPoints = 0;
                RightPoints = 0;
                
            }

            embark = embark * 0 + (-1); //Setting the direction to go left(when ball resets)


            
            gameObject.transform.position = originalPos;
            AddForce(embark);

            rb.AddForce(new Vector3(1, 0, 10));

        }


        if (collider.tag == "RightGoal")
        {
            Debug.Log("Left Paddle has scored in the right goal!");

            LeftPoints += 1;//Adds a Point to Left Player since the scored

            Debug.Log("Current Score:" + LeftPoints + " - " + RightPoints); //Display Current Score//Display Current Score

            //Checking if it's Game Over
            if (LeftPoints == 3)
            {
                Debug.Log("Game Over, Left Paddle Wins");
                LeftPoints = 0;
                RightPoints = 0;
                
            }

            embark = embark * 0 + (1); //Setting the direction to go right(when ball resets)
            gameObject.transform.position = originalPos;
            AddForce(embark);
            rb.AddForce(new Vector3(-1, 0, 10));
        }

        
    }


}
