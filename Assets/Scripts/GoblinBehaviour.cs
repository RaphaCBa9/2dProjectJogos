using UnityEngine;

public class GoblinBehavior : EnemyBase
{
    private AudioClip attackSound;
    private AudioClip deathSound;

    protected override void Start()
    {
        base.Start();
        attackSound = Resources.Load<AudioClip>("Sounds/goblinAttack");
        deathSound = Resources.Load<AudioClip>("Sounds/goblinDeath");
    }

    protected override float GetAttackDelay()
    {
        return 0f;
    }

    protected override System.Collections.IEnumerator PerformDelayedAttack(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (attackSound != null && audioSource != null)
            audioSource.PlayOneShot(attackSound);

        int layerMask = ~LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lockedAttackDirection, attackRange, layerMask);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            hit.collider.GetComponent<Health>().TomarDano(meleeDamage);
        }
    }

    protected override void Die()
    {
        if (deathSound != null && audioSource != null)
            audioSource.PlayOneShot(deathSound);

        base.Die();
    }

    new public void EndAttackAnimation()
    {
        base.EndAttackAnimation();
    }
}
