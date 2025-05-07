using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTeleport : MonoBehaviour
{
    private int minRandom = 4;
    private int maxRandom = 5;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            // collision.transform.position = new(0f, 4f, 0f);
            // SceneManager.LoadSceneAsync(2);
            for (int i = 1; i < SceneManager.sceneCount; i++) {
                // Debug.Log(SceneManager.GetSceneAt(i).name);
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
            }
            SceneManager.LoadSceneAsync(Random.Range(minRandom, maxRandom+1), LoadSceneMode.Additive);
            // SceneManager.LoadSceneAsync(Random.Range(minRandom, maxRandom+1));
        }
    }
}
