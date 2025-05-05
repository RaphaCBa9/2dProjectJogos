using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float maxSpeed = 2f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public int maxHealth = 3;
    public float meleeDamage = 5f;

    private float currentSpeed;
    private int currentHealth;
    private Transform player;
    private float lastAttackTime;
    private AudioSource audioSource;

    private AudioClip attackSound;
    private AudioClip deathSound;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 direction;
    private Vector2 lastMovement;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        currentHealth = maxHealth;
        currentSpeed = maxSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            transform.position += (Vector3) (currentSpeed * Time.deltaTime * direction);

            anim.SetFloat("moveX", direction.x);
            anim.SetFloat("moveY", direction.y);
            anim.SetFloat("moveMagnitude", direction.magnitude);
            // anim.SetFloat("lastMoveX", lastMovement.x);
            // anim.SetFloat("lastMoveY", lastMovement.y);
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
        currentSpeed = 0f;
        anim.SetBool("attack", true);
        Vector2 direction = (player.position - transform.position).normalized;
        int layerMask = ~LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange, layerMask);
        if (attackSound != null && audioSource != null)
            audioSource.PlayOneShot(attackSound);
        if (hit.collider != null)
        {
            Debug.Log("Raycast acertou: " + hit.collider.name);
            if (hit.collider.name == "Player") {
                hit.collider.GetComponent<Health>().TomarDano(meleeDamage);
            }
        }
        else
        {
            Debug.Log("Raycast não acertou nada");
        }
    }

    public void EndAttackAnimation() {
        anim.SetBool("attack", false);
        currentSpeed = maxSpeed;
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
