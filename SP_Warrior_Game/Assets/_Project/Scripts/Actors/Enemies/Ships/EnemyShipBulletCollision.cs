using UnityEngine;
[RequireComponent(typeof(PositionController))]

[RequireComponent(typeof(AnimationController))]
public class EnemyShipBulletCollision : MonoBehaviour
{
    private PositionController _positionController;
    private AnimationController _animationController;

    [SerializeField]
    private char _shipType;

    private void Awake()
    {
        _positionController = GetComponent<PositionController>();
        _animationController = GetComponent<AnimationController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerBullet") && !collision.CompareTag("Wall") && !collision.CompareTag("Player"))
            return;

        if (collision.CompareTag("PlayerBullet")) Destroy(collision.gameObject);

        _positionController.ClearConstantForwardMovement();
        _positionController.Stop();
        
        _animationController.StartAnimation($"EnemyShip{_shipType}_Damaged", true, () => Destroy(gameObject));

    }
}
