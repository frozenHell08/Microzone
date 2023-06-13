using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MadeEnums;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private Transform gameCanvas;

    private LevelLogic _levelLogic;
    public Category category;
    private Enemy enemy;
    private int enemyAttack;
    private int charaRes;

    Func<int, int, int> defenseCalc = (resistance, level) => (resistance * 3) + level;

    void Start() {
        _levelLogic = GameObject.Find("GameController").GetComponent<LevelLogic>();
        enemy = _levelLogic.GetEnemySource();
        enemyAttack = enemy.enemyAttack;
        category = enemy.category;
        healthBar.UpdateHealthBar(ch.maxHealth, ch.currentHealth);

        switch (category) {
            case Category.Bacteria :
                charaRes = ch.res_bacteria;
                break;
            case Category.Parasite : 
                charaRes = ch.res_parasite;
                break;
            case Category.Virus :
                charaRes = ch.res_virus;
                break;
        }
    }

    void Update() {
        if (ch.currentHealth <= 0) {
            _levelLogic.FailedLevel();
        }
    }

    void OnCollisionEnter(Collision col) {
        Transform children = gameObject.transform;

        if (col.gameObject.CompareTag("Enemy")) {
            Debug.Log("enemy hit");

            foreach (ContactPoint contact in col.contacts) {
                Collider thisCollider = contact.thisCollider;

                if (thisCollider.CompareTag("Player")) {
                    Debug.Log("Player hit");
                    EnemyPlayerHit();
                    break;
                } else if (thisCollider.CompareTag("Weapon")) {
                    Debug.Log("Weapon hit");
                    EnemyWeaponHit(col);
                    break;
                }
            }  
        }
    }

    private void EnemyPlayerHit() {
        int defense = defenseCalc(charaRes, ch.level);
        int calculatedAttack = enemyAttack - defense;

        if ((enemyAttack - defense) < 0) calculatedAttack = 0;

        ch.currentHealth -= calculatedAttack;
        rController.UpdateHealthInRealm();

        healthText.text = $"HP\t{ch.currentHealth}/{ch.maxHealth}";
        healthBar.UpdateHealthBar(ch.maxHealth, ch.currentHealth);

        Vector3 characterPosition = this.gameObject.transform.position;

        RectTransform canvasRectTransform = gameCanvas.GetComponent<RectTransform>();
        Vector2 screenPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Camera.main.WorldToScreenPoint(characterPosition), Camera.main, out screenPosition);

        Vector3 spawnPosition = canvasRectTransform.TransformPoint(screenPosition);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = $"-{calculatedAttack}";
    }

    private void EnemyWeaponHit(Collision collision) { // weapon damage etc
        EnemyHandler e = collision.gameObject.GetComponent<EnemyHandler>();

        if (ch.equippedSolution == null) {
            e.TakeDamage(2);
            return;
        }

        FieldInfo field = ch.solutionsCount.GetType().GetFields().FirstOrDefault(f => f.Name.Equals(ch.equippedSolution.solutionName, StringComparison.OrdinalIgnoreCase));

        int solnamount = (int) field.GetValue(ch.solutionsCount);

        int soldmg = 0;

        if (ch.equippedSolution._category.Equals(category)) {
            soldmg = ch.equippedSolution.attackPoints;
        }

        if (solnamount > 0) {
            field.SetValue(ch.solutionsCount, (solnamount - 1));
            //save changes to realm
            rController.UseSolution(ch.equippedSolution.solutionName, solnamount - 1);
            e.TakeDamage(soldmg);

            if (soldmg == 0) e.DuplicateEnemy();
        } else {
            e.TakeDamage(2);
        }
    }
}

#if UNITY_EDITOR
[ CustomEditor ( typeof(CollisionHandler) ) ]
public class CollisionHandlerEditor : Editor {

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Header("Source");
        Property("rController");
        Property("ch");
        Property("gameCanvas");
        Property("healthBar");
        
        Header("Screen texts");
        Property("healthText");
        Property("damageTextPrefab");

        serializedObject.ApplyModifiedProperties();
    }

    private SerializedProperty GetSeriaProperty(string name) {
        return serializedObject.FindProperty(name);
    }

    private void Property(string name) {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
    }

    private void Header(string label) {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
}
#endif