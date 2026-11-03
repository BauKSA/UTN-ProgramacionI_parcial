using UnityEngine;
[RequireComponent(typeof(AnimationController))]

[RequireComponent(typeof(EyeEnemyStateController))]
public class EyeBulletCollision : MonoBehaviour
{
    private AnimationController _animationController;
    private EyeEnemyStateController _stateController;

    private void Awake()
    {
        _animationController = GetComponent<AnimationController>();
        _stateController = GetComponent<EyeEnemyStateController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerBullet"))
            return;

        Destroy(collision.gameObject);

        if (!_stateController.EyeOpened)
            return;

        _stateController.SetBeingDamaged(true);
        _animationController.StartAnimation("EyeEnemy_Damaged", true,
            () => _stateController.SetBeingDamaged(false));
    }
}
