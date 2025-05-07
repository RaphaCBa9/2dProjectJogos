using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float maxSpeed = 2f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public int maxHealth = 3;
    public float meleeDamage = 5f;

    protected float currentSpeed;
    protected int currentHealth;
    protected Transform player;
    protected float lastAttackTime;
    protected AudioSource audioSource;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected Vector2 lockedAttackDirection;
    protected Vector2 lockedAnimDirection;

    protected bool isAttacking = false;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        currentHealth = maxHealth;
        currentSpeed = maxSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            isAttacking = false;
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(currentSpeed * Time.deltaTime * direction);

            if (!anim.GetBool("attack"))
            {
                anim.SetFloat("moveX", direction.x);
                anim.SetFloat("moveY", direction.y);
                anim.SetFloat("moveMagnitude", direction.magnitude);
            }
        }
        else
        {
            if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
            {
                isAttacking = true;
                lockedAttackDirection = (player.position - transform.position).normalized;
                lockedAnimDirection = lockedAttackDirection;

                anim.SetFloat("moveX", lockedAnimDirection.x);
                anim.SetFloat("moveY", lockedAnimDirection.y);
                anim.SetFloat("moveMagnitude", lockedAnimDirection.magnitude);

                anim.SetBool("attack", true);
                currentSpeed = 0f;

                StartCoroutine(PerformDelayedAttack(GetAttackDelay()));
                lastAttackTime = Time.time;
            }
        }
    }

    public virtual void EndAttackAnimation()
    {
        anim.SetBool("attack", false);
        currentSpeed = maxSpeed;
        isAttacking = false;
        Debug.Log("EndAttackAnimation chamado");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        anim.SetTrigger("takeDamage");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject, 1f);
        anim.SetBool("isDead", true);
    }

    protected abstract float GetAttackDelay();
    protected abstract System.Collections.IEnumerator PerformDelayedAttack(float delay);
}
