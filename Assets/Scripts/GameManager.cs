using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerWizardPrefab;
    [SerializeField] private GameObject playerSkeletonPrefab;
    public GameObject playerPrefabSelected;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SelectWizard()
    {
        playerPrefabSelected = playerWizardPrefab;
        PlayGame();
    }

    public void SelectSkeleton()
    {
        playerPrefabSelected = playerSkeletonPrefab;
        PlayGame();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }

}
