using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    private float ampInitial = 50;
    private float amp = 0;
    private float embark = -1;

    public Vector3 originalPos;

    private float LeftPoints = 0;
    private float RightPoints = 0;

    //Audio Stuff
    private AudioSource audioSource;
    public AudioClip leftClip;
    public AudioClip rightClip;

    public AudioClip leftGunClip;
    public AudioClip rightGunClip;

    //Gui Stuff
    public TextMeshProUGUI score;

    //Gui Stuff
    private TextMeshPro mText;



    // Start is called before the first frame update
    void Start()
    {
        //Audio Stuff
        audioSource = GetComponent<AudioSource>();

        //Gui Stuff
        mText = gameObject.GetComponent<TextMeshPro>();

        //Getting/Setting Original position to help reset later when the ball scores
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        rb = GetComponent<Rigidbody>();
    

        AddForce(embark); //Initial push (Set right now to go left initially)
        
    }

    // Update is called once per frame
    void Update()
    {
        //Showing the score[Gui]
        score.text = "Score\n" + LeftPoints + " - " + RightPoints;
    }

    void FixedUpdate()
    {
        
    }


    private void AddForce(float direction)
    {
        rb.AddForce(new Vector3(ampInitial * direction, 0, 1));
    }

    void OnCollisionEnter(Collision other)
    {
        //Audio Stuff
        //Make Sound when Paddles hit
        if (other.gameObject.name == "PaddleLeft")
        {
            //when it speeds past a certain point
            if (amp >= 4) //When Speed is Equal to or Greather than 15
            {
                audioSource.PlayOneShot(leftGunClip);
            }
            else
            {
                audioSource.PlayOneShot(leftClip);
            }
        }

        if (other.gameObject.name == "PaddleRight")
        {
            //when it speeds past a certain point
            if (amp >= 4)
            {
                audioSource.PlayOneShot(rightGunClip);
            }  
            else
            {
                audioSource.PlayOneShot(rightClip);
            }
        }


        //Change color of the ball
        if (other.gameObject.tag == "Paddle")
        {
            gameObject.GetComponent<MeshRenderer>().material.color = NewColor();
        }
        

        



    }

    void OnCollisionExit(Collision other)
    {
        amp += 1;
        embark = embark * -1;
        //rb.velocity = new Vector2((amp-=1) * embark, 0);
        rb.velocity = rb.velocity.normalized * (10 +amp);

    }

    private Color NewColor()
    {
        Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        return color;
    }

    void OnTriggerEnter(Collider collider)
    {
        //For Powerups
        if(collider.tag == "Power1")
        {
            //Speeds Up the Ball
            amp = (10 + amp) * 2;
            rb.velocity = rb.velocity.normalized * (10 + amp);

            //Destroys PowerUp
            Destroy(collider.gameObject);
        }

        if (collider.tag == "Power2")
        {
            //Slows down the Ball
            amp = 0;
            rb.velocity = rb.velocity.normalized * (1 + amp);

            //Destorys PowerUp
            Destroy(collider.gameObject);
        }


        //For Direction (-1) = left, (1) = right
        if (collider.tag == "LeftGoal")
        {
            //Changing the color of the text every time someone scores
            score.color = NewColor();

            Debug.Log("Right Paddle has scored in the left goal!");

            RightPoints += 1;//Adds a Point to Right Player since the scored

            Debug.Log("Current Score:" + LeftPoints + " - " + RightPoints); //Display Current Score

            //Checking if it's Game Over
            if(RightPoints == 11)
            {
                //Declare game over message and reset scores
                Debug.Log("Game Over, Right Paddle Wins");
                LeftPoints = 0;
                RightPoints = 0;
            }

            
            gameObject.transform.position = originalPos; //Reset ball to its original position

            amp = 0; //resets the amp counter(So that the ball speed incrememnts from 0)
            rb.velocity = rb.velocity.normalized * 0; //Resets the speed of the moving ball

            embark = embark * 0 + (-1); //Setting the direction to go left(when ball resets)
            AddForce(embark);


        }


        if (collider.tag == "RightGoal")
        {
            //Changing the color of the text every time someone scores
            score.color = NewColor();

            Debug.Log("Left Paddle has scored in the right goal!");

            LeftPoints += 1;//Adds a Point to Left Player since the scored

            Debug.Log("Current Score:" + LeftPoints + " - " + RightPoints); //Display Current Score//Display Current Score

            //Checking if it's Game Over
            if (LeftPoints == 11)
            {
                //Declare game over message and reset score
                Debug.Log("Game Over, Left Paddle Wins");
                LeftPoints = 0;
                RightPoints = 0;
            }

            
            gameObject.transform.position = originalPos; //Reset ball to its original position

            amp = 0; //resets the amp counter(So that the ball speed incrememnts from 0)
            rb.velocity = rb.velocity.normalized * 0; //Resets the speed of the moving ball

            embark = embark * 0 + (1); //Setting the direction to go right(when ball resets)s
            AddForce(embark);

            
            //rb.AddForce(new Vector3(-1, 0, 0));
        }

        
    }


}
