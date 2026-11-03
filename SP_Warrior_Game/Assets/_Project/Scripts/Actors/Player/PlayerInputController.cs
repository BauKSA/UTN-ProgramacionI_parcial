using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementStateController))]

[RequireComponent(typeof(RotationStateController))]

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(PlayerHealthStateController))]
public class PlayerInputController : MonoBehaviour
{
    private IPlayerActions _actions;
    [SerializeField] private GameObject _bulletPrefab;

    private PlayerResetStatesEvent _resetEvent;
    private readonly List<Action> _unbinders = new();

    private MovementStateController _movementStateController;
    private RotationStateController _rotationController;
    private AnimationController _animationController;
    private PlayerHealthStateController _healthController;

    private void OnEnable()
    {
        GlobalInputController.Instance.OnPlayerMovementChanged += SyncBindings;
    }

    private void Awake()
    {
        _movementStateController = GetComponent<MovementStateController>();
        _rotationController = GetComponent<RotationStateController>();
        _animationController = GetComponent<AnimationController>();
        _healthController = GetComponent<PlayerHealthStateController>();
    }

    private void Start()
    {
        _actions = GlobalInputController.Instance.PlayerActions;

        ShootEvent shootEvent = new(_bulletPrefab, gameObject);
        _actions.Shoot.performed += shootEvent.Execute;

        _resetEvent =
            new(_movementStateController, _animationController);

        ResetStates();
        BindLimitedMovementInput();
    }

    private void SyncBindings()
    {
        ResetStates();

        if (GlobalInputController.Instance.PlayerMovement == GlobalInputController.PlayerMovementType.LIMITED)
            BindLimitedMovementInput();
        else
            BindFreeMovementInput();
    }

    private void ResetStates()
    {
        _resetEvent?.Execute();

        foreach (var unbind in _unbinders)
            unbind();

        _unbinders.Clear();
    }

    private void Bind(InputAction action, Action<InputAction.CallbackContext> performed,
                      Action<InputAction.CallbackContext> canceled = null)
    {
        action.performed += performed;
        if (canceled != null) action.canceled += canceled;

        _unbinders.Add(() =>
        {
            action.performed -= performed;
            if (canceled != null) action.canceled -= canceled;
        });
    }

    private void BindLimitedMovementInput()
    {
        MovementEvent right = new(
            MovementDirection.Right,
            _movementStateController,
            _healthController);

        MovementEvent left = new(
            MovementDirection.Left,
            _movementStateController,
            _healthController);

        MovementEvent up = new(
            MovementDirection.Up,
            _movementStateController,
            _healthController);

        MovementEvent down = new(
            MovementDirection.Down,
            _movementStateController,
            _healthController);

        Bind(_actions.MoveRight, right.Execute, right.Stop);
        Bind(_actions.MoveLeft, left.Execute, left.Stop);
        Bind(_actions.MoveUp, up.Execute, up.Stop);
        Bind(_actions.MoveDown, down.Execute, down.Stop);
    }

    private void BindFreeMovementInput()
    {
        RotateEvent rotateRight = new(
            RotateDirection.Right,
            _rotationController,
            _healthController);

        RotateEvent rotateLeft = new(
            RotateDirection.Left,
            _rotationController,
            _healthController);

        Bind(_actions.RotateRight, rotateRight.Execute, rotateRight.Stop);
        Bind(_actions.RotateLeft, rotateLeft.Execute, rotateLeft.Stop);

        PlayerMovementEvent moveForward = new(
            MovementDirection.Forward,
            _movementStateController,
            _animationController,
            _healthController);

        PlayerMovementEvent moveBackward = new(
            MovementDirection.Back,
            _movementStateController,
            _animationController,
            _healthController);

        Bind(_actions.MoveForward, moveForward.Execute, moveForward.Stop);
        Bind(_actions.MoveBack, moveBackward.Execute, moveBackward.Stop);
    }
}
