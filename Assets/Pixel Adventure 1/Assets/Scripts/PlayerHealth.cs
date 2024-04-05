using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public Image healthBar;
    public TextMeshProUGUI healthText;
    
    void Awake()
    {
        currentHealth = maxHealth;
        
        // if the name of the player is PlayerPink
        if(gameObject.name == "playerPink")
        {
            // set the healthbar to an image under canvas called Health Canvas 2, the canvas has image called Grean, its the healthbar
            healthBar = GameObject.Find("Health Canvas 2").transform.Find("Green").GetComponent<Image>();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthText.text = currentHealth.ToString() + "%";
            healthBar.fillAmount = currentHealth / maxHealth;
            Debug.Log("Player is dead");
            //Die();
        }
        else
        {
            healthText.text = currentHealth.ToString() + "%";
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            healthText.text = maxHealth.ToString() + "%";
            currentHealth = maxHealth;
        }

        else
        {
            healthText.text = currentHealth.ToString() + "%";
        }
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    
}
