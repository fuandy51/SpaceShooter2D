using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public GameObject heartPrefab;
    public Transform heartsParent;

    public GameObject gameOverMenu; // Reference to the game over menu

    private List<GameObject> heartsList = new List<GameObject>();

    private bool isDamageCooldown = false;
    public float damageCooldownTime = 1f; // Set the cooldown time

    private void Start()
    {
        currentHealth = maxHealth;
        InitializeHearts();
    }

    private void InitializeHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsParent);
            heartsList.Add(heart);
        }

        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < heartsList.Count; i++)
        {
            heartsList[i].SetActive(i < currentHealth);
        }

        // Check if health is depleted
        if (currentHealth <= 0)
        {
            ShowGameOverMenu();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDamageCooldown && (other.CompareTag("Enemy1") || other.CompareTag("Enemy2") || other.CompareTag("EnemyBullet")))
        {
            isDamageCooldown = true;
            TakeDamage(1);
            Destroy(other.gameObject);
            StartCoroutine(ResetDamageCooldown());
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();
    }

    private void ShowGameOverMenu()
    {
        // Activate the game over menu
        if (!string.IsNullOrEmpty("Game Over"))
        {
            SceneManager.LoadScene("Game Over");
        }
    }

    IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldownTime);
        isDamageCooldown = false;
    }
}
