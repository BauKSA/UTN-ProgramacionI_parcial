using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(HealthStateController))]
[RequireComponent(typeof(MovementStateController))]

[RequireComponent(typeof(PositionController))]
[RequireComponent(typeof(MovementSettings))]
public class PlayerHealthStateController : HealthStateController
{
    private HeartsContainerController _heartsContainer;
    private HealthStateController _healthStateController;
    private MovementStateController _movementStateController;
    private PositionController _positionController;
    private MovementSettings _movementSettings;

    private void Awake()
    {
        _healthStateController = GetComponent<HealthStateController>();
        _movementStateController = GetComponent<MovementStateController>();
        _positionController = GetComponent<PositionController>();
        _movementSettings = GetComponent<MovementSettings>();
    }

    private void Start()
    {
        _heartsContainer = HeartsContainerController.Instance;
        _heartsContainer.Initialize(Lives);
    }

    private void Update()
    {
        if (_heartsContainer.GetHearts() == Lives)
            return;

        if(_heartsContainer.GetHearts() > Lives)
        {
            while (_heartsContainer.GetHearts() > Lives)
                _heartsContainer.RemoveHeart();
        }
        else
        {
            while (_heartsContainer.GetHearts() < Lives)
                _heartsContainer.AddHeart();
        }
    }

    public void CheckState()
    {
        _healthStateController.AddLives(-1);

        if (_healthStateController.Lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
            return;
        }

        _healthStateController.SetInvincibility(false);
        _healthStateController.SetAlive(true);
        _movementStateController.CanMove = true;
        _positionController.SetConstantForwardMovement(_movementSettings.Speed.x);
    }
}