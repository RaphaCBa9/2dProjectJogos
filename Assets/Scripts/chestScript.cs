using UnityEngine;

public class chestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public BoxCollider2D collider;
    public Rigidbody2D rb;
    public Animator animator;

    public CircleCollider2D interactionRange;

    public bool canInteract = false;

    public AudioSource openSound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found in the scene.");
        }
        collider = GetComponent<BoxCollider2D>();

        interactionRange = GetComponent<CircleCollider2D>();
        interactionRange.isTrigger = true;

        rb = GetComponent<Rigidbody2D>();

        openSound = GetComponent<AudioSource>();
        if (openSound == null)
        {
            Debug.LogError("Open sound not assigned.");
        }


        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            Debug.Log("Player is in range to interact with the chest.");
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (openSound != null)
                {
                    openSound.Play();
                }
                else
                {
                    Debug.LogError("Open sound not assigned.");
                }
                Debug.Log("Player interacted with the chest.");
                animator.SetTrigger("open");

                // Disable the collider to prevent further interactions
                interactionRange.enabled = false;
            }
        }

    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            Debug.Log("Player is out of range to interact with the chest.");
        }
    }
}
