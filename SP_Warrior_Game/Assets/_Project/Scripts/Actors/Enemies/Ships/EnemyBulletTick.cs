using UnityEngine;

[RequireComponent(typeof(PositionController))]
[RequireComponent(typeof(MovementSettings))]
public class EnemyBulletTick : MonoBehaviour
{
    private PositionController _positionController;
    private MovementSettings _movementSettings;

    void Awake()
    {
        _positionController = GetComponent<PositionController>();
        _movementSettings = GetComponent<MovementSettings>();
    }

    private void Start()
    {
        _positionController.SetConstantForwardMovement(_movementSettings.Speed.x);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
