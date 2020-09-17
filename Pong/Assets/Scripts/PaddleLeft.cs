using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLeft : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip1;
    public float amplify = 2;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        //if(Mathf.Abs(transform.position.z) < 8.0)
        {
            transform.Translate(new Vector3(0, 0, Input.GetAxis("LeftPaddle")) * Time.deltaTime * amplify); //Makes the Paddle move
        }
        
    }

    //when it colldies with the ball
    void onCollisionEnter(Collision other)
    {
        //audioSource.PlayOneShot(clip1);
        //audioSource.PlayOneShot(audioSource, 0.7f);
        
    }
}
