using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int meleeDamage = 3;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) {
            EnemyBase e = collision.gameObject.GetComponent<EnemyBase>();
            e.TakeDamage(meleeDamage);
        }
    }

}
