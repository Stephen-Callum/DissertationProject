using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour {

    public float startingHealth = 3.0f;
    public float currentHealth;
    public float healthRemaining;
    public float flashSpeed = 5.0f;
    public float invulnerabilityTime;
    [NonSerialized] public bool isVulnerable;
    public bool canCollect;
    public Slider healthSlider;
    public Image damageImage;
    public Color flashColour = new Color(1.0f, 0.0f, 0.0f, 0.1f);

    private PlayerMovement playerMovement;
    private bool isDead;
    private bool isDamaged;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
        invulnerabilityTime = 1.5f;
        isVulnerable = true;
        isDead = false;
        canCollect = true;
    }

	public void ResetVulnerability()
    {
        isVulnerable = true;
    }

    // Return a score based on player health remaining. Score is used to calculate fitness.
    public float HealthRemainingScore()
    {
        if (currentHealth == 0.0f)
        {
            return healthRemaining = 3.0f;
        }
        if(currentHealth == 1)
        {
            return 0.0f;
        }
        if (currentHealth == 2)
        {
            return 1.0f;
        }
        else
        {
            return 2.0f;
        }
    }

	// Update is called once per frame
	private void Update()
    {
        OnHitFlash();
    }
    
    // Called when the player takes damage. 'damage' refers to the amount of damage the player takes.
    public void TakeDamage(float damage)
    {
        if (isVulnerable)
        {
            isDamaged = true;
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            isVulnerable = false;
            if (currentHealth <= 0 && !isDead)
            {
                isVulnerable = false;
                Death();
            }
            Invoke("ResetVulnerability", invulnerabilityTime);
        }
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
        canCollect = false;
        playerMovement.enabled = false;
    }
}
