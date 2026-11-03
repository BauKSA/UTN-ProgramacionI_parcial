using UnityEngine;

[RequireComponent(typeof(EyeEnemyStateController))]
public class EyeEnemyTick : MonoBehaviour
{
    [SerializeField]
    private GameObject _topEye;
    [SerializeField]
    private GameObject _bottomEye;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private GameObject _freeEyeSpawner;


    private bool _reachedTarget = false;
    private bool _startPosition = false;

    private bool _firstLoop = true;
    private bool _active = false;

    public EyeEnemyShootEvent Shoot;

    private EyeEnemyStateController _stateController;

    private void Awake()
    {
        Shoot = new(gameObject);
        _stateController = GetComponent<EyeEnemyStateController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerViewRange")) return;
        Activate();
    }

    private void Activate()
    {
        EyeBeakTick topEyeBeakTick = _topEye.GetComponent<EyeBeakTick>();
        EyeBeakTick bottomEyeBeakTick = _bottomEye.GetComponent<EyeBeakTick>();

        topEyeBeakTick.Activate();
        bottomEyeBeakTick.Activate();

        topEyeBeakTick.ReachedTarget += () => _reachedTarget = true;
        topEyeBeakTick.StartPosition += () => _startPosition = true;
        topEyeBeakTick.ClosingBeak += () => {
            if (_firstLoop) return;

            _stateController.SetEyeOpened(false);

            if (_stateController.BeingDamaged)
            {
                _stateController.SetPendingAnimation("EyeEnemy_Closed");
                return;
            }

            Shoot.Reset();
        };

        _freeEyeSpawner.GetComponent<FreeEyeSpawner>().Activate();
        _active = true;
    }

    private void Update()
    {
        if (_stateController.BeingDamaged) return;
        if (!_active) return;

        if (_startPosition)
        {
            if (_firstLoop)
            {
                _firstLoop = false;
                _startPosition = false;
            }

            _startPosition = false;

            Shoot?.Initialize(_bulletPrefab);
        }

        if (_reachedTarget)
        {
            if (_firstLoop)
            {
                _reachedTarget = false;
                return;
            }

            _reachedTarget = false;

            _stateController.SetEyeOpened(true);
            Shoot?.Execute();
        }
    }
}
