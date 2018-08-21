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
    private PlayerHealth playerHealth;
    private FireBullets fireBullets;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        restartText = GameObject.Find("RestartCountdown").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
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
        if (playerHealth.currentHealth <= 0)
        {
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
