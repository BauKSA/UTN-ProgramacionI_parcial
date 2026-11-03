using UnityEngine.InputSystem;

public interface IPlayerActions
{
    InputAction RotateRight { get; }
    InputAction RotateLeft { get; }
    InputAction MoveForward { get; }
    InputAction MoveBack { get; }
    InputAction MoveDown { get; }
    InputAction MoveUp { get; }
    InputAction MoveRight { get; }
    InputAction MoveLeft { get; }
    InputAction Shoot { get; }
}