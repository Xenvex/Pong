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

    //private float direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        rb = GetComponent<Rigidbody>();
        //rb = transform.GetComponent<Rigidbody>(); same as the line above
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
        Debug.Log("I hit a " + other.gameObject.name);

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
        Debug.Log("I scored in the goal!");
        gameObject.transform.position = originalPos;
    }


}
