using UnityEngine;
using UnityEngine.InputSystem;

public class FreeEyeShootEvent : ActionEvent
{
    private readonly GameObject _owner;
    private readonly FreeEyeShootConfig _config;

    public FreeEyeShootEvent(GameObject owner, FreeEyeShootConfig config)
    {
        _owner = owner;
        _config = config;
    }

    public override void Execute(InputAction.CallbackContext context) => Execute();

    public void Execute()
    {
        GameObject[] prefabs = _config.BulletPrefabs;
        for(var i = 0; i < prefabs.Length; i++)
        {
            Debug.Log($"Instantiate Bullet n{i}");
            Object.Instantiate(prefabs[i], _owner.transform.position, Quaternion.identity);
        }
    }
}