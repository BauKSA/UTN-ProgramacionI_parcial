using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField]
    private float _rotation;

    private void Awake()
    {
        _rotation = transform.rotation.eulerAngles.z;
    }

    private void SyncRotation()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, _rotation);
    }

    public void UpdateRotation(float rotation)
    {
        _rotation += rotation;

        SyncRotation();
    } 
}