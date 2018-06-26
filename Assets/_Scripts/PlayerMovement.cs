using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    public float rotationSpeed;

    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float thrust = Input.GetAxis("Vertical");
        float rotate = Input.GetAxis("Horizontal");

        rb2d.AddRelativeForce(Vector2.up * thrust * speed);
        transform.Rotate(0, 0, -rotate * rotationSpeed);
    }
}
