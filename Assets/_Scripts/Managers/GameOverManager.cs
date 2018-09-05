using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    public float restartDelay;
    
    private float restartTimer;
    private float timeToRestart;
    private bool isOver;
    private GameObject player;
    private GameObject enemy;
    private Animator anim;
    private Text restartText;
    private AIController<Genes> aIController; 
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private FireBullets fireBullets;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        aIController = GetComponent<AIController<Genes>>();
        restartText = GameObject.Find("RestartCountdown").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        fireBullets = enemy.GetComponent<FireBullets>();
        isOver = false;
        timeToRestart = restartDelay;
        SetRestartText();
    }

    private void SetRestartText()
    {
        restartText.text = timeToRestart.ToString();
    }

    private void Update()
    {
        OnDeath();
    }

    private void OnDeath()
    {
        if (playerHealth.currentHealth <= 0 || enemyHealth.currentHealth <= 0)
        {
            aIController.FitnessFunction(aIController.numOfGames);
            // correct memory-wise?
            aIController.numOfGames++;
            // apply fitness function
            aIController.SaveGeneration(aIController.fullPath);
            anim.SetTrigger("GameOver");
            restartTimer += Time.deltaTime;
            timeToRestart -= Time.deltaTime;
            SetRestartText();
            if(restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            isOver = true;
            fireBullets.CeaseFire(isOver);
        }
    }
}
