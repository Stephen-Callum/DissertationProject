using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public float flashSpeed = 5f;
    public float invulnerabilityTime;
    [NonSerialized]
    public bool isVulnerable;
    public Slider healthSlider;
    public Image damageImage;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    // Animator anim;
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
    }
	
	// Update is called once per frame
	private void Update()
    {
        OnHitFlash();
    }
    
    // Called when the player takes damage. 'damage' refers to the amount of damage the player takes.
    public void TakeDamage(int damage)
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

    public void ResetVulnerability()
    {
        isVulnerable = true;
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
