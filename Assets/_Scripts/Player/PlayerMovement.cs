using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;

    public Rigidbody2D rb2dplayer;
	
	// Update is called once per frame
	private void Update()
    {
        // Check for vertical input from keyboard
        float thrust = Input.GetAxis("Vertical");

        // Check for horizontal input from keyboard
        float lateralMovement = Input.GetAxis("Horizontal");

        // Multiply floats for X and Y axis movement by speed 
        float xMovement = lateralMovement * speed * Time.deltaTime;
        float yMovement = thrust * speed * Time.deltaTime;

        // Create transform vector in accordance to input data
        Vector3 newPosition = new Vector3
        {
            x = transform.position.x + (xMovement * transform.right.x),
            y = transform.position.y + (yMovement * transform.up.y),
            z = transform.position.z + ((xMovement + yMovement) * transform.up.z)
        };

        // Set transform to newPosition
        transform.position = newPosition;
	}
}
