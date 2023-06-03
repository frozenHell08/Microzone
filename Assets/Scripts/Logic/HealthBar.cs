using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;

    private Camera cam;

    void Start() {
        cam = Camera.main;
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth) {
        healthBarSprite.fillAmount = currentHealth / maxHealth;

        Debug.Log($"health : {currentHealth}/{maxHealth}");
    }

    void Update() {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
