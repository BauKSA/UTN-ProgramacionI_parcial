using UnityEngine;
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(PlayerHealthStateController))]

[RequireComponent(typeof(MovementStateController))]
[RequireComponent(typeof(PositionController))]
public class PlayerEnemyBulletCollision : MonoBehaviour
{
    private AnimationController _animationController;
    private PlayerHealthStateController _healthController;
    private PositionController _positionController;
    private MovementStateController _movementStateController;

    private void Awake()
    {
        _animationController = GetComponent<AnimationController>();
        _healthController = GetComponent<PlayerHealthStateController>();
        _positionController = GetComponent<PositionController>();
        _movementStateController = GetComponent<MovementStateController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("EnemyBullet") && !collision.CompareTag("Wall") && !collision.CompareTag("EnemyShip"))
            return;

        if (collision.CompareTag("EnemyBullet")) Destroy(collision.gameObject);

        if (_healthController.Invincibility)
            return;

        _positionController.Stop();
        _positionController.ClearConstantForwardMovement();
        _movementStateController.CanMove = false;
        _movementStateController.Reset();

        _healthController.SetInvincibility(true);
        _healthController.SetAlive(false);
        _animationController.StartAnimation("PlayerShip_Damaged", true, _healthController.CheckState);
    }
}
