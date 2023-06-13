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

    public void DuplicateEnemy() {
        float duplicateRadius = 5f;
        float duplicateAngle = 0f;
        float angleIncrement = 360f / 1;

        float angle = duplicateAngle + angleIncrement;

        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)) * duplicateRadius;
        Vector3 duplicatePosition = transform.position + offset;

        GameObject enemyPrefab = this.gameObject;

        Collider[] colliders = Physics.OverlapSphere(duplicatePosition, 0.5f);

        GameObject newEnemy = Instantiate(enemyPrefab, duplicatePosition, transform.rotation);
    }
}
