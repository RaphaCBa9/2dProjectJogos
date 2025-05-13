using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Dictionary<string, bool> powerUpsAtivos = new Dictionary<string, bool>() {
        {"isAutomaticShotActive", false}
    };
    
    public GameObject shotPrefab;
    public float automaticShotCooldown = 0.5f;
    private float lastAutomaticShot;

    void Start()
    {
        lastAutomaticShot = Time.time;
    }

    void Update()
    {
        if (powerUpsAtivos["isAutomaticShotActive"] && Time.time - lastAutomaticShot > automaticShotCooldown) {
            AutomaticShot();
            lastAutomaticShot = Time.time;
        }
    }

    private void AutomaticShot() {
        GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        shot.GetComponent<ShotMovement>().moveDir = new(Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
