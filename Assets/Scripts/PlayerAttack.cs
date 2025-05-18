using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int meleeDamage = 3;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyBase e = collision.gameObject.GetComponent<EnemyBase>();
            e.TakeDamage(meleeDamage);
        }
        if (collision.CompareTag("Boss"))
        {
            try
            {
                skeletonBossScript b = collision.gameObject.GetComponent<skeletonBossScript>();
                b.takeDamage(meleeDamage);
            }
            catch (System.Exception ex)
            {
                Debug.Log("Error while trying to deal damage to the boss: " + ex.Message);
            }
            finally
            {
                skeletonBossScript2 b = collision.gameObject.GetComponent<skeletonBossScript2>();
                b.takeDamage(meleeDamage);
            }
        }
        if (collision.CompareTag("Breakable"))
        {
            Debug.Log("Hit a breakable object");
            Breakable b = collision.gameObject.GetComponent<Breakable>();
            b.TakeDamage();
        }
    }

}
