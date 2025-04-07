using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for describing the structe of the playerMemory that is shared between the scenes
/// </summary>
public class PlayerMemory : MonoBehaviour
{
    [SerializeField] private string[] players;

    /*--- PUBLIC FUNCTIONS ---*/

    /// <summary>
    /// Function for setting the players based on a string[] input
    /// </summary>
    /// <param name="players">string[] that describes the players</param>
    public void SetPlayers(string[] players)
    {
        this.players = players;
    }

    /// <summary>
    /// Function for getting the saved players as a string[]
    /// </summary>
    /// <returns>string[] that describes the players</returns>
    public string[] GetPlayers()
    {
        return players;
    }
}
