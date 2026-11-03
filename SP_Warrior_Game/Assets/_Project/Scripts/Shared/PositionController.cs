using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PositionController : MonoBehaviour
{
    private Vector2 _currentSpeed;
    private Vector2 _maxSpeed;
    private Vector2 _pendingMove;

    private Rigidbody2D _rigidbody;

    private float _constantForwardSpeed;

    public void SetConstantForwardMovement(float speed)
    {
        _constantForwardSpeed = speed;
    }

    public void ClearConstantForwardMovement()
    {
        _constantForwardSpeed = 0f;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = (Vector2)transform.right * (_currentSpeed.x * _maxSpeed.x + _constantForwardSpeed)
                         + (Vector2)transform.up * (_currentSpeed.y * _maxSpeed.y);

        _rigidbody.linearVelocity = velocity + _pendingMove / Time.fixedDeltaTime;
        _pendingMove = Vector2.zero;
    }

    public void Stop()
    {
        _pendingMove = Vector2.zero;
        _currentSpeed = Vector2.zero;
        _rigidbody.linearVelocity = Vector2.zero;
    }

    public void SnapTo(Vector2 position)
    {
        _pendingMove = Vector2.zero;
        _currentSpeed = Vector2.zero;
        _rigidbody.linearVelocity = Vector2.zero;

        _rigidbody.position = position;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    public void UpdatePosition(Vector2 delta)
    {
        _pendingMove += delta;
    }

    public void MoveForward(float speed, float acceleration)
    {
        _maxSpeed.x = speed;
        _currentSpeed.x = Mathf.Clamp(_currentSpeed.x + GetStep(acceleration, speed), -1f, 1f);
        LimitDiagonal();
    }

    private float GetMinCurrentSpeedX()
    {
        if (_constantForwardSpeed <= 0f || _maxSpeed.x <= 0f)
            return -1f;

        float allowedDrop = _constantForwardSpeed * (0.25f);
        return Mathf.Max(-1f, -allowedDrop / _maxSpeed.x);
    }

    public void MoveBackward(float speed, float acceleration, bool limited = false)
    {
        float floor = (limited && _constantForwardSpeed <= 0f) ? 0f : GetMinCurrentSpeedX();

        if (limited && _currentSpeed.x <= floor)
            return;

        _maxSpeed.x = speed;
        _currentSpeed.x = Mathf.Clamp(_currentSpeed.x - GetStep(acceleration, speed), floor, 1f);

        LimitDiagonal();
    }

    public void VerticalMovement(float speed, float acceleration, bool up = true)
    {
        float direction = up ? 1f : -1f;

        _maxSpeed.y = speed;
        _currentSpeed.y = Mathf.Clamp(_currentSpeed.y + direction * GetStep(acceleration, speed), -1f, 1f);
        LimitDiagonal();
    }

    public void DecelerateHorizontal(float deceleration)
    {
        _currentSpeed.x = Mathf.MoveTowards(_currentSpeed.x, 0f, GetStep(deceleration, _maxSpeed.x));
    }

    public void DecelerateVertical(float deceleration)
    {
        _currentSpeed.y = Mathf.MoveTowards(_currentSpeed.y, 0f, GetStep(deceleration, _maxSpeed.y));
    }

    private float GetStep(float rate, float speed)
    {
        if (speed <= 0f)
            return 0f;

        if (float.IsPositiveInfinity(rate))
            return 2f;

        return rate / speed * Time.deltaTime;
    }

    private void LimitDiagonal()
    {
        if (_currentSpeed.sqrMagnitude > 1f)
            _currentSpeed.Normalize();
    }
}