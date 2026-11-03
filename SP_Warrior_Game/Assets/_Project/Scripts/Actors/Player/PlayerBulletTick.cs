using UnityEngine;
[RequireComponent(typeof(PositionController))]

[RequireComponent(typeof(MovementSettings))]
public class PlayerBulletTick : MonoBehaviour
{
    private PositionController _positionController;
    private MovementSettings _movementSettings;

    private void Awake()
    {
        _positionController = GetComponent<PositionController>();
        _movementSettings = GetComponent<MovementSettings>();
    }

    private void Update()
    {
        _positionController.MoveForward(_movementSettings.Speed.x, _movementSettings.Acceleration);
    }
}
