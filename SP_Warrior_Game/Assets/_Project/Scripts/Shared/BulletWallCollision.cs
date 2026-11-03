using UnityEngine;

public class BulletWallCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Wall")) return;
        Destroy(gameObject);
    }
}
