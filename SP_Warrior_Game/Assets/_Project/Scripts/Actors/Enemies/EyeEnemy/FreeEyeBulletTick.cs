using UnityEngine;

public enum FreeEyeBulletType
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}

[RequireComponent(typeof(MovementSettings))]

[RequireComponent(typeof(PositionController))]
[RequireComponent(typeof(MovementStateController))]
public class FreeEyeBulletTick : MonoBehaviour
{
    private MovementSettings _movementSettings;
    private PositionController _positionController;
    private MovementStateController _movementStateController;

    private readonly float _lifeTime = 3.5f;
    private float _currentTime = 0f;

    [SerializeField]
    private FreeEyeBulletType _bulletType;

    private void Awake()
    {
        _movementSettings = GetComponent<MovementSettings>();
        _positionController = GetComponent<PositionController>();
        _movementStateController = GetComponent<MovementStateController>();
    }

    private void Start()
    {
        switch (_bulletType)
        {
            case FreeEyeBulletType.TopLeft:
                _movementStateController.Movement.Up = true;
                _movementStateController.Movement.Left = true;
                break;
            case FreeEyeBulletType.TopRight:
                _movementStateController.Movement.Up = true;
                _movementStateController.Movement.Right = true;
                break;
            case FreeEyeBulletType.BottomLeft:
                _movementStateController.Movement.Down = true;
                _movementStateController.Movement.Left = true;
                break;
            case FreeEyeBulletType.BottomRight:
                _movementStateController.Movement.Down = true;
                _movementStateController.Movement.Right = true;
                break;
        }

    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _lifeTime)
            Destroy(gameObject);

        Vector2 direction = Vector2.zero;

        if (_movementStateController.Movement.Up)
            direction.y = 1f;
        else if (_movementStateController.Movement.Down)
            direction.y = -1f;

        if (_movementStateController.Movement.Right)
            direction.x = 1f;
        else if (_movementStateController.Movement.Left)
            direction.x = -1f;

        direction.Normalize();

        Vector2 position = new Vector2(
            direction.x * _movementSettings.Speed.x,
            direction.y * _movementSettings.Speed.y
        ) * Time.deltaTime;

        _positionController.UpdatePosition(position);
    }
}
