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
    {
        Debug.Log(transform.position);
        ScreenWrap();
	}

    // Rigidbody physics calculations every fixed framerate frame
    private void FixedUpdate()
    {
        // Check for vertical input from keyboard
        float thrust = Input.GetAxis("Vertical");
        
        // Check for horizontal input from keyboard
        float rotate = Input.GetAxis("Horizontal");

        // Force added to relative y axis of rigidbody
        rb2dplayer.AddRelativeForce(Vector2.up * thrust * speed);

        // Rotate rigidbody on z axis
        transform.Rotate(0, 0, -rotate * rotationSpeed);
    }
    
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
    }
}
