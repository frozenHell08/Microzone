using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamAnswers : MonoBehaviour
{
    public bool isCorrect = false;
    public ExamManager examManager;

    public void Answer() {
        if (isCorrect) {
            examManager.correctAnswer();
        } else {
            examManager.wrongAnswer();
        }
    }
}
