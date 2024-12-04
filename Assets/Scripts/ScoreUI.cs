using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMesh Pro namespace

public class DeathSceneUI : MonoBehaviour
{
    public TMP_Text targetCountText; // Reference to the TextMesh Pro component

    private void Start()
    {
        // Update the text with the destroyed target count
        targetCountText.text = "Score: " + TargetManager.destroyedTargetCount;
    }
}
