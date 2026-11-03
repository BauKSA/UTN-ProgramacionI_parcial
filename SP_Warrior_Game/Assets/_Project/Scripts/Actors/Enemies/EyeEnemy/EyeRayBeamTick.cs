using UnityEngine;
[RequireComponent(typeof(PositionController))]

[RequireComponent(typeof(MovementSettings))]
[RequireComponent(typeof(MovementStateController))]
public class EyeRayBeamTick : MonoBehaviour
{
    private PositionController _positionController;
    private MovementSettings _movementSettings;
    private MovementStateController _movementStateController;

    [SerializeField] private float _startX;
    [SerializeField] private float _endX;
    [SerializeField] private float _minScale = 0.1f;
    [SerializeField] private float _maxScale = 1f;

    public bool _isShot = false;

    private readonly float _lifeTime = 3f;
    private float _currentTime = 0f;

    private void Awake()
    {
        _positionController = GetComponent<PositionController>();
        _movementSettings = GetComponent<MovementSettings>();
        _movementStateController = GetComponent<MovementStateController>();
    }

    private void Start()
    {
        _startX = transform.position.x;
        _endX = _startX + 15f;
    }

    private void Update()
    {
        if (_isShot)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _lifeTime)
                Destroy(gameObject);

            if (!_movementStateController.Movement.Back)
                return;

            _positionController.MoveBackward(_movementSettings.Speed.x, _movementSettings.Acceleration);
            return;
        }

        if (transform.position.x >= _endX)
        {
            _positionController.Stop();
            _isShot = true;
            return;
        }

        float progress = Mathf.InverseLerp(_startX, _endX, transform.position.x);
        float currentScale = Mathf.Lerp(_minScale, _maxScale, progress);

        transform.localScale = new Vector3(currentScale, currentScale, 1f);

        _positionController.MoveForward(_movementSettings.Speed.x, _movementSettings.Acceleration);
    }
}
