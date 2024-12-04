using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadSceneAsync(1);
        GameManager.Instance.RestartGame();

    }
    public void QuitGame(){
        Application.Quit();
    }

}
