using UnityEngine;
[RequireComponent(
    typeof(MovementStateController),
    typeof(PositionController)
)]

[RequireComponent(typeof(RotationStateController))]
[RequireComponent(typeof(RotationController))]
[RequireComponent(typeof(MovementSettings))]
[RequireComponent(typeof(AnimationController))]
public class PlayerTick : MonoBehaviour
{
    private RotationStateController _rotationState;
    private RotationController _rotationController;
    private PositionController _positionController;
    private MovementStateController _movementState;
    private MovementSettings _movementSettings;
    private AnimationController _animationController;

    void Awake()
    {
        _rotationState = GetComponent<RotationStateController>();
        _rotationController = GetComponent<RotationController>();
        _positionController = GetComponent<PositionController>();
        _movementState = GetComponent<MovementStateController>();
        _movementSettings = GetComponent<MovementSettings>();
        _animationController = GetComponent<AnimationController>();
    }

    private void Start()
    {
        _animationController.SetNextAnimation("PlayerShip_Movement");
        _positionController.SetConstantForwardMovement(_movementSettings.Speed.x / 2f);
    }

    private void Update()
    {
        if (!_rotationState.Active || !_rotationState.CanRotate)
            return;

        float rotation = 0f;
        if (_rotationState.Rotation.Right)
            rotation -= 2.5f;
        else if (_rotationState.Rotation.Left)
            rotation += 2.5f;

        _rotationController.UpdateRotation(rotation);

        if(!_movementState.Active || !_movementState.CanMove)
            return;
        if (_movementSettings == null)
            return;

        if (_movementState.Movement.Forward || _movementState.Movement.Right)
        {
            _positionController.MoveForward(_movementSettings.Speed.x, _movementSettings.Acceleration);
        }
        else if (_movementState.Movement.Back)
        {
            _positionController.MoveBackward(_movementSettings.Speed.x, _movementSettings.Acceleration, true);
        }
        else if (_movementState.Movement.Left)
        {
            _positionController.MoveBackward(_movementSettings.Speed.x, _movementSettings.Acceleration);
        }
        else
        {
            _positionController.DecelerateHorizontal(_movementSettings.Deceleration);
        }

        if (_movementState.Movement.Up || _movementState.Movement.Down)
        {
            _positionController.VerticalMovement(
                _movementSettings.Speed.y,
                _movementSettings.Acceleration,
                _movementState.Movement.Up);
        }
        else
        {
            _positionController.DecelerateVertical(_movementSettings.Deceleration);
        }

    }
}