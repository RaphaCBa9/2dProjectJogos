using UnityEngine;

public class PlayerSkeletonAttack : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;

    void Shoot()
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        Vector2 attackDir = pm.movement;
        if (attackDir.Equals(Vector2.zero))
        {
            attackDir = pm.lastMovement;
            if (attackDir.Equals(Vector2.zero))
            {
                attackDir = new Vector2(1f, 0f);
            }
        }

        Quaternion attackRotation = Quaternion.identity;
        if (attackDir.Equals(Vector2.left))
        {
            attackRotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
        else if (attackDir.Equals(Vector2.down))
        {
            attackRotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
        }
        else if (attackDir.Equals(Vector2.up))
        {
            attackRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
        }

        GameObject arrow = Instantiate(arrowPrefab, transform.position, attackRotation);
        arrow.GetComponent<ShotMovement>().moveDir = attackDir;
    }
}
