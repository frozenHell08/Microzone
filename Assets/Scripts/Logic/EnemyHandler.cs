using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Enemy enemyObj;
    [SerializeField] private TMP_Text enemyName;

    private int currentHealth;

    void Start()
    {
        currentHealth = enemyObj.enemyHp;
        healthBar.UpdateHealthBar(enemyObj.enemyHp, currentHealth);
        enemyName.text = enemyObj.enemyName;
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Destroy(gameObject);
        } else {
            healthBar.UpdateHealthBar(enemyObj.enemyHp, currentHealth);
        }
    }
}
