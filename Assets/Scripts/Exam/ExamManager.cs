using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExamManager : MonoBehaviour
{
    [SerializeField] private RealmController rController; 
    [SerializeField] private CurrentCharacter ch;

    public List<QuestionAndAnswers> QnA;
    
    public GameObject[] options;
    private int currentQuestion;
    public GameObject Quizpanel;
    public GameObject GOPanel;
    public GameObject MGHome;
    public GameObject MGQuestion;
    public GameObject examPanel;
    public GameObject confirmPanel;
    public GameObject scorePanel;

    public GameObject correctSprite;
    public GameObject wrongSprite;

    public TMP_Text QuestionTxt;
    // public Image QuestionImage;
    public TMP_Text ScoreTxt;
    public TMP_Text ratingTxt;
    public TMP_Text messagetxt;
    public TMP_Text scoreHistory;

    public Warning passed;
    public Warning failed;

    int totalQuestions = 0;
    private int score;
    private List<QuestionAndAnswers> tempStorage;

    void OnEnable() {
        tempStorage = new List<QuestionAndAnswers>(QnA);
        totalQuestions = QnA.Count;
        MGQuestion.SetActive(false);

        Debug.Log($"total questions : {totalQuestions}");
    }

    public void startExam() {
        Debug.Log("started?");
        generateQuestion();
        MGHome.SetActive(false);
        MGQuestion.SetActive(true);
        Quizpanel.SetActive(true);
    }

    private void generateQuestion() {
        if (tempStorage.Count > 0) {
            currentQuestion = UnityEngine.Random.Range(0, tempStorage.Count);
            QuestionTxt.text = tempStorage[currentQuestion].Question;
            SetAnswer();
        } else {
            Finished();
        }
    }

    private void SetAnswer() {
        for (int i = 0; i < options.Length; i++) {
            options[i].GetComponent<ExamAnswers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = tempStorage[currentQuestion].Answers[i];

            if (tempStorage[currentQuestion].CorrectAnswer == i + 1) {
                options[i].GetComponent<ExamAnswers>().isCorrect = true;
            }
        }
    }

    public void nextQuestion() {
        confirmPanel.SetActive(false);
        correctSprite.SetActive(false);
        wrongSprite.SetActive(false);
        generateQuestion();
    }

    public void correctAnswer() {
        score += 1;
        tempStorage.RemoveAt(currentQuestion);
        correctSprite.SetActive(true);
        confirmPanel.SetActive(true); 
    }

    public void wrongAnswer() {
        tempStorage.RemoveAt(currentQuestion);
        wrongSprite.SetActive(true);
        confirmPanel.SetActive(true);
    }

    private void Finished() {
        Quizpanel.SetActive(false);
        GOPanel.SetActive(true);

        ScoreTxt.text = $"{score}/{totalQuestions}";
        float decRate = (float) score / totalQuestions;
        float rating = decRate * 100;

        ratingTxt.text = $"{rating}%";

        if (rating >= 75) {
            messagetxt.text = string.Format(passed.message, rating);
        } else {
            messagetxt.text = string.Format(failed.message, rating);
        }

        string[] parts = examPanel.name.Split(' ');
        string assessmentCategory = parts.Last();

        rController.SaveExamScore(assessmentCategory, score, totalQuestions, rating);
    }

    public void ExitExam() {
        tempStorage.Clear();
        score = 0;

        GOPanel.SetActive(false);
        MGHome.SetActive(true);
        examPanel.SetActive(false);
    }

    public void ControlScoresHistory() {
        if (scorePanel.activeSelf) {
            scorePanel.SetActive(false);
        } else {
            scorePanel.SetActive(true);
            ShowRecords();
        }
    }

    private void ShowRecords() {
        string[] parts = examPanel.name.Split(' ');
        string assessmentCategory = parts.Last();

        var listofscores = rController.GetTopScores(assessmentCategory);

        string records = "";
        int i = 1;

        for (int j = 0; j < 10; j++) {
            if (j < listofscores.Count) {
                var score = listofscores[j];
                records += $"{i}. {score.date}\t{score.score}/{score.totalScore}\t\t{score.rating}%\n";
            } else {
                records += $"{i}. No Record\n";
            }

            i++;
        }

        scoreHistory.text = records;
    }
}

#if UNITY_EDITOR
[ CustomEditor (typeof(ExamManager)) ]
public class ExamManagerEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Header("Source");
        Property("rController");
        Property("ch");

        Header("Screen Panels");
        Property("Quizpanel");
        Property("GOPanel");
        Property("MGHome");
        Property("MGQuestion");
        Property("examPanel");
        Property("confirmPanel");
        Property("scorePanel");

        Header("Answer");
        Property("correctSprite");
        Property("wrongSprite");

        Header("Texts");
        Property("QuestionTxt");
        Property("ScoreTxt");
        Property("ratingTxt");
        Property("messagetxt");
        Property("scoreHistory");

        Header("Messages");
        Property("passed");
        Property("failed");

        Header("Questions");
        Property("QnA");
        Property("options");

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