using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Map : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Warning warningMsg;
    [SerializeField] private Warning warningBactExam;
    [SerializeField] private Warning warningParaExam;
    [SerializeField] private string stagePrefix;
    [SerializeField] private Sprite locked, unlocked;
    [SerializeField] private GameObject examBactIcon;
    [SerializeField] private GameObject examParIcon;
    [SerializeField] private GameObject examVirIcon;

    StringComparison oic;

    void OnEnable() {
        oic = StringComparison.OrdinalIgnoreCase;
        
        int c = 0;

        foreach (Image img in mapPanel.GetComponentsInChildren<Image>(true)) {
            if (img.name.StartsWith(stagePrefix, oic)) {
                img.preserveAspect = true;

                if (c < ch.totalStages) {
                    img.sprite = unlocked;
                    img.GetComponent<Button>().interactable = true;
                } else {
                    img.sprite = locked;
                    img.GetComponent<Button>().interactable = false;
                }
                c++;
            }
        }

        Fresh();

        if (ch.totalStages >= 6) {
            AccessButton(examBactIcon, true);
        }

        if (ch.totalStages >= 12) {
            AccessButton(examParIcon, true);
        }

        if (ch.totalStages == 18) {
            AccessButton(examVirIcon, true);
        }
    }

    public void EnterStage(string stage) {
        string[] parts = stage.Split(' ');
        string stageNumber = parts.Last();
        int number = Int32.Parse(stageNumber);
        bool examVerdict;
        // 1-6 exam, 7-12 exam, 13-18 exam
        switch (number) {
            case 7 :
                Debug.Log("Accessing stage 7");
                examVerdict = GetVerdict("Bacteria");

                if (examVerdict) {
                    SceneManager.LoadScene(stage);
                } else {
                    TMP_Text text = messagePanel.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(x => x.name.Equals("message"));

                    text.text = warningBactExam.message;
                    messagePanel.SetActive(true);
                    return;
                }
                break;
            case 13 :
                // GetRating("parasite");
                break;
        }

        if (number == 7) {
            examVerdict = GetVerdict("Bacteria");

            if (examVerdict) {
                SceneManager.LoadScene(stage);
            } else {
                TMP_Text text = messagePanel.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(x => x.name.Equals("message"));

                text.text = warningBactExam.message;
                messagePanel.SetActive(true);
                return;
            }
        }

        if (number == 13) {
            examVerdict = GetVerdict("Parasite");

            if (examVerdict) {
                SceneManager.LoadScene(stage);
            } else {
                TMP_Text text = messagePanel.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(x => x.name.Equals("message"));

                text.text = warningBactExam.message;
                messagePanel.SetActive(true);
                return;
            }
        }

        if (ch.currentHealth <= 1) {
            TMP_Text text = messagePanel.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(x => x.name.Equals("message"));

            if (text == null) {
                Debug.Log("missing text field assignment");
            } else {
                text.text = warningMsg.message;
            }

            messagePanel.SetActive(true);
        } else {
            SceneManager.LoadScene(stage);
        }
    }

    public void StartExam(GameObject exam) {
        exam.SetActive(true);
    }

    private bool GetVerdict(string microbeExam) {
        bool verdict = false;

        Debug.Log($"microbe exam : {microbeExam}");
        float rating = rController.GetRating(microbeExam);
        Debug.Log(rating);
        if (rating >= 75) {
            verdict = true;
        }

        return verdict;
    }

    private void Fresh() {
        AccessButton(examBactIcon, false);
        AccessButton(examParIcon, false);
        AccessButton(examVirIcon, false);
    }

    private void AccessButton(GameObject obj, bool value) {
        Button btn = obj.GetComponent<Button>();
        btn.interactable = value;
    }
}

#if UNITY_EDITOR
[ CustomEditor (typeof(Map)) ]
public class MapEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Header("Source");
        Property("rController");
        Property("ch");
        Property("mapPanel");
        Property("stagePrefix");
        
        Header("Sprites");
        Property("locked");
        Property("unlocked");

        Header("Exam Icons");
        Property("examBactIcon");
        Property("examParIcon");
        Property("examVirIcon");

        Header("Warning");
        Property("warningMsg");
        Property("warningBactExam");
        Property("warningParaExam");
        Property("messagePanel");

        serializedObject.ApplyModifiedProperties();
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