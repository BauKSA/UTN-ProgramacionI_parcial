using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerViewRangeTick : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private GameObject _mainCamera;
    [SerializeField]
    private GameObject _playerTarget;
    [SerializeField]
    private float _offset = 185f;
    private bool _movementLimited = false;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_movementLimited) return;

        _rigidBody.MovePosition(new Vector2(
            _playerTarget.transform.position.x + _offset,
            0f
        ));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("CameraLimit")) return;
        
        CameraMovementLimitation cameraMovement = _mainCamera.GetComponent<CameraMovementLimitation>();
        cameraMovement.SetXLimit(_mainCamera.transform.position.x);

        _playerTarget.GetComponent<PositionController>().ClearConstantForwardMovement();
        _movementLimited = true;
    }
}
