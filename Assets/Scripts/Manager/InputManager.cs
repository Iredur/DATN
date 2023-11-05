using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerControl playerControl;
    private void Awake()
    {
        if (_playerController == null)
            _playerController = FindObjectOfType<PlayerController>();
        //for C# events
        playerControl = new PlayerControl();
        playerControl.Player.Enable();
        
        playerControl.Player.Dodge.performed += _playerController.Dodge;
        
        playerControl.Player.Shoot.started += _playerController.Shoot_started;
        playerControl.Player.Shoot.performed += _playerController.Shoot;
        playerControl.Player.Shoot.canceled += _playerController.Shoot_cancelled;

        playerControl.Player.Aim.performed += _playerController.Aim;

    }

    private void Update()
    {
        _playerController.inputVector = playerControl.Player.Movement.ReadValue<Vector2>();
        _playerController.mousePosition = playerControl.Player.Aim.ReadValue<Vector2>();
    }
}
