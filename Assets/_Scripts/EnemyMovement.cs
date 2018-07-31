using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Quaternion target;
    public float smoothing = 10.0f;
    Quaternion shipRotation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void FixedUpdate()
    {
        Quaternion.LookRotation(target.transform.position - transform.position);

        if (transform.rotation > target)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, shipRotation, Time.deltaTime * smoothing);
        }
    }
}
