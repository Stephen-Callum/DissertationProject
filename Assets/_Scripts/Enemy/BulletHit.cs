using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

    [SerializeField]
    private int bulletDamage;

    private GameObject player;
    private PlayerHealth playerHealth;
    private ObjectPoolingSystem objectPoolingSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.name);
            objectPoolingSystem.ReturnToPool("DamagingBullet", gameObject);
            playerHealth.TakeDamage(bulletDamage);
        }
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Use this for initialization
    void Start()
    {
        objectPoolingSystem = ObjectPoolingSystem.SharedInstance;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
