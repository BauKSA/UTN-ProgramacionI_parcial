using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class EyeEnemyStateController : StateController
{
    private AnimationController _animationController;

    private bool _pendingAnimation = false;
    private string _pendingAnimationName = "";
    private bool _lastDamageState = false;
    private bool _eyeOpened = false;

    public bool PendingAnimation
    {
        get => _pendingAnimation;
        private set
        {
            if (_pendingAnimation == value) return;
            _pendingAnimation = value;
        }
    }

    public string PendingAnimationName
    {
        get => _pendingAnimationName;
        private set
        {
            if (_pendingAnimationName == value) return;
            _pendingAnimationName = value;
        }
    }

    public bool EyeOpened
    {
        get => _eyeOpened;
        private set
        {
            if (_eyeOpened == value) return;
            _eyeOpened = value;
        }
    }

    private void Awake()
    {
        _animationController = GetComponent<AnimationController>();
    }

    private void Start()
    {
        _lastDamageState = BeingDamaged;
    }

    private void Update()
    {
        if (BeingDamaged == _lastDamageState) return;
        _lastDamageState = BeingDamaged;

        if (BeingDamaged)
        {
            ResetPending();
            return;
        }

        if (PendingAnimation)
            _animationController.StartAnimation(PendingAnimationName);

        ResetPending();
    }

    public void SetPendingAnimation(string name)
    {
        PendingAnimation = true;
        PendingAnimationName = name;
    }

    public void ResetPending()
    {
        PendingAnimation = false;
        PendingAnimationName = "";
    }

    public void SetEyeOpened(bool value) => EyeOpened = value;
}