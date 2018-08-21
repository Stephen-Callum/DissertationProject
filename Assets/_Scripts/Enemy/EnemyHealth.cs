using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public float flashSpeed = 5f;
    public Slider healthSlider;
    public Image damageImage;
    public Color flashColour = new Color(1f, 0f, 1f, 0.1f);

    private GameObject player;
    private PlayerMovement playerMovement;
    private bool isDead;
    private bool isDamaged;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

    public void TakeDamage(int damage)
    {
        isDamaged = true;
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    
    private void Death()
    {
        isDead = true;
        playerMovement.enabled = false;
    }
}