using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBullets : MonoBehaviour {

    ObjectPoolingSystem objectPoolingSystem;

    private void OnTriggerExit(Collider other)
    {
        GameObject otherObj = other.GetComponent<GameObject>();
        objectPoolingSystem.ReturnToPool(otherObj);
        Debug.Log(otherObj + " has exited the trigger");
    }

    // Use this for initialization
    void Start () {
        objectPoolingSystem = ObjectPoolingSystem.SharedInstance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
