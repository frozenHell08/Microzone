using System;
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

                // Debug.Log($"normal : {contact.normal}");
                // Debug.Log($"othercollider : {contact.otherCollider}");
                // Debug.Log($"point : {contact.point}");
                // Debug.Log($"separation : {contact.separation}");
                // Debug.Log($"this collider : {contact.thisCollider}");

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

        // Debug.Log($"{}");
        // Debug.Log($"{}");
    }

    private void EnemyPlayerHit() {
        int defense = defenseCalc(charaRes, ch.level);
        int calculatedAttack = enemyAttack - defense;

        if ((enemyAttack - defense) < 0) calculatedAttack = 0;

        ch.currentHealth -= calculatedAttack;
        rController.UpdateHealthInRealm();

        healthText.text = $"HP\t{ch.currentHealth}/{ch.maxHealth}";
    }

    private void EnemyWeaponHit(Collision collision) { // weapon damage etc

        // Enemy e = collision.gameObject.GetComponent<Enemy>();

        Debug.Log(collision.gameObject);
        // Debug.Log(e);

        Destroy(collision.gameObject);
        Debug.Log("boop");
        // _levelLogic.AddKill();
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
        
        Header("Screen texts");
        Property("healthText");

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