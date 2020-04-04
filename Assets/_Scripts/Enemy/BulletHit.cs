using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{

    [SerializeField] private float bulletDamage;
    private GameObject player;
    private PlayerHealth playerHealth;
    private ObjectPoolingSystem objectPoolingSystem;

    public float BulletDamage { get => bulletDamage; set => bulletDamage = value; }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth.isVulnerable)
            {
                objectPoolingSystem.ReturnToPool("DamagingBullet", gameObject);
                playerHealth.TakeDamage(BulletDamage);
            }
        }
    }

    // Use this for initialization
    private void Start()
    {
        objectPoolingSystem = ObjectPoolingSystem.SharedInstance;
    }
}
