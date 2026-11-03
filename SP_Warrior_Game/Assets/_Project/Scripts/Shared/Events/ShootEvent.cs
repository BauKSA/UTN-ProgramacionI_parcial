using UnityEngine;
using UnityEngine.InputSystem;

public class ShootEvent : ActionEvent
{
    protected GameObject _bulletPrefab;
    protected GameObject _parent;

    public ShootEvent(GameObject bulletPrefab, GameObject parent)
    {
        _bulletPrefab = bulletPrefab;
        _parent = parent;
    }
    public override void Execute(InputAction.CallbackContext context)
    {
        if (!_parent.TryGetComponent<HealthStateController>(out var state)){
            Debug.Log("Parent does not have HealthStateController");
            return;
        }

        if (!state.Alive) return;

        float offset = 0f;

        if (_parent.TryGetComponent<SpriteRenderer>(out SpriteRenderer parentSpriteRenderer))
            offset += parentSpriteRenderer.bounds.extents.x;

        if (_bulletPrefab.TryGetComponent<SpriteRenderer>(out SpriteRenderer bulletSpriteRenderer))
            offset += bulletSpriteRenderer.bounds.extents.x;

        Vector2 position = (Vector2)_parent.transform.position
                         + (Vector2)_parent.transform.right * offset;

        Object.Instantiate(_bulletPrefab, position, _parent.transform.rotation);
    }
}
