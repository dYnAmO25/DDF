using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Script for handling the Final
/// </summary>
public class FinalManager : MonoBehaviour
{
    [SerializeField] private GameObject finalButton;
    [SerializeField] private ChromaEffect chroma;
    [SerializeField] private TMP_Text questionsText;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject timerUI;

    private PlayerManager[] playerManagers;
    private PlayerManager firstPlayer;
    private PlayerManager secondPlayer;
    private GameObject[] healthButtons;
    private bool finalActive = false;

    /*--- UNITY FUNCTIONS ---*/

    private void Start()
    {
        healthButtons = GameObject.FindGameObjectsWithTag("HealthButton");

        GameObject[] playerGameObjects;

        playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
        playerManagers = new PlayerManager[playerGameObjects.Length];

        for (int i = 0; i < playerGameObjects.Length; i++)
        {
            playerManagers[i] = playerGameObjects[i].GetComponent<PlayerManager>();
        }

        UpdateFinalButtonUI();
    }

    /*--- PRIVATE FUNCTIONS ---*/

    /// <summary>
    /// Function for getting the final 10 questions
    /// </summary>
    /// <returns>Returns the final ten questions with answers as a formatted string</returns>
    private string GetFinalQuestions()
    {
        string text = "";
        Question[] questions = quizManager.GetFinalQuestions();

        for (int i = 0; i < questions.Length; i++)
        {
            text += ("F: " + questions[i].question + " A: " + questions[i].answer + "\n");
        }
        return text;
    }

    /// <summary>
    /// Function for setting the player-HealthButtons based on the paramter
    /// </summary>
    /// <param name="status">A boolean that is used to set the gameObject.active tab</param>
    private void SetHealthButtons(bool status)
    {
        foreach (var healthButtonGameObject in healthButtons)
        {
            healthButtonGameObject.SetActive(status);
        }
    }

    /// <summary>
    /// Checks if the final should be available or not
    /// </summary>
    /// <returns></returns>
    private bool IsFinalReady()
    {
        bool gotFirstPlayer = false;

        int i = playerManagers.Length;

        foreach (var playerManager in playerManagers)
        {
            if (playerManager.GetPlayerHealth() == 0)
            {
                i--;
            }
            else
            {
                if (gotFirstPlayer)
                {
                    firstPlayer = playerManager;
                    gotFirstPlayer = true;
                }
                else
                {
                    secondPlayer = playerManager;
                }
            }
        }
        if (i == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*--- PUBLIC FUNCTIONS ---*/

    /// <summary>
    /// Function for setting the finalButton visible or not based on if the final is ready or not
    /// </summary>
    public void UpdateFinalButtonUI()
    {
        finalButton.SetActive(IsFinalReady());
    }

    /// <summary>
    /// Function for starting the final
    /// </summary>
    public void TriggerFinal()
    {
        finalActive = !finalActive;
        questionsText.gameObject.SetActive(finalActive);

        if (finalActive)
        {
            chroma.SetHue(1.1f);
            questionsText.text = GetFinalQuestions();
            SetHealthButtons(false);
            buttonText.text = "End Final";
            timerUI.SetActive(false);
        }
        else
        {
            chroma.SetHue(0.475f);
            SetHealthButtons(true);
            buttonText.text = "Start Final";
            timerUI.SetActive(true);
        }
    }
}
