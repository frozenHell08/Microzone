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

    private void OnEnable()
    {
        totalQuestions = QnA.Count;
        GOPanel.SetActive(false);
        generateQuestion();
    }

    /*void OnEnable()
    {
        profiledata = rController.FindProfile(GeneralData.player_LoggedIn);
    }*/

    public void retry() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void GameOver()
    {   
        Quizpanel.SetActive(false);
        GOPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;


        scoreEquiv = score * .10;

        // if (scoreEquiv == 0)
        // {
        //     GeneRewardTxt.text = "0"; // reward (0)

        //     rController.realmDB.Write(() => {
        //         profiledata.firstLogin = false;
        //         profiledata.GeneCount += 0 ; //reward gene <<<<
        //     });
        // }
        // else if(scoreEquiv <= 3 && scoreEquiv != 0)
        // {
        //     GeneRewardTxt.text = "1"; // reward (1-3)

        //     rController.realmDB.Write(() => {
        //         profiledata.firstLogin = false;
        //         profiledata.GeneCount += 1; //reward gene <<<<
        //     });
        // }
        // else if (scoreEquiv >= 4 && scoreEquiv <= 9)
        // {
        //     GeneRewardTxt.text = "2"; // reward (4-9)

        //     rController.realmDB.Write(() => {
        //         profiledata.firstLogin = false;
        //         profiledata.GeneCount += 2; //reward gene <<<< mali yata haha
        //     });
        // }
        // else if (scoreEquiv >= 10 && scoreEquiv <= 14)
        // {
        //     GeneRewardTxt.text = "3";// reward (9-14)

        //     rController.realmDB.Write(() => {
        //         profiledata.firstLogin = false;
        //         profiledata.GeneCount += 3; //reward gene <<<<
        //     });
        // }
        // else if (scoreEquiv == 15)
        // {
        //     GeneRewardTxt.text = "5";// reward (15)

        //     rController.realmDB.Write(() => {
        //         profiledata.firstLogin = false;
        //         profiledata.GeneCount += 5; //reward gene <<<<
        //     });
        // }
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
