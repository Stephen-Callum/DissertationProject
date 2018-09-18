using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    //[SerializeField]
    //private Quaternion leftTarget, rightTarget;
    [SerializeField]
    private float rotationSpeed;
    //private Quaternion enemyTransform;
    private float zRotation;

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        EnemyRotation();
	}

    void EnemyRotation()
    {
        //enemyTransform = GetComponent<Rigidbody2D>().transform.rotation;

        zRotation += Time.deltaTime * rotationSpeed;
        
        //if (transform.rotation.z < rightTarget.z)
        //{
        //    transform.rotation = Quaternion.Euler(0.0f, 0.0f, zRotation);
        //}
        

        //if (enemyTransform.z == leftTarget.rotation.z)
        //{
        //    rotationSpeed = Mathf.Abs(rotationSpeed);
        //}
        //if (enemyTransform.z == rightTarget.rotation.z)
        //{
        //    rotationSpeed = Mathf.Abs(rotationSpeed);
        //}
        //target = leftTarget;
        //GetComponent<Rigidbody2D>().transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);

        //if (target == leftTarget)
        //{
        //    target = rightTarget;
        //}

    }
}
