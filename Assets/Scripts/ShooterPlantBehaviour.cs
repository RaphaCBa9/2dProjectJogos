using UnityEngine;

public class ShooterPlantBehaviour : EnemyBase
{
    public GameObject projectilePrefab;
    public float shootSpeed = 10f;
    public float retreatRange = 6f; // distância mínima para começar a recuar

    private AudioClip shootSound;

    protected override void Start()
    {
        base.Start();
        shootSound = Resources.Load<AudioClip>("Sounds/plantShoot");
    }

    protected override void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Recuar se o jogador estiver muito perto
        if (distance < retreatRange)
        {
            Vector2 retreatDir = -directionToPlayer;
            transform.position += (Vector3)(currentSpeed * Time.deltaTime * retreatDir);

            anim.SetFloat("moveX", retreatDir.x);
            anim.SetFloat("moveY", retreatDir.y);
            anim.SetFloat("moveMagnitude", retreatDir.magnitude);
        }
        else
        {
            // Parar de se mover
            anim.SetFloat("moveMagnitude", 0f);
        }

        // Atirar se não estiver atacando e cooldown estiver disponível
        if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
        {
            isAttacking = true;
            lockedAttackDirection = directionToPlayer;
            lockedAnimDirection = directionToPlayer;

            anim.SetFloat("moveX", lockedAnimDirection.x);
            anim.SetFloat("moveY", lockedAnimDirection.y);
            anim.SetBool("attack", true);
            currentSpeed = 0f;

            StartCoroutine(PerformDelayedAttack(GetAttackDelay()));
            lastAttackTime = Time.time;
        }
    }

    protected override float GetAttackDelay()
    {
        return 0.3f; // pequeno atraso antes de atirar, pode ajustar
    }

    protected override System.Collections.IEnumerator PerformDelayedAttack(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (shootSound != null && audioSource != null)
            audioSource.PlayOneShot(shootSound);

        if (projectilePrefab != null)
        {
            // Criar o projétil com um pequeno offset à frente da planta
            Vector2 shootPosition = (Vector2)transform.position + lockedAttackDirection * 0.5f;
            GameObject proj = Instantiate(projectilePrefab, shootPosition, Quaternion.identity);

            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f; // garantir que não caia
                rb.linearVelocity = lockedAttackDirection * shootSpeed;
            }
        }

        EndAttackAnimation(); // opcional: já finaliza após disparar
    }

    public void EndAttackAnimation()
    {
        base.EndAttackAnimation();
    }
}
