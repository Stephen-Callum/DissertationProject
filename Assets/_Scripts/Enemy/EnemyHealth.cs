using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public float flashSpeed = 5.0f;
    public Slider healthSlider;
    public Image damageImage;
    public Color flashColour = new Color(1.0f, 0.0f, 1.0f, 0.1f);

    private GameObject player;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    private bool isDead;
    private bool isDamaged;
    private float healthRemaining;

    public void TakeDamage(int damage)
    {
        isDamaged = true;
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        if(currentHealth <= 0 && !isDead)
        {
            playerHealth.isVulnerable = false;
            Death();
        }
    }

    // Calculate percentage health left for enemey
    public float HealthRemainingScore()
    {
        if (currentHealth > 0)
        {
            return healthRemaining = 1 - (currentHealth / startingHealth);
        }
        else if(currentHealth == 0)
        {
            return healthRemaining = 1.0f;
        }
        return 0;
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
        isDamaged = false;
        isDead = false;
    }

    private void Update()
    {
        OnHitFlash();
    }

    private void OnHitFlash()
    {
        if (isDamaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        isDamaged = false;
    }

    
    
    private void Death()
    {
        isDead = true;
        playerMovement.enabled = false;
    }
}