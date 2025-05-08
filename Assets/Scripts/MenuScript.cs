using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }

    public void GoToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}