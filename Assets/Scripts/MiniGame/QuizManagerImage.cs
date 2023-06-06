using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Realms;
using ProfileCreation;
using UnityEngine.SceneManagement;

public class QuizManagerImage : MonoBehaviour
{
    [SerializeField] private RealmController rController; 
    [SerializeField] private CurrentCharacter ch;
    private ProfileModel profiledata; 

    public List<ImageAndAnswers> QnA;
    private List<ImageAndAnswers> tempStorage;
    public GameObject[] options;
    public int currentQuestion;
    public GameObject Quizpanel;
    public GameObject GOPanel;
    public GameObject MGHome;
    public GameObject MGQuestionImage;
    public GameObject MG;
    public GameObject confirmPanel;
    public GameObject correctSprite;
    public GameObject wrongSprite;


    
    public Image QuestionImage;
    public TMP_Text ScoreTxt;
    public TMP_Text GeneRewardTxt;

    int totalQuestions = 0;
    private int score;
    //private double scoreEquiv = 0;





    private void OnEnable()
    {
        tempStorage = new List<ImageAndAnswers>(QnA);
        totalQuestions = QnA.Count;
        MGQuestionImage.SetActive(false);

        profiledata = rController.FindProfile(ch.characterID);
    }
    public void play()
    {
        
        generateQuestion();
        MGQuestionImage.SetActive(true);
        Quizpanel.SetActive(true);
    }

    public void retry() 
    {
        QnA.Clear();
        QnA.AddRange(tempStorage);
        tempStorage.Clear();
        score = 0;
        OnEnable();
        GOPanel.SetActive(false);
        MGHome.SetActive(true);
        
    }

    public void GameOver()
    {   
        Quizpanel.SetActive(false);
        GOPanel.SetActive(true);
        //ScoreTxt.text = score + "/" + totalQuestions; //change totalQuestion
        ScoreTxt.text = score + "/5" ;

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
        correctSprite.SetActive(true);
        confirmPanel.SetActive(true);
        

    }

    public void wrong()
    {
        //when answer is wrong
        QnA.RemoveAt(currentQuestion);
        wrongSprite.SetActive(true);
        confirmPanel.SetActive(true);
        
    }

    public void next()
    {
        confirmPanel.SetActive(false);
        correctSprite.SetActive(false);
        wrongSprite.SetActive(false);
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
        if (QnA.Count >= 14) //change total questions
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionImage.sprite = QnA[currentQuestion].image; // for image
            //QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswer(); 
        }
        else
        {
            Debug.Log("Out of question");
            GameOver();
        }
    }
}
