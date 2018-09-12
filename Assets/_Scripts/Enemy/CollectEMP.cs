using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEMP : MonoBehaviour {

    [SerializeField] private int EMPDamage;
    private GameObject enemy;
    private GameObject player;
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;
    private ObjectPoolingSystem objectPoolingSystem;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = enemy.GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth.canCollect)
            {
                Debug.Log(collision.name);
                objectPoolingSystem.ReturnToPool("EMPCharge", gameObject);
                enemyHealth.TakeDamage(EMPDamage);
            }
        }
    }

    private void Start()
    {
        objectPoolingSystem = ObjectPoolingSystem.SharedInstance;
    }
}
