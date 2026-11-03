using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementDirection
{
    Up,
    Down,
    Left,
    Right,
    Forward,
    Back
}

public class MovementEvent : ActionEvent
{
    protected readonly MovementDirection _direction;
    protected readonly MovementStateController _movementState;
    protected readonly HealthStateController _healthStateController;

    public MovementEvent(
        MovementDirection direction,
        MovementStateController movementState,
        HealthStateController healthStateController
        )
    {
        _direction = direction;
        _movementState = movementState;
        _healthStateController = healthStateController;
    }
    public override void Execute(InputAction.CallbackContext context)
    {
        if (!_movementState.Active)
            return;

        if (!_healthStateController.Alive)
            return;

        LocalExecute();

        switch (_direction)
        {
            default:
                break;
            case MovementDirection.Right:
                _movementState.Movement.Right = true;
                break;
            case MovementDirection.Left:
                _movementState.Movement.Left = true;
                break;
            case MovementDirection.Up:
                _movementState.Movement.Up = true;
                break;
            case MovementDirection.Down:
                _movementState.Movement.Down = true;
                break;
            case MovementDirection.Forward:
                _movementState.Movement.Forward = true;
                break;
            case MovementDirection.Back:
                _movementState.Movement.Back = true;
                break;
        }
    }

    public override void Stop(InputAction.CallbackContext context)
    {
        if (!_healthStateController.Alive)
            return;

        LocalStop();

        switch (_direction)
        {
            default:
                break;
            case MovementDirection.Right:
                _movementState.Movement.Right = false;
                break;
            case MovementDirection.Left:
                _movementState.Movement.Left = false;
                break;
            case MovementDirection.Up:
                _movementState.Movement.Up = false;
                break;
            case MovementDirection.Down:
                _movementState.Movement.Down = false;
                break;
            case MovementDirection.Forward:
                _movementState.Movement.Forward = false;
                break;
            case MovementDirection.Back:
                _movementState.Movement.Back = false;
                break;
        }
    }
}