using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    public float rotationSpeed;
    public float topOfScreen;
    public float bottomOfScreen;
    public float leftOfScreen;
    public float rightOfScreen;

    public Rigidbody2D rb2dplayer;

	// Use this for initialization
	void Start ()
    {
        rb2dplayer = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
<<<<<<< HEAD
    {
        Debug.Log(transform.position);
        ScreenWrap();
	}

    // Rigidbody physics calculations every fixed framerate frame
    private void FixedUpdate()
=======
>>>>>>> eb471397703b1a805448bfdf216c33ad6a18d2a6
    {
        // Check for vertical input from keyboard
        float thrust = Input.GetAxis("Vertical");
<<<<<<< HEAD
        
        // Check for horizontal input from keyboard
=======

        // Floating point variable for horizontal input
>>>>>>> eb471397703b1a805448bfdf216c33ad6a18d2a6
        float rotate = Input.GetAxis("Horizontal");

        float distance = thrust * speed * Time.deltaTime;
        Vector3 newPosition = new Vector3();
        newPosition.x = transform.position.x + (distance * transform.up.x);
        newPosition.y = transform.position.y + (distance * transform.up.y);
        newPosition.z = transform.position.z + (distance * transform.up.z);

        transform.position = newPosition;

        // Rotate rigidbody on z axis
        transform.Rotate(0, 0, -rotate * rotationSpeed);
    }
<<<<<<< HEAD
    
    // Handle Screen wrapping
    void ScreenWrap()
    {
        Vector2 newPosition = transform.position;
        if (transform.position.y > topOfScreen)
        {
            newPosition.y = bottomOfScreen;
        }
        if (transform.position.y < bottomOfScreen)
        {
            newPosition.y = topOfScreen;
        }
        if (transform.position.x > rightOfScreen)
        {
            newPosition.x = leftOfScreen;
        }
        if (transform.position.x < leftOfScreen)
        {
            newPosition.x = rightOfScreen;
        }
=======

    // Rigidbody physics calculations every fixed framerate frame
    private void FixedUpdate()
    {
        //// Floating point variable for vertical input
        //float thrust = Input.GetAxis("Vertical");
        
        //// Floating point variable for horizontal input
        //float rotate = Input.GetAxis("Horizontal");

        //// Force added to relative y axis of rigidbody
        //rb2dplayer.AddForce(transform.up * thrust * speed);

        //// Rotate rigidbody on z axis
        //transform.Rotate(0, 0, -rotate * rotationSpeed);
>>>>>>> eb471397703b1a805448bfdf216c33ad6a18d2a6
    }
}
