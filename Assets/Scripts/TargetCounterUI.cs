using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetCounterUI : MonoBehaviour
{
    public TargetManager targetManager; // Reference to the TargetManager
    public TextMeshProUGUI targetCounterText; // Uncomment if using TextMeshPro

    private void Start()
    {
        // Initialize text for testing
        targetCounterText.text = "Targets Destroyed: 0";
    }

    private void Update()
    {
        // Update the UI text with the current destroyed target count
        if (targetManager != null)
        {
            targetCounterText.text = "Targets Destroyed: " + targetManager.GetDestroyedTargetCount();
        }
    }
}
