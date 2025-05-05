using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public int maxHealth = 3;

    private int currentHealth;
    private Transform player;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
        else
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        int layerMask = ~LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange, layerMask);

        if (hit.collider != null)
        {
            Debug.Log("Raycast acertou: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Raycast não acertou nada");
        }
    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Inimigo morreu!");
        Destroy(gameObject);
    }
}
