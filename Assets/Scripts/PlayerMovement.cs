using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float speed;
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

        anim.SetFloat("speedY", moveVertical);

        Vector2 movement = new(moveHorizontal, moveVertical);

        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement.normalized);
    }
}
