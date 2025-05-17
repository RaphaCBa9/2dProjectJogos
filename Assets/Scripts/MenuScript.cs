using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void ChooseCharacter()
    {
        SceneManager.LoadSceneAsync("PlayerSelect");
    }

    public void GoToMenu()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        Destroy(gm);
        SceneManager.LoadSceneAsync(0);
    }
}