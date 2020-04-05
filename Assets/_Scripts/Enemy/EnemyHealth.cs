using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float startingHealth = 5.0f;
    public float currentHealth;
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
    private AudioSource[] audioSources;
    private Animator anim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        audioSources = GetComponents<AudioSource>();
        currentHealth = startingHealth;
        isDamaged = false;
        isDead = false;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        isDamaged = true;
        currentHealth -= damage;
        audioSources[2].Play();
        healthSlider.value = currentHealth;
        anim.ResetTrigger("EnemyTakeDamage");
        anim.SetTrigger("EnemyTakeDamage");
        if (currentHealth <= 0 && !isDead)
        {
            playerHealth.isVulnerable = false;
            Death();
        }
    }
    // Return a score depending on enemy health left. Score is used to calculate fitness.
    public float HealthRemainingScore()
    {
        if (currentHealth == 0)
        {
            return 0.0f;
        }

        else
        {
            return currentHealth / startingHealth;
        }
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
        audioSources[3].Play();
        playerMovement.enabled = false;
    }
}