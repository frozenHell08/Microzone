using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Realms;
using ProfileCreation;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private RealmController rController; //xxx
    [SerializeField] private CurrentCharacter ch;
    private ProfileModel profiledata; //xxx
    // Minigame mg;
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
    public GameObject GOPanel;
    public GameObject MGHome;
    public GameObject MGQuestion;
    public GameObject MG;

    public TMP_Text QuestionTxt;
    // public Image QuestionImage;
    public TMP_Text ScoreTxt;
    public TMP_Text GeneRewardTxt;

    int totalQuestions = 0;
    private int score;
    private double scoreEquiv = 0;
    private List<QuestionAndAnswers> tempStorage;


    private void OnEnable()
    {
        tempStorage = new List<QuestionAndAnswers>(QnA);
        totalQuestions = QnA.Count;
        GOPanel.SetActive(false);
        generateQuestion();

        profiledata = rController.FindProfile(ch.characterID);

        // totalQuestions = QnA.Count;
        // GOPanel.SetActive(false);
        // generateQuestion();
    }

    public void retry() {
        QnA.Clear();
        QnA.AddRange(tempStorage);
        tempStorage.Clear();
        score = 0;
        OnEnable();
        Quizpanel.SetActive(true);
    }

    public void GameOver()
    {   
        Quizpanel.SetActive(false);
        GOPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;

        GeneRewardTxt.text = score.ToString();

        if (score > 0) {
            ch.genes += score;

            rController.realmDB.Write(() => {
                profiledata.GeneCount += score;
            });
        }
    }

    public void correct()
    {
        // when answer is right
        score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        //when answer is wrong
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScripts>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScripts>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            // QuestionImage.sprite = QnA[currentQuestion].Question; // for image
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswer();
        }
        else
        {
            Debug.Log("Out of question");
            GameOver();
        }
    }
}
