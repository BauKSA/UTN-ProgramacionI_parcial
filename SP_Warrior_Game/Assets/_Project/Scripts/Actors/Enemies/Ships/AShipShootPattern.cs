using UnityEngine;

class AShipShootPattern : Pattern
{
    private readonly float _shootInterval;
    private readonly GameObject _bulletPrefab;
    private readonly Transform _origin;

    private float _shootTimer = 0f;

    public AShipShootPattern(float shootInterval, GameObject bulletPrefab, Transform origin)
    {
        _shootInterval = shootInterval;
        _bulletPrefab = bulletPrefab;
        _origin = origin;
    }

    protected override void Execute()
    {
        Object.Instantiate(_bulletPrefab, _origin.position, _origin.rotation);
    }

    protected override bool Wait()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer >= _shootInterval)
        {
            _shootTimer = 0f;
            return false;
        }

        return true;
    }
}