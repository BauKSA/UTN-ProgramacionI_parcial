using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalInputController : MonoBehaviour
{
    public static GlobalInputController Instance { get; private set; }
    private GlobalInput _controller;

    // Controllers
    public GlobalInput.PlayerInputActions PlayerInputActions => _controller.PlayerInput;
    public GlobalInput.MenuInputActions MenuInputActions => _controller.MenuInput;

    public enum InputContext
    {
        GAMEPLAY,
        MENU
    }

    public enum PlayerMovementType
    {
        LIMITED,
        ALL_RANGE
    }

    private InputContext _context;
    public IPlayerActions PlayerActions { get; private set; }
    public IMenuActions MenuActions { get; private set; }
    public event System.Action OnPlayerMovementChanged;

    public PlayerMovementType PlayerMovement { get; private set; }

    public void SetLimitedMovement(PlayerMovementType value)
    {
        PlayerMovement = value;
        OnPlayerMovementChanged?.Invoke();
    }

    private class PlayerActionsWrapper : IPlayerActions
    {
        private readonly GlobalInput.PlayerInputActions _map;
        public PlayerActionsWrapper(GlobalInput.PlayerInputActions map) => _map = map;

        public InputAction RotateRight => _map.RotateRigth;
        public InputAction RotateLeft => _map.RotateLeft;
        public InputAction MoveForward => _map.MoveForward;
        public InputAction MoveBack => _map.MoveBack;
        public InputAction MoveRight => _map.MoveRight;
        public InputAction MoveLeft => _map.MoveLeft;
        public InputAction MoveUp => _map.MoveUp;
        public InputAction MoveDown => _map.MoveDown;
        public InputAction Shoot => _map.Shoot;
    }

    private class MenuActionsWrapper : IMenuActions
    {
        private readonly GlobalInput.MenuInputActions _map;
        public MenuActionsWrapper(GlobalInput.MenuInputActions map) => _map = map;
        public InputAction Start => _map.Start;
        public InputAction Exit => _map.Exit;
        public InputAction Controls => _map.Controls;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _controller = new GlobalInput();
        PlayerActions = new PlayerActionsWrapper(_controller.PlayerInput);
        MenuActions = new MenuActionsWrapper(_controller.MenuInput);
        PlayerMovement = PlayerMovementType.LIMITED;

        SetContext(InputContext.MENU);
    }

    private void OnDestroy()
    {
        if (Instance != this) return;
        _controller.Dispose();
    }

    public void SetContext(InputContext context)
    {
        _controller.asset.Disable();
        _context = context;

        ManageContext();
    }

    private void ManageContext()
    {
        switch (_context)
        {
            case InputContext.GAMEPLAY:
                _controller.PlayerInput.Enable();
                break;
            case InputContext.MENU:
                _controller.MenuInput.Enable();
                break;
            default:
                break;
        }
    }

    
}