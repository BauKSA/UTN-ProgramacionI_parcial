using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerResetStatesEvent : ActionEvent
{
    private readonly AnimationController _animationController;
    private readonly MovementStateController _movementState;

    public PlayerResetStatesEvent(
        MovementStateController movementState,
        AnimationController animationController
        ) : base() {
        _animationController = animationController;
        _movementState = movementState;
    }

    public override void Execute(InputAction.CallbackContext context) => Execute();

    public void Execute()
    {
        _animationController.StartAnimation("PlayerShip_IDLE");
        _movementState.Reset();
    }
}