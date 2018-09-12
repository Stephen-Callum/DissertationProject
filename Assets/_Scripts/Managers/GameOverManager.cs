using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    public float RestartDelay;
    
    private float restartTimer;
    private float timeToRestart;
    private bool isOver;
    private GameObject player;
    private GameObject enemy;
    private GameObject gAManager;
    private Animator anim;
    private Text restartText;
    private AIController aIController; 
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private FireBullets fireBullets;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        restartText = GameObject.Find("RestartCountdown").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        gAManager = GameObject.FindGameObjectWithTag("GAManager");
        aIController = gAManager.GetComponent<AIController>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        fireBullets = enemy.GetComponent<FireBullets>();
        isOver = false;
        timeToRestart = RestartDelay;
        SetRestartText();
    }

    private void SetRestartText()
    {
        restartText.text = timeToRestart.ToString();
    }

    private void Update()
    {
        OnDeath();
        Debug.Log("number of game = " + aIController.NumOfGames);
    }

    private void OnDeath()
    {
        if (playerHealth.currentHealth <= 0 || enemyHealth.currentHealth <= 0)
        {
            aIController.FitnessFunction(aIController.NumOfGames);
            // correct memory-wise? need to increase only once, maybe put it in a function to call once.
            // apply fitness function
            aIController.SaveGeneration(aIController.FullPath);
            anim.SetTrigger("GameOver");
            restartTimer += Time.deltaTime;
            timeToRestart -= Time.deltaTime;
            SetRestartText();
            if(restartTimer >= RestartDelay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            isOver = true;
            fireBullets.CeaseFire(isOver);
        }
    }
}
