using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    public float rotationSpeed;

    public Rigidbody2D rb2dplayer;

	// Use this for initialization
	void Start () {
        rb2dplayer = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Rigidbody physics calculations every fixed framerate frame
    private void FixedUpdate()
    {
        // Floating point variable for vertical input
        float thrust = Input.GetAxis("Vertical");
        
        // Floating point variable for horizontal input
        float rotate = Input.GetAxis("Horizontal");

        // Force added to relative y axis of rigidbody
        rb2dplayer.AddRelativeForce(Vector2.up * thrust * speed);

        // Rotate rigidbody on z axis
        transform.Rotate(0, 0, -rotate * rotationSpeed);
    }
}
