using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private float amplify = 20;
    
    //private float direction = 1;
    //random comment here, don't mine me

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb = transform.GetComponent<Rigidbody>(); same as the line above
        AddForce(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            
        //}
        //rb.AddExplosionForce(this.Vector3.right * amplify);
    }

    private void AddForce(float direction)
    {
        rb.AddForce(new Vector3(1 * direction, 0, 0) * amplify);
    }

    void OnCollisionEnter(Collision other)
    {
        //Increase speed on bounce
        


        Debug.Log("I hit a " + other.gameObject.name);
        //other.gameObject.GetComponent<MeshRenderer>().material.color = NewColor();

        //Change color of the ball
        gameObject.GetComponent<MeshRenderer>().material.color = NewColor();

    }

    void OnCollisionExit(Collision other)
    {
        //other.gameObject.GetComponent<MeshRenderer>().material.color = NewColor();
        amplify += 1000;
    }

    private Color NewColor()
    {
        Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        return color;
    }


}
