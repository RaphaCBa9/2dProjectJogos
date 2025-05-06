using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int meleeDamage = 3;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) {
            EnemyBehavior e = collision.gameObject.GetComponent<EnemyBehavior>();
            e.TakeDamage(meleeDamage);
        }
    }

}
