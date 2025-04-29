using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private Vector2 lastMovement;
    [SerializeField] private float speed;
    public GameObject coinPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        if (moveHorizontal == 0 && moveVertical == 0 && movement.x != 0 || movement.y != 0) {
            lastMovement = movement;
        }

        movement = new(moveHorizontal, moveVertical);

        anim.SetFloat("speedX", movement.x);
        anim.SetFloat("speedY", movement.y);
        anim.SetFloat("moveMagnitude", movement.magnitude);
        anim.SetFloat("lastMoveX", lastMovement.x);
        anim.SetFloat("lastMoveY", lastMovement.y);

        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement.normalized);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
