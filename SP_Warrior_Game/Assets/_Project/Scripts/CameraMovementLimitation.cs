using UnityEngine;

public class CameraMovementLimitation : MonoBehaviour
{
    private bool _xLimited = false;
    private float _xLimit = 0f;
    void LateUpdate()
    {
        float x = _xLimited ? _xLimit : transform.position.x;
        transform.position = new Vector3(
            x,
            0f,
            transform.position.z
        );
    }

    public void SetXLimit(float limit)
    {
        _xLimited = true;
        _xLimit = limit;
    }
}
