using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private PowerUp pu;
    public List<string> possibleCollectables = new List<string> {
        "isAutomaticShotActive",
    };
    public int coletavelIndex = 0;

    void Awake()
    {
        pu = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUp>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            pu.powerUpsAtivos[possibleCollectables[coletavelIndex]] = true;
            Destroy(gameObject);
        }
    }
}
