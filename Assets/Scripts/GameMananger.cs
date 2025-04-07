using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script for handeling the game start
/// </summary>
public class GameMananger : MonoBehaviour
{
    [SerializeField] GameObject[] playerPanels;

    private PlayerMemory playerMemory;

    /*--- UNITY FUNCTIONS ---*/

    private void Awake()
    {
        // Making the app run in the background
        Application.runInBackground = true;
    }

    private void Start()
    {
        // Gets the game memory from a gameObject and initilases the game, else it returns to the mainmenu
        playerMemory = GameObject.FindGameObjectWithTag("PlayerMemory")?.GetComponent<PlayerMemory>();
        if (playerMemory == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            ActivatePlayerPanels();
        }
    }

    /*--- PRIVATE FUNCTIONS ---*/


    /// <summary>
    /// A function that initialises the game/the players
    /// </summary>
    private void ActivatePlayerPanels()
    {
        for (int i = 0; i < playerPanels.Length; i++)
        {
            if (i < playerMemory.GetPlayers().Length)
            {
                playerPanels[i].SetActive(true);
                playerPanels[i].GetComponent<PlayerManager>().SetPlayerName(playerMemory.GetPlayers()[i]);
            }
            else
            {
                playerPanels[i].SetActive(false);
            }
        }
    }
}
