using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEMP : MonoBehaviour {

    [SerializeField]
    private int EMPDamage;
    private GameObject enemy;
    private EnemyHealth enemyHealth;
    private ObjectPoolingSystem objectPoolingSystem;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = enemy.GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.name);
            objectPoolingSystem.ReturnToPool("EMPCharge", gameObject);
            enemyHealth.TakeDamage(EMPDamage);
        }
    }

    private void Start()
    {
        objectPoolingSystem = ObjectPoolingSystem.SharedInstance;
    }
}
