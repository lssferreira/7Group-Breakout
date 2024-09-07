public class InputManager
{
    private PlayerControls _playerControls;

    public float Movement => _playerControls.Gameplay.Movement.ReadValue<float>();

    public InputManager()
    {
        _playerControls = new PlayerControls();
        _playerControls.Gameplay.Enable();
    }
}