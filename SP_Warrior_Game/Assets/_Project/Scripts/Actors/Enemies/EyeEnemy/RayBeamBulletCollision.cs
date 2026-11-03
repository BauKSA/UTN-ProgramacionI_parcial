using UnityEngine;

public class RayBeamBulletCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerBullet"))
            return;

        Destroy(collision.gameObject);
    }
}
