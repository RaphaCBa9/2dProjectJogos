using System.Collections;
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
            // for (int i = 1; i < SceneManager.sceneCount; i++) {
            //     // Debug.Log(SceneManager.GetSceneAt(i).name);
            //     SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
            // }
            // SceneManager.LoadSceneAsync(Random.Range(minRandom, maxRandom+1), LoadSceneMode.Additive);
            Debug.Log("Encostou no teleporte");
            StartCoroutine(LoadScene());
            // collision.attachedRigidbody.MovePosition(new(collision.transform.position.x, -collision.transform.position.y));
            collision.transform.position = new(0f, 4f, 0f);
            Debug.Log("Teleportou");
            // SceneManager.LoadSceneAsync(Random.Range(minRandom, maxRandom+1));
        }
    }

    private IEnumerator LoadScene() {
        Debug.Log("Comecou carregar");
        AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).name);
        AsyncOperation load = SceneManager.LoadSceneAsync(Random.Range(minRandom, maxRandom+1), LoadSceneMode.Additive);
        while (!unload.isDone || !load.isDone) {
            yield return null;
        }
        Debug.Log("Terminou carregar");
    }
}
