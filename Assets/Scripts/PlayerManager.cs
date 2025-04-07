using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Script for handling the players
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private Color deathColor;
    [SerializeField] private FinalManager finalManager;

    private string playerName = "Player";
    private int playerHealth;
    private Color lifeColor;

    private const int MAX_HEALTH = 3;
    private const float ONE_HEALTH = 0.33f;
    private const float TWO_HEALTH = 0.66f;

    /*--- UNITY FUNCTIONS ---*/

    private void Start()
    {
        playerHealth = MAX_HEALTH;
        lifeColor = background.color;
        SetHealthBarUI();
        finalManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FinalManager>();
    }

    /*--- PRIVATE FUNCTIONS ---*/

    /// <summary>
    /// Function for updating the playerHealth-UI based on the playerHealth
    /// </summary>
    private void SetHealthBarUI()
    {
        switch (playerHealth)
        {
            case 0:
                healthBar.fillAmount = 0f;
                break;
            case 1:
                healthBar.fillAmount = ONE_HEALTH;
                break;
            case 2:
                healthBar.fillAmount = TWO_HEALTH;
                break;
            case 3:
                healthBar.fillAmount = 1f;
                break;
        }
    }

    /// <summary>
    /// Function for checking if the player should be dead, and updateing the UI accordingly
    /// </summary>
    private void CheckDeath()
    {
        if (playerHealth <= 0)
        {
            background.color = deathColor;
        }
        else
        {
            background.color = lifeColor;
        }
    }

    /// <summary>
    /// Function for updating the UI-nametag
    /// </summary>
    private void UpdatePlayerNameUI()
    {
        playerNameText.text = playerName;
    }

    /*--- PUBLIC FUNCTIONS ---*/

    /// <summary>
    /// Setter for the playerName, that triggers an UI-update
    /// </summary>
    /// <param name="name">new name of the player</param>
    public void SetPlayerName(string name)
    {
        this.playerName = name;
        UpdatePlayerNameUI();
    }

    /// <summary>
    /// Setter for playerHealth
    /// </summary>
    /// <returns>Returns the health of the player as int</returns>
    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    /// <summary>
    /// Function for removing health from the player
    /// </summary>
    public void RemoveHealth()
    {
        if (playerHealth > 0)
        {
            playerHealth--;
            SetHealthBarUI();
            CheckDeath();
            finalManager.UpdateFinalButtonUI();
        }
    }

    /// <summary>
    /// Function for adding health to the player
    /// </summary>
    public void AddHealth()
    {
        if (playerHealth < MAX_HEALTH)
        {
            playerHealth++;
            SetHealthBarUI();
            CheckDeath();
            finalManager.UpdateFinalButtonUI();
        }
    }
}
