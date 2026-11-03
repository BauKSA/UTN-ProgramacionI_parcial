using UnityEngine;

[RequireComponent(typeof(MovementStateController))]
[RequireComponent(typeof(MovementSettings))]
[RequireComponent(typeof(PositionController))]
public class EyeBeakTick : MonoBehaviour
{
    private MovementStateController _movementStateController;
    private MovementSettings _movementSettings;
    private PositionController _positionController;
    private bool _active = false;

    public event System.Action ReachedTarget;
    public event System.Action StartPosition;
    public event System.Action ClosingBeak;

    [SerializeField]
    private bool _topEye = true;

    private void Awake()
    {
        _movementSettings = GetComponent<MovementSettings>();
        _positionController = GetComponent<PositionController>();
        _movementStateController = GetComponent<MovementStateController>();
    }

    public void Activate()
    {
        _active = true;

        EyeBeakMovementEvent movementEvent = new(gameObject, _topEye, this);
        movementEvent.Execute();

        if (!_topEye) return;

        movementEvent.ReachedTarget += HandleReachedTarget;
        movementEvent.StartPosition += HandleStartPosition;
        movementEvent.ClosingBeak += HandleClosingBeak;
    }

    private void Update()
    {
        if(!_active) return;

        Vector2 delta = Vector2.zero;
        if (_movementStateController.Movement.Up)
            delta += Vector2.up * (_movementSettings.Speed.y * Time.deltaTime);
        else if (_movementStateController.Movement.Down)
            delta += Vector2.down * (_movementSettings.Speed.y * Time.deltaTime);

        if (_movementStateController.Movement.Right)
            delta += Vector2.right * (_movementSettings.Speed.x * Time.deltaTime);
        else if (_movementStateController.Movement.Left)
            delta += Vector2.left * (_movementSettings.Speed.x * Time.deltaTime);

        _positionController.UpdatePosition(delta);
    }

    private void HandleReachedTarget() => ReachedTarget?.Invoke();
    private void HandleStartPosition() => StartPosition?.Invoke();
    private void HandleClosingBeak() => ClosingBeak?.Invoke();
}