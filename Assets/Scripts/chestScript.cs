using System.Collections.Generic;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public BoxCollider2D collider;
    public Rigidbody2D rb;
    public Animator animator;

    public GameObject eKeyHover;

    public CircleCollider2D interactionRange;

    public bool canInteract = false;

    public AudioSource openSound;

    [SerializeField] private GameObject powerUpPrefab;

    public List<Sprite> possibleCollectablesImages;
    [SerializeField] private Sprite spriteCollectable1;
    [SerializeField] private Sprite spriteCollectable2;
    [SerializeField] private Sprite spriteCollectable3;
    [SerializeField] private Sprite spriteCollectable4;
    [SerializeField] private Sprite spriteCollectable5;

    private bool canOpen = false;

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

        possibleCollectablesImages = new List<Sprite>()
        {
            spriteCollectable1,
            spriteCollectable2,
            spriteCollectable3,
            spriteCollectable4,
            spriteCollectable5,
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen)
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

                // Disable the eKeyHover game object
                if (eKeyHover != null)
                {
                    eKeyHover.SetActive(false);
                    Destroy(eKeyHover);
                }
                else
                {
                    Debug.LogError("eKeyHover object not found in the scene.");
                }

                GameObject powerUp = Instantiate(powerUpPrefab, new Vector2(transform.position.x, transform.position.y - 1f), Quaternion.identity);
                Collectable powerUpScript = powerUp.GetComponent<Collectable>();
                powerUpScript.coletavelIndex = Random.Range(0, powerUpScript.possibleCollectables.Count);
                powerUp.GetComponent<SpriteRenderer>().sprite = possibleCollectablesImages[powerUpScript.coletavelIndex];
                if (powerUpScript.coletavelIndex == 4)
                {
                    powerUp.GetComponent<SpriteRenderer>().color = new Color(0f, 205f, 253f, 255f);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
            canInteract = true;
            Debug.Log("Player is in range to interact with the chest.");

            //enalble the "eKeyHover" game object
            if (eKeyHover != null)
            {
                eKeyHover.SetActive(true);
            }
            else
            {
                Debug.LogError("eKeyHover object not found in the scene.");
            }
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

                // Disable the eKeyHover game object
                if (eKeyHover != null)
                {
                    eKeyHover.SetActive(false);
                    Destroy(eKeyHover);
                }
                else
                {
                    Debug.LogError("eKeyHover object not found in the scene.");
                }

                GameObject powerUp = Instantiate(powerUpPrefab, new Vector2(transform.position.x, transform.position.y - 1f), Quaternion.identity);
                Collectable powerUpScript = powerUp.GetComponent<Collectable>();
                powerUpScript.coletavelIndex = Random.Range(0, powerUpScript.possibleCollectables.Count);
                powerUp.GetComponent<SpriteRenderer>().sprite = possibleCollectablesImages[powerUpScript.coletavelIndex];
                if (powerUpScript.coletavelIndex == 4)
                {
                    powerUp.GetComponent<SpriteRenderer>().color = new Color(0f, 205f, 253f, 255f);
                }
            }
        }

    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
            canInteract = false;
            Debug.Log("Player is out of range to interact with the chest.");
            // Disable the eKeyHover game object
            if (eKeyHover != null)
            {
                eKeyHover.SetActive(false);
            }
            else
            {
                Debug.LogError("eKeyHover object not found in the scene.");
            }
        }
    }
}
