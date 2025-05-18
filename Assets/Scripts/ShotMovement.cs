using UnityEngine;

public class ShotMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2f;
    public float lifetime = 5f;
    public int damage = 1;
    public Vector2 moveDir = new Vector2(0f, 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * moveDir.normalized);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyBase e = collision.GetComponent<EnemyBase>();
            e.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Breakable"))
        {
            Breakable b = collision.GetComponent<Breakable>();
            b.TakeDamage();
        }
        if (collision.CompareTag("Boss"))
        {
            try
            {
                skeletonBossScript b = collision.gameObject.GetComponent<skeletonBossScript>();
                b.takeDamage(damage);
            }
            catch (System.Exception ex)
            {
                Debug.Log("Error while trying to deal damage to the boss: " + ex.Message);
            }
            finally
            {
                skeletonBossScript2 b = collision.gameObject.GetComponent<skeletonBossScript2>();
                b.takeDamage(damage);
            }
        }
        
    }
}
