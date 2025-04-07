using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that handles the main menu
/// </summary>
public class PlayerAmountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject[] playerInputFields;
    [SerializeField] private GameObject playerMemory;

    private int player;

    private const int MIN_PLAYER = 3;
    private const int MAX_PLAYER = 9;

    /*--- UNITY FUNCTIONS ---*/

    private void Start()
    {
        player = MIN_PLAYER;
        UpdateUI();
        UpdatePlayerInputFields();
    }

    /*--- PRIVATE FUNCTIONS ---*/

    /// <summary>
    /// Function for updating the UI
    /// </summary>
    private void UpdateUI()
    {
        text.text = player.ToString() + " Player";
    }

    /// <summary>
    /// Function for updating the playerNameInputFields UI
    /// </summary>
    private void UpdatePlayerInputFields()
    {
        for (int i = 0; i < playerInputFields.Length; i++)
        {
            if (i < player)
            {
                playerInputFields[i].SetActive(true);
            }
            else
            {
                playerInputFields[i].SetActive(false);
            }
        }
    }

    /*--- PUBLIC FUNCTIONS ---*/

    /// <summary>
    /// Function for adding a player
    /// </summary>
    public void AddPlayer()
    {
        if (player < MAX_PLAYER)
        {
            player++;
            UpdateUI();
            UpdatePlayerInputFields();
        }
    }

    /// <summary>
    /// Function for removing a player
    /// </summary>
    public void RemovePlayer()
    {
        if (player > MIN_PLAYER)
        {
            player--;
            UpdateUI();
            UpdatePlayerInputFields();
        }
    }

    /// <summary>
    /// Function for starting the game
    /// </summary>
    public void StartGame()
    {
        string[] players = new string[player];

        for (int i = 0; i < player; i++)
        {
            players[i] = playerInputFields[i].GetComponent<TMP_InputField>().text;
        }

        GameObject memory = Instantiate(playerMemory);
        memory.GetComponent<PlayerMemory>().SetPlayers(players);
        DontDestroyOnLoad(memory);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Function for qutting the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
