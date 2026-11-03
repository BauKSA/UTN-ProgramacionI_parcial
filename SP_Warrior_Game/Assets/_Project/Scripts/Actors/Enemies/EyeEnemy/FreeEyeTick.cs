using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(StateController))]
public class FreeEyeTick : MonoBehaviour
{
    private AnimationController _animationController;
    private StateController _stateController;
    private FreeEyeShootEvent _shootEvent;

    [SerializeField]
    private FreeEyeShootConfig _config;

    private readonly float _lifeTime = 3f;
    private float _currentTime = 0f;

    private void Awake()
    {
        _animationController = GetComponent<AnimationController>();
        _stateController = GetComponent<StateController>();

        _shootEvent = new(gameObject, _config);
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < 3; i++)
            yield return null;

        _animationController.StartAnimation("FreeEye_Open", true, _shootEvent.Execute);
        _animationController.SetNextAnimation("FreeEye_Opened");

        _stateController.SetInitialized(true);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _lifeTime)
            Sleep();
    }

    private void Sleep()
    {
        _animationController.StartAnimation("FreeEye_Close", true, () => Destroy(gameObject));
    }
}
