using UnityEngine;
using System;

public class PlayerMovementEvent : MovementEvent
{
    private readonly AnimationController _animationController;
    private bool forwardAnimationActive = false;

    public PlayerMovementEvent(
        MovementDirection direction,
        MovementStateController movementState,
        AnimationController animationController,
        HealthStateController healthState
        ) : base(direction, movementState, healthState) { _animationController = animationController; }

    public override void LocalExecute()
    {
        if(_direction == MovementDirection.Forward && !forwardAnimationActive)
        {
            _animationController.SetNextAnimation("PlayerShip_Movement");
            forwardAnimationActive = true;
        }
    }

    public override void LocalStop()
    {
        if (_direction == MovementDirection.Forward && forwardAnimationActive)
        {
            _animationController.SetNextAnimation("PlayerShip_IDLE");
            forwardAnimationActive = false;
        }
    }
}