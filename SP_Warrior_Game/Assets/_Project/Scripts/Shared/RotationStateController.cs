using UnityEngine;

public interface IRotationState
{
    bool Right { get; set; }
    bool Left { get; set; }
}

[System.Serializable]
public class RotationState : IRotationState
{
    [SerializeField] private bool right;
    [SerializeField] private bool left;

    public bool Right { get => right; set => right = value; }
    public bool Left { get => left; set => left = value; }

    public RotationState(bool right, bool left)
    {
        Right = right;
        Left = left;
    }
}

public interface IRotationStateController
{
    bool Active { get; set; }
    bool CanRotate { get; set; }
    IRotationState Rotation { get; set; }
}

public class RotationStateController : MonoBehaviour, IRotationStateController
{
    [SerializeField] private bool active = true;
    [SerializeField] private bool canRotate = true;
    [SerializeField] private RotationState rotation = new(false, false);

    public bool Active { get => active; set => active = value; }
    public bool CanRotate { get => canRotate; set => canRotate = value; }

    public IRotationState Rotation
    {
        get => rotation;
        set => rotation = value as RotationState;
    }
}