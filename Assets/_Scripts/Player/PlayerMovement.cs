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

        float xMovement = lateralMovement * speed * Time.deltaTime;
        float yMovement = thrust * speed * Time.deltaTime;

        Vector3 newPosition = new Vector3();
        newPosition.x = transform.position.x + (xMovement * transform.right.x);
        newPosition.y = transform.position.y + (yMovement * transform.up.y);
        newPosition.z = transform.position.z + ((xMovement + yMovement) * transform.up.z);

        transform.position = newPosition;
	}
}
