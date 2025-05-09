using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // 0: norte
    // 1: leste
    // 2: sul
    // 3: oeste
    public Dictionary<string, Dictionary<int, string>> rooms = new Dictionary<string, Dictionary<int, string>>();
    public List<string> roomsList = new List<string>() {"room01", "room2"};

    // void Awake()
    // {
    //     rooms = new Dictionary<string, Dictionary<int, string>>() {
    //         {"inicial", new Dictionary<int, string>() {{2, null}}}
    //     };
    // }

    // void Update()
    // {
    //     Debug.Log(GameObject.FindGameObjectsWithTag("TeleportZone"));
    // }

    public void AddRoom(string currentRoom, int position, string nextRoom) {
        if (rooms.ContainsKey(currentRoom)) {
            rooms[currentRoom].Add(position, nextRoom);
        } else {
            rooms.Add(currentRoom, new Dictionary<int, string>() {{position, nextRoom}});
        }
        
        if (rooms.ContainsKey(nextRoom)) {
            rooms[nextRoom].Add((position + 2) % 4, currentRoom);
        } else {
            rooms.Add(nextRoom, new Dictionary<int, string>() {{(position + 2) % 4, currentRoom}});
        }
    }
}
