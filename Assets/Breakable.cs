using UnityEngine;

public class Breakable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int hitsToBreak = 2;
    public Animator animator;

    public Collider2D collider;
    
    public Rigidbody2D rb;

    public
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the GameObject.");
        }

        collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("Collider component is missing on the GameObject.");
        }

    }


    public void TakeDamage()
    {
        hitsToBreak--;
        if (hitsToBreak <= 0)
        {
            Break();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }
    
    private void Break()
    {
        animator.SetTrigger("break");
    }
}
