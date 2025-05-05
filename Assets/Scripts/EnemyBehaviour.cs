using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public int maxHealth = 3;
    public float danoMelee = 5f;

    private int currentHealth;
    private Transform player;
    private float lastAttackTime;
    private AudioSource audioSource;

    private AudioClip attackSound;
    private AudioClip deathSound;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

        attackSound = Resources.Load<AudioClip>("Sounds/goblinAttack");
        deathSound = Resources.Load<AudioClip>("Sounds/goblinDeath");
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
        if (attackSound != null && audioSource != null)
            audioSource.PlayOneShot(attackSound);
        if (hit.collider != null)
        {
            Debug.Log("Raycast acertou: " + hit.collider.name);
            if (hit.collider.name == "Player")
            {
                hit.collider.GetComponent<Health>().TomarDano((int)danoMelee);

            }
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

        // Toca som de morte
        if (deathSound != null && audioSource != null)
            audioSource.PlayOneShot(deathSound);

        Destroy(gameObject, 0.5f);
    }
}
