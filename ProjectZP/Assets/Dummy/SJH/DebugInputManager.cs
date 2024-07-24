using UnityEngine;
using UnityEngine.InputSystem;

public class DebugInputManager : MonoBehaviour
{
    [SerializeField] private CharacterController _playerCharacter;
    private const float MOVE_SPEED = 0.05f;
    private const float ROTATE_SPEED = 0.3f;

    private Vector3 _moveDirection;

    private void Awake()
    {
        if (_playerCharacter == null)
            _playerCharacter = transform.parent.GetComponent<CharacterController>();
    }
    private void Update()
    {
        _playerCharacter.Move(_moveDirection * MOVE_SPEED);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();

        _moveDirection = new Vector3(input.x, 0f, input.y);
        _moveDirection = transform.TransformDirection(_moveDirection);
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        if (input == null)
            return;

        var xRotation = _playerCharacter.transform.eulerAngles.x - input.y * ROTATE_SPEED;
        var yRotation = _playerCharacter.transform.eulerAngles.y + input.x * ROTATE_SPEED;

        _playerCharacter.transform.eulerAngles = new Vector3(xRotation, yRotation, 0f);
    }
}
