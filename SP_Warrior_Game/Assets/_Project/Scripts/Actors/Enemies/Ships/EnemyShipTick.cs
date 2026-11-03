using System;
using UnityEngine;
[RequireComponent(typeof(PositionController))]
[RequireComponent(typeof(MovementSettings))]
public class EnemyShipTick : MonoBehaviour
{
    private PositionController _positionController;
    private MovementSettings _movementSettings;
    private AShipShootPattern _shootPattern;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _shootInterval = 2f;
    [SerializeField]
    private char _shipType = 'A';

    void Awake()
    {
        _positionController = GetComponent<PositionController>();
        _movementSettings = GetComponent<MovementSettings>();
    }

    void Start()
    {
        _shootPattern = new(_shootInterval, _bulletPrefab, transform);
    }

    private void Update()
    {
        _shootPattern.Tick();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerViewRange"))
            return;

        _shootPattern.Activate();

        _positionController.SetConstantForwardMovement(_movementSettings.Speed.x);

        if (_shipType == 'A')
        {
            if (Math.Abs(transform.position.y - _player.transform.position.y) < 25f)
                return;

            Vector2 direction = _player.transform.position - transform.position;
            transform.right = direction;
        }
    }
}