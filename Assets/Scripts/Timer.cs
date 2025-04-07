using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Script for handling the timer
/// </summary>
public class Timer : MonoBehaviour
{
    [SerializeField] private ChromaEffect chroma;
    [SerializeField] private GameObject questionUI;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject timerButtons;
    [SerializeField] private float roundTime;

    private float currentTime;
    private bool gameRunning = false;

    private const float MIN_ROUND_TIME = 60f;
    private const float MAX_ROUND_TIME = 540;
    private const float TIMER_STEP = 10f;
    private const float defaultHue = 0.475f;
    private const float pausedHue = 0.175f;
    private const float playingHue = 0.3f;
    private const float endHue = 1;
    private QuizManager quizManager;
    private GameObject[] healthButtons;

    /*---  UNITY FUNCTIONS ---*/

    private void Start()
    {
        currentTime = roundTime;
        UpdateQuestionUI(false);
        quizManager = GetComponent<QuizManager>();
        healthButtons = GameObject.FindGameObjectsWithTag("HealthButton");
        UpdateTimerUI();
    }

    private void Update()
    {
        // Checks if the timer IU should be updated or round has ended
        if (gameRunning && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                currentTime = 0;
            }
            UpdateTimerUI();
        }
        else if (currentTime == 0 && gameRunning)
        {
            EndRound();
        }
    }

    /*--- PRIVATE FUNCTIONS ---*/

    /// <summary>
    /// Function for setting the round to the start
    /// </summary>
    private void SetRoundToStart()
    {
        currentTime = roundTime;
        UpdateQuestionUI(false);
        gameRunning = false;
        UpdateBackgroundColor(defaultHue);
        UpdateTimerUI();
    }

    /// <summary>
    /// Function for pausing the round
    /// </summary>
    private void PauseRound()
    {
        gameRunning = false;
        UpdateQuestionUI(false);
        UpdateBackgroundColor(pausedHue);
        UpdateTimerUI();
    }

    /// <summary>
    /// Function for continueing the round after it has been paused
    /// </summary>
    private void ContinueRound()
    {
        gameRunning = true;
        UpdateQuestionUI(true);
        UpdateBackgroundColor(playingHue);
        UpdateTimerUI();
    }

    /// <summary>
    /// Function for starting the round
    /// </summary>
    private void StartRound()
    {
        currentTime = roundTime;
        gameRunning = true;
        UpdateQuestionUI(true);
        UpdateBackgroundColor(playingHue);
        quizManager.SetNewQuestions();
        UpdateTimerUI();
    }

    /// <summary>
    /// Function for ending the round
    /// </summary>
    private void EndRound()
    {
        currentTime = 0f;
        gameRunning = false;
        UpdateBackgroundColor(endHue);
        UpdateTimerUI();
    }

    /// <summary>
    /// Function for updating the timer UI and setting the value formatted
    /// </summary>
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        if (seconds > 9)
        {
            text.text = minutes.ToString() + ":" + seconds.ToString();
        }
        else
        {
            text.text = minutes.ToString() + ":0" + seconds.ToString();
        }
    }

    /// <summary>
    /// Function for updating the color of the background
    /// </summary>
    /// <param name="hue"></param>
    private void UpdateBackgroundColor(float hue)
    {
        chroma.SetHue(hue);
    }

    /// <summary>
    /// Function for updating the status of the question UI
    /// </summary>
    /// <param name="status"></param>
    private void UpdateQuestionUI(bool status)
    {
        questionUI.SetActive(status);
    }

    /// <summary>
    /// Function for updateing the status of the playerHealth buttons
    /// </summary>
    private void UpdateHealthButtons()
    {
        bool status = !gameRunning && currentTime == roundTime;

        foreach (var healthButtonGameObject in healthButtons)
        {
            healthButtonGameObject.SetActive(status);
        }
    }

    /// <summary>
    /// Function for updating the status of the timer button
    /// </summary>
    private void UpdateTimerButtons()
    {
        bool status = !gameRunning && currentTime == roundTime;

        timerButtons.SetActive(status);
    }

    /*--- PUBLIC FUNCTIONS ---*/

    /// <summary>
    /// Function for handling timer button presses based on the state of the round/game
    /// </summary>
    public void TriggerRoundButton()
    {
        if (currentTime == 0 && gameRunning == false)
        {
            SetRoundToStart();
        }
        else if (currentTime > 0 && currentTime < roundTime && gameRunning == true)
        {
            PauseRound();
        }
        else if (currentTime > 0 && currentTime < roundTime && gameRunning == false)
        {
            ContinueRound();
        }
        else if (currentTime == roundTime && gameRunning == false)
        {
            StartRound();
        }
        UpdateHealthButtons();
        UpdateTimerButtons();
    }

    /// <summary>
    /// Function for increasing the timer duration
    /// </summary>
    public void IncreaserTimer()
    {
        if (roundTime < MAX_ROUND_TIME)
        {
            roundTime += TIMER_STEP;
            currentTime = roundTime;
            UpdateTimerUI();
        }
    }

    /// <summary>
    /// Function for decreasing the timer duration
    /// </summary>
    public void DecreaseTimer()
    {
        if (roundTime > MIN_ROUND_TIME)
        {
            roundTime -= TIMER_STEP;
            currentTime = roundTime;
            UpdateTimerUI();
        }
    }
}
