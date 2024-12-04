using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Makes the GameManager persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    #endregion

    public int currentScore = 0; // Current score
    public bool isPlaying = true;
    private int lastScoreCheckpoint = 0; // Track the last score multiple of 10
    public BackgroundControl_0 backgroundControl; // Reference to BackgroundControl_0 script

    private void Update()
    {
        // Check if the score is a multiple of 10 and greater than the last checkpoint
        int scoreCheckpoint = currentScore / 10;

        // Change background if score reaches the next multiple of 10
        if (scoreCheckpoint > lastScoreCheckpoint)
        {
            lastScoreCheckpoint = scoreCheckpoint; // Update the checkpoint
            backgroundControl.NextBG(); // Call the method to change the background
            Debug.Log("Score reached: " + currentScore + ", changing background.");
        }
    }

    public void RestartGame()
    {
        currentScore = 0; // Reset score
        isPlaying = true; // Set the game state back to playing
    }

    public void GameOver()
    {
        isPlaying = false;
        SceneManager.LoadSceneAsync(2); // Load the game over scene
    }

    public void IncreaseScore()
    {
        currentScore++; // Increase score by 1
    }

    public string PrettyScore()
    {
        return currentScore.ToString(); // Return score as string
    }
}
