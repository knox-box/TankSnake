using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl_0 : MonoBehaviour
{
    [Header("BackgroundNum 0 -> 3")]
    public int backgroundNum; // Current background number
    public Sprite[] Layer_Sprites; // Sprites for the layers
    private GameObject[] Layer_Object = new GameObject[5]; // Array to hold Layer objects
    private int max_backgroundNum = 3; // Max background number (0-3)

    void Start()
    {
        // Initialize all Layer Objects
        for (int i = 0; i < Layer_Object.Length; i++)
        {
            Layer_Object[i] = GameObject.Find("Layer_" + i);
        }

        ChangeSprite(); // Set initial sprite based on the current backgroundNum
    }

    void ChangeSprite()
    {
        // Update the sprite of each layer based on the backgroundNum
        Layer_Object[0].GetComponent<SpriteRenderer>().sprite = Layer_Sprites[backgroundNum * 5];
        for (int i = 1; i < Layer_Object.Length; i++)
        {
            Sprite changeSprite = Layer_Sprites[backgroundNum * 5 + i];
            // Change Layer_1->7
            Layer_Object[i].GetComponent<SpriteRenderer>().sprite = changeSprite;
            // Change child sprites of Layer_1->7
            Layer_Object[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = changeSprite;
            Layer_Object[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = changeSprite;
        }
    }

    public void NextBG()
    {
        backgroundNum = backgroundNum + 1;
        if (backgroundNum > max_backgroundNum) backgroundNum = 0; // Reset to first background if max is exceeded
        ChangeSprite(); // Apply the new background sprite
    }

    public void BackBG()
    {
        backgroundNum = backgroundNum - 1;
        if (backgroundNum < 0) backgroundNum = max_backgroundNum; // Go to the max background if less than 0
        ChangeSprite(); // Apply the new background sprite
    }

    // Coroutine to change the background automatically every 10 seconds

}
