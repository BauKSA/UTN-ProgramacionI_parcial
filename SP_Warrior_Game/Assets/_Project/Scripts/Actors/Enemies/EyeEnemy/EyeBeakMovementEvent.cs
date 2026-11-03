using UnityEngine;
using UnityEngine.InputSystem;

public class EyeBeakMovementEvent : ActionEvent
{
    private readonly GameObject _eyeBeak;
    private readonly MovementStateController _movementStateController;
    private readonly HealthStateController _healthStateController;
    private readonly PositionController _positionController;
    private readonly MonoBehaviour _coroutineRunner;
    private readonly float _waitTime = 3.5f;
    private readonly bool _topEye;

    public event System.Action ReachedTarget;
    public event System.Action StartPosition;
    public event System.Action ClosingBeak;

    public EyeBeakMovementEvent(GameObject eyeBeak, bool topEye, MonoBehaviour coroutineRunner)
    {
        _eyeBeak = eyeBeak;
        _topEye = topEye;
        _coroutineRunner = coroutineRunner;

        _positionController = _eyeBeak.GetComponent<PositionController>();
        _movementStateController = _eyeBeak.GetComponent<MovementStateController>();
        _healthStateController = _eyeBeak.GetComponent<HealthStateController>();
    }


    public override void Execute(InputAction.CallbackContext context) => Execute();

    public void Execute()
    {
        _coroutineRunner.StartCoroutine(PatternLoop());
    }

    private System.Collections.IEnumerator PatternLoop()
    {
        Vector2 startPosition = _eyeBeak.transform.position;
        Vector2 targetPosition = new(startPosition.x + 18f, _eyeBeak.transform.position.y);

        while (_healthStateController.Alive)
        {
            if (_topEye) _movementStateController.Movement.Up = true;
            else _movementStateController.Movement.Down = true;

            _movementStateController.Movement.Right = true;
            yield return new WaitUntil(() => _eyeBeak.transform.position.x >= targetPosition.x);

            if (_topEye) _movementStateController.Movement.Up = false;
            else _movementStateController.Movement.Down = false;

            _movementStateController.Movement.Right = false;
            _positionController.SnapTo(new Vector2(targetPosition.x, _eyeBeak.transform.position.y));

            if (_topEye) ReachedTarget?.Invoke();
        
            yield return new WaitForSeconds(_waitTime);

            if (_topEye) ClosingBeak?.Invoke();

            if (_topEye) _movementStateController.Movement.Down = true;
            else _movementStateController.Movement.Up = true;

            _movementStateController.Movement.Left = true;
            yield return new WaitUntil(() => _eyeBeak.transform.position.x <= startPosition.x);

            if (_topEye) _movementStateController.Movement.Down = false;
            else _movementStateController.Movement.Up = false;

            _movementStateController.Movement.Left = false;
            _positionController.SnapTo(new Vector2(startPosition.x, startPosition.y));
            yield return new WaitForSeconds(_waitTime);

            if (_topEye) StartPosition?.Invoke();
        }
    }
}
