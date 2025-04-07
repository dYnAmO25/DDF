using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for background color effects
/// </summary>
public class ChromaEffect : MonoBehaviour
{
    [SerializeField] private bool isOn;
    [SerializeField] private Image background;
    [SerializeField] private float speed;
    [SerializeField] private float hue = 0.5f;

    /*--- UNITY FUNCTIONS ---*/

    private void Update()
    {
        if (isOn)
        {
            background.color = Color.HSVToRGB(hue, 1, 1);
            UpdateColor();
        }
        else
        {
            background.color = Color.HSVToRGB(hue, 1, 1);
        }
    }

    /// <summary>
    /// Function for updating the hue of the background
    /// </summary>
    private void UpdateColor()
    {
        if (hue >= 1)
        {
            hue = 0;
        }
        hue += speed;
    }

    /*--- PUBLIC FUNCTION ---*/

    /// <summary>
    /// Function for setting the background hue
    /// </summary>
    /// <param name="hue">A float that represents the hue (h) of a HSV color</param>
    public void SetHue(float hue)
    {
        this.hue = hue;
    }
}
