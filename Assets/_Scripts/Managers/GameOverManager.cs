using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public float RestartDelay;
    private bool gameRunning;
    private float restartTimer;
    private float timeToRestart;
    private bool isOver;
    private GameObject player;
    private GameObject enemy;
    private GameObject gaManager;
    private Animator anim;
    private Text restartText;
    [SerializeField]
    private Text gameOverMessage;
    private AIController aIController; 
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private AudioFadeOut audioFade;
    private FireBullets fireBullets;
    private GameObject mainCamera;
    private AudioSource bgMusic;

    public Text GameOverMessage { get => gameOverMessage; set => gameOverMessage = value; }

    // One of the first functions to be called
    private void Awake()
    {
        anim = GetComponent<Animator>();
        restartText = GameObject.Find("RestartCountdown").GetComponent<Text>();
        gameOverMessage = gameObject.transform.GetChild(4).GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        gaManager = GameObject.FindGameObjectWithTag("GAManager");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        bgMusic = mainCamera.GetComponent<AudioSource>();
        aIController = gaManager.GetComponent<AIController>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        fireBullets = enemy.GetComponent<FireBullets>();
        isOver = false;
        timeToRestart = RestartDelay;
        SetRestartTimer();
    }

    private void Start()
    {
        gameRunning = true;
    }

    private void SetGameOverText()
    {
        if(playerHealth.currentHealth == 0)
        {
            gameOverMessage.text = "You Died!";
        } 
        else if (enemyHealth.currentHealth == 0)
        {
            gameOverMessage.text = "Victory!\nYou Beat Zeus!";
        }
    }

    private void SetRestartTimer()
    {
        restartText.text = timeToRestart.ToString();
    }

    private void Update()
    {
        if (IsGameOver())
        {
            if (gameRunning)
            {
                OnDeath();
            }

            DeathScreenAnimation();
        }
    }

    // Check if game is over based on player and enemy health
    public bool IsGameOver()
    {
        if (playerHealth.currentHealth <= 0 || enemyHealth.currentHealth <= 0)
        {
            return true;
        }

        return false;
    }

    private void ShutGameDown()
    {
        if (aIController.Save.NumOfGames == aIController.Save.Population.Count)
        {
            Application.Quit();
        }
    }

    // Handle what happens after death
    private void OnDeath()
    {
        gameRunning = false;
        //aIController.HealthFitnessFunction(aIController.Save.NumOfGames);
        //aIController.TimeFitnessFuntion(aIController.Save.NumOfGames);
        aIController.HealthAndTimeFitnessFunction(aIController.Save.NumOfGames);
        aIController.SaveGeneration(aIController.FullPath);
        StartCoroutine(AudioFadeOut.FadeOut(bgMusic, 3));
        SetGameOverText();
        anim.SetTrigger("GameOver");
        isOver = true;
        fireBullets.CeaseFire(isOver);
        ShutGameDown();
    }


    private void DeathScreenAnimation()
    {
        restartTimer += Time.deltaTime;
        timeToRestart -= Time.deltaTime;
        SetRestartTimer();
        if (restartTimer >= RestartDelay)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
