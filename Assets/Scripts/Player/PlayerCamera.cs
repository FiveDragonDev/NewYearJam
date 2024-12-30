using UnityEngine;

public sealed class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField, Min(0)] private float _sensetivity = 1;

    private float _angle;
    private Vector2 _additionalAngle;
    private Vector2 _viewInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PlayerInput.Input.Camera.Enable();
    }
    private void Update()
    {
        HandleInputs();

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation, Quaternion.Euler(
            (_angle + _additionalAngle.y) * Vector3.right +
            Vector3.forward * _additionalAngle.x),
            Time.deltaTime * 12);

        _player.Rotate(_sensetivity * _viewInput.x * Vector3.up);
    }
    
    public void SetAdditionalAngle(Vector2 angle) => _additionalAngle = angle;

    private void HandleInputs()
    {
        _viewInput = PlayerInput.Input.Camera.View.ReadValue<Vector2>();
        _angle += -_viewInput.y * _sensetivity;
        _angle = Mathf.Clamp(_angle, -75, 75);
    }
}
