using UnityEngine;
using UnityEngine.InputSystem;

public class EyeEnemyShootEvent : ActionEvent
{
    private readonly GameObject _owner;
    private readonly float _offset = 37.5f;
    private GameObject _bulletPrefab;

    public EyeEnemyShootEvent(GameObject owner)
    {
        _owner = owner;
    }

    public override void Execute(InputAction.CallbackContext context) => Execute();

    public void Initialize(GameObject bulletPrefab)
    {
        Vector2 position = _owner.transform.position;
        position.x -= _offset;
        GameObject bullet = Object.Instantiate(bulletPrefab, position, Quaternion.identity);
        bullet.transform.localScale = new Vector3(0.1f, 0.1f, 1f);

        _bulletPrefab = bullet;
    }

    public void Execute()
    {
        _owner.TryGetComponent<AnimationController>(out var animationController);
        if(animationController == null)
            return;

        animationController.StartAnimation("EyeEnemy_Open", true, Shoot);
        animationController.SetNextAnimation("EyeEnemy_Opened");
    }

    private void Shoot()
    {
        _bulletPrefab.TryGetComponent<MovementStateController>(out var movementStateController);
        if (movementStateController == null)
            return;

        _bulletPrefab.TryGetComponent<MovementSettings>(out var movementSettings);
        if (movementSettings == null)
            return;

        movementStateController.Movement.Back = true;
        movementSettings.Speed = new(75f, movementSettings.Speed.y);

        Debug.Log("Eye Enemy Shoot Event Executed");
    }

    public void Reset()
    {
        _owner.TryGetComponent<AnimationController>(out var animationController);
        if (animationController == null)
            return;

        animationController.StartAnimation("EyeEnemy_Close", true);
        animationController.SetNextAnimation("EyeEnemy_Closed");
    }

}
