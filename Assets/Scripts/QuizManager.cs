using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class that is used for the question.json format
/// </summary>
[System.Serializable]
public class QuizData
{
    public Question[] questions;
}

/// <summary>
/// Class that is used for the question.json format
/// </summary>
[System.Serializable]
public class Question
{
    public string question;
    public string answer;
}

/// <summary>
/// Script for handling the questions
/// </summary>
public class QuizManager : MonoBehaviour
{
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text answerText;
    [SerializeField] private TMP_Text leftQuestionsText;

    private Question currentQuestion;
    private QuizData quizData;

    private const int FINAL_QUESTIONS = 10;

    /*--- UNITY FUNCTIONS ---*/

    private void Start()
    {
        LoadQuizData();
        SetNewQuestions();
    }

    /*--- PRIVATE FUNCTIONS ---*/

    /// <summary>
    /// Function for loading the quizData from the json and saving it as Question[]
    /// </summary>
    private void LoadQuizData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("questionData");

        if (jsonData != null)
        {
            quizData = JsonUtility.FromJson<QuizData>(jsonData.text);
        }
        else
        {
            Debug.LogError("Failed to load quiz data!");
        }
    }

    /// <summary>
    /// Function for updating the question ui based on the currently selected question
    /// </summary>
    private void UpdateQuestionUI()
    {
        questionText.text = "Frage:\n" + currentQuestion.question;
        answerText.text = "Antwort:\n" + currentQuestion.answer;
        leftQuestionsText.text = quizData.questions.Length.ToString() + " questions left";
    }

    /*--- PUBLIC FUNCTIONS ---*/


    /// <summary>
    /// Function for getting a new question from the questions and removes them from the array, then updates the UI
    /// </summary>
    public void SetNewQuestions()
    {
        if (quizData.questions != null && quizData.questions.Length > 0)
        {
            int randomIndex = Random.Range(0, quizData.questions.Length);

            currentQuestion = quizData.questions[randomIndex];

            List<Question> remainingQuestions = new List<Question>(quizData.questions);
            remainingQuestions.RemoveAt(randomIndex);

            quizData.questions = remainingQuestions.ToArray();
        }
        else
        {
            currentQuestion = new Question
            {
                question = "No more questions available.",
                answer = "Well Well Well"
            };
        }
        UpdateQuestionUI();
    }

    /// <summary>
    /// Function that gets 10 questions from the questionsArray and removes them from the array
    /// </summary>
    /// <returns>Return an Question[] with 10 questions</returns>
    public Question[] GetFinalQuestions()
    {
        if (quizData.questions == null || quizData.questions.Length == 0)
        {
            Debug.LogError("No questions available to extract.");
            return new Question[0]; 
        }

        int questionsToRetrieve = Mathf.Min(FINAL_QUESTIONS, quizData.questions.Length);
        List<Question> questionList = new List<Question>(quizData.questions);
        List<Question> finalQuestions = new List<Question>();

        for (int i = 0; i < questionsToRetrieve; i++)
        {
            int randomIndex = Random.Range(0, questionList.Count);
            finalQuestions.Add(questionList[randomIndex]);
            questionList.RemoveAt(randomIndex);
        }

        quizData.questions = questionList.ToArray();
        return finalQuestions.ToArray();
    }
}
