using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private Vector2 lastMovement;
    [SerializeField] public float maxSpeed = 5f;
    public float speed;
    public GameObject coinPrefab;

    private GameObject roomManager;

    [SerializeField] private GameObject attackHitBox;
    [SerializeField] private float attackHitBoxOffset = 1f;
    private float lastMeleeAtack;
    private float meleeAttackCooldown = 0.5f;

    void Awake()
    {
        roomManager = GameObject.FindGameObjectWithTag("RoomManager");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = maxSpeed;
        lastMeleeAtack = Time.time;
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

        float attack = Input.GetAxisRaw("Fire1");
        if (attack > 0 && Time.time - lastMeleeAtack > meleeAttackCooldown) {
            lastMeleeAtack = Time.time;
            Attack();
        }
    }

    void Attack() {
        anim.SetTrigger("attack");
    }

    void EnableAttackHitBox() {
        speed = 0f;
        Vector2 offset = new(0f, 0f);

        if (Math.Abs(movement.magnitude) > 0) {
            if (movement.x > 0) {
                offset = new(attackHitBoxOffset, 0f);
                attackHitBox.transform.localScale = (Vector3) new(1, 2);
            } else if (movement.x < 0) {
                offset = new(-attackHitBoxOffset, 0f);
                attackHitBox.transform.localScale = (Vector3) new(1, 2);
            } else if (movement.y > 0) {
                offset = new(0f, attackHitBoxOffset);
                attackHitBox.transform.localScale = (Vector3) new(2, 1);
            } else {
                offset = new(0f, -attackHitBoxOffset);
                attackHitBox.transform.localScale = (Vector3) new(2, 1);
            }
        } else {
            if (lastMovement.x > 0) {
                offset = new(attackHitBoxOffset, 0f);
                attackHitBox.transform.localScale = (Vector3) new(1, 2);
            } else if (lastMovement.x < 0) {
                offset = new(-attackHitBoxOffset, 0f);
                attackHitBox.transform.localScale = (Vector3) new(1, 2);
            } else if (lastMovement.y > 0) {
                offset = new(0f, attackHitBoxOffset);
                attackHitBox.transform.localScale = (Vector3) new(2, 1);
            } else {
                offset = new(0f, -attackHitBoxOffset);
                attackHitBox.transform.localScale = (Vector3) new(2, 1);
            }
        }

        attackHitBox.transform.position = (Vector2) transform.position + offset;
        attackHitBox.SetActive(true);
    }

    void DisableAttackHitBox() {
        attackHitBox.SetActive(false);
        speed = maxSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            maxSpeed += 1f;
            speed = maxSpeed;

            RoomManager rm = roomManager.GetComponent<RoomManager>();
            string currentRoom = SceneManager.GetSceneAt(1).name;
            rm.roomObjects[currentRoom].Add(other.gameObject.name, false);

            Destroy(other.gameObject);
        }
    }
}
