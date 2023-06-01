using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float duplicationInterval = 5f;

    private float timeSinceLastDuplication;

    public bool canDuplicate = true;

    private LevelLogic _levelLogic;

    void Start() {
        _levelLogic = GameObject.Find("GameController").GetComponent<LevelLogic>();
    }

    private void Update()
    {
        if (_levelLogic.GetStageStatus()) return; 

        timeSinceLastDuplication += Time.deltaTime;

        if (timeSinceLastDuplication >= duplicationInterval)
        {
            DuplicateEnemy();
            timeSinceLastDuplication = 0f;
        }
    }

    private void DuplicateEnemy()
    {
        float duplicateRadius = 5f; // Adjust the radius of the duplication circle as needed
        float duplicateAngle = 0f; // Starting angle for the duplicates

        int maxDuplicates = 4; // Maximum number of duplicates around the original
        float angleIncrement = 360f / maxDuplicates; // Angle increment between duplicates

        for (int i = 0; i < maxDuplicates; i++)
        {
            float angle = duplicateAngle + (i * angleIncrement);
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)) * duplicateRadius;
            Vector3 duplicatePosition = transform.position + offset;

            Collider[] colliders = Physics.OverlapSphere(duplicatePosition, 0.5f); // Adjust the sphere radius as needed

            if (colliders.Length == 0)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, duplicatePosition, transform.rotation);

                // Disable duplication for the cloned enemies
                newEnemy.GetComponent<Parasite>().canDuplicate = false;
                Debug.Log("duplicated");
                // Perform any additional setup or modifications on the new enemy, if needed
            }
        }
    }


}