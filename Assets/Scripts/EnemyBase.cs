using UnityEngine;
using UnityEngine.SceneManagement;

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
    protected bool isDead = false;


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
        if (isDead == true)
        {
            anim.SetFloat("moveX", 0f);
            anim.SetFloat("moveY", 0f);
            anim.SetFloat("moveMagnitude", 0f);
        }

        if (distance > attackRange)
        {
            isAttacking = false;
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(currentSpeed * Time.deltaTime * direction);

            if (!isAttacking)
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
                // isAttacking = true;
                lockedAttackDirection = (player.position - transform.position).normalized;
                lockedAnimDirection = lockedAttackDirection;

                anim.SetFloat("moveX", lockedAnimDirection.x);
                anim.SetFloat("moveY", lockedAnimDirection.y);
                anim.SetFloat("moveMagnitude", lockedAnimDirection.magnitude);



                anim.SetTrigger("attack");
                currentSpeed = 0f;
                lastAttackTime = Time.time;
            }
        }
    }

    public void AttackCaller()
    {   
        if (isAttacking) return;
        isAttacking=true;
        Debug.Log($"AttackCaller chamado por {gameObject.name} em {Time.time}");
        StartCoroutine(PerformDelayedAttack());
    }


    public virtual void EndAttackAnimation()
    {
        currentSpeed = maxSpeed;
        isAttacking = false;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // Interrompe a anima��o de ataque, se estiver em execu��o
        anim.ResetTrigger("attack");
        isAttacking = false;
        currentSpeed = maxSpeed;

        // Toca a anima��o de dano imediatamente
        anim.SetTrigger("takeDamage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


protected virtual void Die()
{
    if (isDead) return;

    isDead = true;

    anim.SetFloat("moveX", 0f);
    anim.SetFloat("moveY", 0f);
    anim.SetFloat("moveMagnitude", 0f);
    anim.SetBool("isDead", true);

    // Desabilita colisores
    Collider2D[] colliders = GetComponents<Collider2D>();
    foreach (var col in colliders)
    {
        col.enabled = false;
    }

    // Congela f�sica
    if (rb != null)
    {
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
    }

    GameObject roomManager = GameObject.FindGameObjectWithTag("RoomManager");
    RoomManager rm = roomManager.GetComponent<RoomManager>();
    string currentRoom = SceneManager.GetSceneAt(1).name;
    rm.roomObjects[currentRoom].Add(gameObject.name, false);

    Destroy(gameObject, 1f); // Destroi ap�s 1 segundo (anima��o de morte)
}

    protected abstract System.Collections.IEnumerator PerformDelayedAttack();
}
