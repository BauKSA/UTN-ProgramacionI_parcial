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
    private float _shootInterval = 0.5f;
    [SerializeField]
    private char _shipType = 'A';

    private bool _shipBTargetingPlayer = false;

    void Awake()
    {
        _positionController = GetComponent<PositionController>();
        _movementSettings = GetComponent<MovementSettings>();
    }

    void Start()
    {
        if (_shipType == 'A') _shootPattern = new(_shootInterval, _bulletPrefab, transform);
    }

    private void Update()
    {
        if (_shipType == 'A') _shootPattern.Tick();

        if (_shipType == 'B')
        {
            if (_shipBTargetingPlayer)
            {
                Vector2 direction = _player.transform.position - transform.position;
                transform.right = direction;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerViewRange"))
            return;

        _positionController.SetConstantForwardMovement(_movementSettings.Speed.x);

        if (_shipType == 'A')
        {
            if (Math.Abs(transform.position.y - _player.transform.position.y) < 25f)
                return;

            Vector2 direction = _player.transform.position - transform.position;
            transform.right = direction;

            _shootPattern.Activate();
        }
        if (_shipType == 'B')
        {
            Vector2 direction = _player.transform.position - transform.position;
            transform.right = direction;

            _shipBTargetingPlayer = true;
        }
    }
}