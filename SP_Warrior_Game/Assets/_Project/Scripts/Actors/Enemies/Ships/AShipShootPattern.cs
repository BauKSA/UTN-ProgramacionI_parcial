using UnityEngine;

class AShipShootPattern : Pattern
{
    private readonly float _shootInterval;
    private readonly GameObject _bulletPrefab;
    private readonly Transform _origin;

    private float _shootTimer = 0f;

    private float _offset;

    public AShipShootPattern(float shootInterval, GameObject bulletPrefab, Transform origin)
    {
        _shootInterval = shootInterval;
        _bulletPrefab = bulletPrefab;
        _origin = origin;

        _offset = _origin.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        _offset += _bulletPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        _offset += 2f;
    }

    protected override void Execute()
    {
        Vector2 spawnPosition = (Vector2)_origin.position + (Vector2)_origin.right * _offset;
        Object.Instantiate(_bulletPrefab, spawnPosition, _origin.rotation);
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