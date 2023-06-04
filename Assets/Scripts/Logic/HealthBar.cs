using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float reduceSpeed = 2;

    private float target = 1;
    private Camera cam;

    void Start() {
        cam = Camera.main;
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth) {
        target = currentHealth / maxHealth;
    }

    void Update() {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}
