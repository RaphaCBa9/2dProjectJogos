using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerWizardPrefab;
    [SerializeField] private GameObject playerSkeletonPrefab;
    public GameObject playerPrefabSelected;

    [SerializeField] private AnimatorController playerWizardDeathAnimator;
    [SerializeField] private AnimatorController playerSkeletonDeathAnimator;
    public AnimatorController playerDeathAnimatorSelected;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SelectWizard()
    {
        playerPrefabSelected = playerWizardPrefab;
        playerDeathAnimatorSelected = playerWizardDeathAnimator;
        PlayGame();
    }

    public void SelectSkeleton()
    {
        playerPrefabSelected = playerSkeletonPrefab;
        playerDeathAnimatorSelected = playerSkeletonDeathAnimator;
        PlayGame();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Player");
        SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Additive);
    }

}
