using UnityEngine;

public class StateController : MonoBehaviour
{
    protected bool _initialized;
    protected bool _beingDamaged = false;

    public bool Initialized
    {
        get => _initialized;
        private set
        {
            if (_initialized == value) return;
            _initialized = value;
        }
    }

    public bool BeingDamaged
    {
        get => _beingDamaged;
        private set
        {
            if (_beingDamaged == value) return;
            _beingDamaged = value;
        }
    }

    public void SetInitialized(bool value) => Initialized = value;
    public void SetBeingDamaged(bool value) => BeingDamaged = value;
}
