using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int meleeDamage = 3;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyBase e = collision.gameObject.GetComponent<EnemyBase>();
            if (e != null)
            {
                e.TakeDamage(meleeDamage);
            }
        }
        else if (collision.CompareTag("Boss"))
        {
            // Tenta causar dano em qualquer tipo de boss existente
            var boss1 = collision.gameObject.GetComponent<skeletonBossScript>();
            if (boss1 != null)
            {
                boss1.takeDamage(meleeDamage);
            }

            var boss2 = collision.gameObject.GetComponent<skeletonBossScript2>();
            if (boss2 != null)
            {
                boss2.takeDamage(meleeDamage);
            }

            var goblinBoss = collision.gameObject.GetComponent<GoblinBoss>();
            if (goblinBoss != null)
            {
                goblinBoss.takeDamage(meleeDamage);
            }
        }
        else if (collision.CompareTag("Breakable"))
        {
            Debug.Log("Hit a breakable object");
            Breakable b = collision.gameObject.GetComponent<Breakable>();
            if (b != null)
            {
                b.TakeDamage();
            }
        }
    }
}
