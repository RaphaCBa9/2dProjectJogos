using UnityEngine;

public class skeletonBossScript2 : MonoBehaviour
{
    private float attackCooldown = 3.0f;
    private float attackTimer = 0.0f;

    private Animator animator;
    public GameObject player;
    public float speed = 2.0f;

    public float xSpeed = 0.0f;
    public float ySpeed = 0.0f;
    public float speedMagnitude = 0.0f;

    public int attackType = -1;

    public int health = 20;
    public int maxHealth = 20;

    private bool isDead = false;

    public float distanceFromPlayer = 1000f;

    private Collider2D bossCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        bossCollider = GetComponent<Collider2D>();

        if (player == null)
        {
            Debug.LogError("Player not found in the scene.");
        }
        if (bossCollider == null)
        {
            Debug.LogError("Boss Collider2D not found.");
        }
    }

    void Update()
    {
        if (isDead) return;

        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceFromPlayer < 7f)
        {
            if (attackTimer <= 0.0f)
            {
                attackType = chooseAttack();
                attackTimer = attackCooldown;

                if (attackType == 0)
                {
                    animator.SetTrigger("attack1");
                }
                else if (attackType == 1)
                {
                    animator.SetTrigger("attack2");
                }
                else if (attackType == 2)
                {
                    animator.SetTrigger("attackSpecial");
                }

                attackType = -1;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }

            followPlayer();
            updateAnimation();
        }
        else
        {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 0);
            animator.SetFloat("moveMagnitude", 0);
        }
    }

    private void followPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        xSpeed = direction.x;
        ySpeed = direction.y;
        speedMagnitude = direction.magnitude;

        transform.position += direction * speed * Time.deltaTime;
    }

    private void updateAnimation()
    {
        animator.SetFloat("moveX", xSpeed);
        animator.SetFloat("moveY", ySpeed);
        animator.SetFloat("moveMagnitude", speedMagnitude);
    }

    public void takeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        Debug.Log("Boss took damage: " + damage);

        if (health <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            Destroy(gameObject, 5.0f);
        }
        else
        {
            animator.SetTrigger("takeDamage");
        }
    }

    private int chooseAttack()
    {
        if (distanceFromPlayer < 3f)
        {
            return Random.Range(0, 2); // attack1 ou attack2
        }
        else
        {
            return 2; // attackSpecial
        }
    }

    // Animation Events — chame dentro das animações
    public void DisableCollider()
    {
        if (bossCollider != null)
        {
            bossCollider.enabled = false;
        }
    }

    public void EnableCollider()
    {
        if (bossCollider != null)
        {
            bossCollider.enabled = true;
        }
    }
}
