using UnityEngine;
using UnityEngine.UIElements;

public interface IMovementState
{
    bool Right { get; set; }
    bool Left { get; set; }
    bool Up { get; set; }
    bool Down { get; set; }
    bool Forward { get; set; }
    bool Back { get; set; }
}

[System.Serializable]
public class MovementState : IMovementState
{
    [SerializeField] private bool right;
    [SerializeField] private bool left;
    [SerializeField] private bool up;
    [SerializeField] private bool down;
    [SerializeField] private bool forward;
    [SerializeField] private bool back;

    public bool Right { get => right; set => right = value; }
    public bool Left { get => left; set => left = value; }
    public bool Up { get => up; set => up = value; }
    public bool Down { get => down; set => down = value; }
    public bool Forward { get => forward; set => forward = value; }
    public bool Back { get => back; set => back = value; }

    public MovementState(bool right, bool left, bool up, bool down)
    {
        Right = right;
        Left = left;
        Up = up;
        Down = down;
        Forward = false;
        Back = false;
    }
}

public interface IMovementStateController
{
    bool Active { get; set; }
    bool CanMove { get; set; }
    IMovementState Movement { get; set; }
}

[RequireComponent(typeof(MovementSettings))]
public class MovementStateController : MonoBehaviour, IMovementStateController
{
    [SerializeField] private bool active = true;
    [SerializeField] private bool canMove = true;
    [SerializeField] private MovementState movement = new(false, false, false, false);

    public bool Active { get => active; set => active = value; }
    public bool CanMove { get => canMove; set => canMove = value; }

    public IMovementState Movement
    {
        get => movement;
        set => movement = value as MovementState;
    }

    public void Reset()
    {
        active = true;
        canMove = true;
        movement = new(false, false, false, false);
    }
}