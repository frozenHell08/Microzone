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
    [SerializeField] private GameObject textParent;
    [SerializeField] private Warning warningMsg;
    [SerializeField] private Warning warningBactExam;
    [SerializeField] private Warning warningParaExam;
    [SerializeField] private Warning warningStartExam;
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

        if (number == 7) {
            CheckExam("Bacteria", warningBactExam, stage);
            return;
        } else if (number == 13) {
            CheckExam("Parasite", warningParaExam, stage);
            return;
        }

        if (ch.currentHealth <= 5) {
            SetMessage(warningMsg);
        } else {
            SceneManager.LoadScene(stage);
        }
    }

    private void SetMessage(Warning msgSource) {
        TMP_Text text = messagePanel.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(x => x.name.Equals("message"));

        text.text = msgSource.message;
        messagePanel.SetActive(true);
    }

    public void StartAssessment(GameObject panel) {
        TMP_Text text = panel.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(x => x.name.Equals("confirmMessage"));

        text.text = warningStartExam.message;
        panel.SetActive(true);
    }

    public void StartExam(GameObject exam) {
        exam.SetActive(true);
        textParent.SetActive(false);
    }

    public void ExitExam(GameObject exam) {
        exam.SetActive(false);
        textParent.SetActive(true);
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

    private void CheckExam(string category, Warning warningMessage, string stage) {
        bool examVerdict = GetVerdict(category);

        if (examVerdict) {
            SceneManager.LoadScene(stage);
        } else {
            TMP_Text text = messagePanel.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(x => x.name.Equals("message"));

            text.text = warningMessage.message;
            messagePanel.SetActive(true);
        }
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
        Property("textParent");
        
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
        Property("warningStartExam");
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