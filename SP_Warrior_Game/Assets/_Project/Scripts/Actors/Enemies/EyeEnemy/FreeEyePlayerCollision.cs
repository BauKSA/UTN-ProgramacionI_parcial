using UnityEngine;

public class FreeEyePlayerCollision : MonoBehaviour
{
    private StateController _freeEyeState;

    private void Awake()
    {
        TryGetComponent<StateController>(out _freeEyeState);
        if (_freeEyeState == null)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (_freeEyeState.Initialized)
            return;

        Debug.Log("FreeEye destroyed due to Player collision");
        Destroy(gameObject);
    }
}