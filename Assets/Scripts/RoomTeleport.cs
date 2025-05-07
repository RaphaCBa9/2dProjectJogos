using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTeleport : MonoBehaviour
{
    private int minRandom = 4;
    private int maxRandom = 5;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            SceneManager.LoadSceneAsync(Random.Range(minRandom, maxRandom+1));
        }
    }
}
