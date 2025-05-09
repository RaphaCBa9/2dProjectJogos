using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTeleport : MonoBehaviour
{
    [SerializeField] private int direction;
    private Vector3 posititionToTeleport;

    void Awake()
    {
        Random.InitState((int) Time.time);
    }

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
            collision.transform.position = posititionToTeleport;
            Debug.Log("Teleportou");
            // SceneManager.LoadSceneAsync(Random.Range(minRandom, maxRandom+1));
        }
    }

    private IEnumerator LoadScene() {
        Debug.Log("Comecou carregar");

        GameObject roomManager = GameObject.FindGameObjectWithTag("RoomManager");
        RoomManager rm = roomManager.GetComponent<RoomManager>();
        List<string> rooms = rm.roomsList;

        string currentRoom = SceneManager.GetSceneAt(1).name;
        Debug.Log(currentRoom);
        string roomToLoad;
        if (rm.rooms.ContainsKey(currentRoom)) {
            if (rm.rooms[currentRoom].ContainsKey(direction)) {
                roomToLoad = rm.rooms[currentRoom][direction];
            } else {
                roomToLoad = rooms[Random.Range(0, rooms.Count + 1)];
                rm.AddRoom(currentRoom, direction, roomToLoad);
                rooms.Remove(roomToLoad);
            }
        } else {
            roomToLoad = rooms[Random.Range(0, rooms.Count + 1)];
            rm.AddRoom(currentRoom, direction, roomToLoad);
            rooms.Remove(roomToLoad);
        }

        if (direction == 0) {
            posititionToTeleport = new Vector3(0f, -3.5f, 0f);
        } else if (direction == 1) {
            posititionToTeleport = new Vector3(-7f, 0f, 0f);
        } else if (direction == 2) {
            posititionToTeleport = new Vector3(0f, 3.5f, 0f);
        } else if (direction == 3) {
            posititionToTeleport = new Vector3(7f, 0f, 0f);
        }

        AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).name);
        AsyncOperation load = SceneManager.LoadSceneAsync(roomToLoad, LoadSceneMode.Additive);
        while (!unload.isDone || !load.isDone) {
            yield return null;
        }
        Debug.Log("Terminou carregar");
    }
}
