using UnityEngine;

public class skeletonBossScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created]

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
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        distanceFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);

        if (distanceFromPlayer < 50f)
        {
            if (attackTimer <= 0.0f)
            {

                if (distanceFromPlayer < 3f)
                {
                    attackType = chooseAttack();
                    attackTimer = attackCooldown;
                }
                else
                {
                    attackType = 2;
                    attackTimer = attackCooldown;
                }




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

                // Reset attack type after attacking
                attackType = -1;

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
        speedMagnitude = Mathf.Sqrt(xSpeed * xSpeed + ySpeed * ySpeed);


        this.transform.position += new Vector3(xSpeed, ySpeed, 0) * speed * Time.deltaTime;
    }

    private void updateAnimation()
    {
        animator.SetFloat("moveX", xSpeed);
        animator.SetFloat("moveY", ySpeed);
        animator.SetFloat("moveMagnitude", speedMagnitude);
    }

    public void takeDamage(int damage)
    {
        Debug.Log("Boss took damage: " + damage);
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            Destroy(gameObject, 5.0f); // Destroy the object after 2 seconds
        }
        else
        {
            animator.SetTrigger("takeDamage");
        }
    }   

    private int chooseAttack()
    {
        // choose seed based on time
        System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
        int attackType = Random.Range(0, 2); // Randomly choose an attack type (0, 1, or 2)
        return attackType;
    }

}
