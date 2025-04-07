using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// Script for handling a "Hold-Button" and switching the scene back by one
/// </summary>
public class BackButton : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private float loadSpeed;
    [SerializeField] private Image progressFiller;

    private float fillerProgress = 0f;

    /*--- UNITY FUNCTIONS ---*/

    void Update()
    {
        // Testing if the button is being hold and handling it
        if (Input.GetKey(KeyCode.Mouse0) && IsPointerOverUIElement(backButton))
        {
            if (fillerProgress < 1)
            {
                AddProgress();
            }
        }
        else
        {
            if (fillerProgress > 0)
            {
                RemoveProgress();
            }
        }
        CheckProgress();
    }

    /*--- PRIVATE FUNCTIONS ---*/

    /// <summary>
    /// Function for checking if the "back to main menu action" should be triggered and triggering it
    /// </summary>
    private void CheckProgress()
    {
        if (fillerProgress == 1f)
        {
            TriggerGoBackAction();
        }
    }

    /// <summary>
    /// Function that handles the "back to main menu" action
    /// </summary>
    private void TriggerGoBackAction()
    {
        // Clears the game memory before going back to the main menu
        GameObject memory = GameObject.FindGameObjectWithTag("PlayerMemory");
        if (memory != null)
        {
            Destroy(memory);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /// <summary>
    /// Function for adding progress based on time
    /// </summary>
    private void AddProgress()
    {
        fillerProgress += (Time.deltaTime * loadSpeed);
        if (fillerProgress > 1f)
        {
            fillerProgress = 1f;
        }
        SetProgressFiller();
    }

    /// <summary>
    /// Function for removing progress based on time
    /// </summary>
    private void RemoveProgress()
    {
        fillerProgress -= (Time.deltaTime * loadSpeed);
        if (fillerProgress < 0f)
        {
            fillerProgress = 0f;
        }
        SetProgressFiller();
    }

    /// <summary>
    /// Function for setting the progress-ui of the filler next to the button
    /// </summary>
    private void SetProgressFiller()
    {
        progressFiller.fillAmount = fillerProgress;
    }

    /// <summary>
    /// Checks if the cursor is above the button parameter
    /// </summary>
    /// <param name="button">The button component that is being checked</param>
    /// <returns>Returns ture if the cursor is over the button, else it returns false</returns>
    private bool IsPointerOverUIElement(Button button)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject == button.gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
