using System.Collections;
using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerCamera _camera;
    [SerializeField] private AudioSource _footstepSource;
    [SerializeField] private AudioClip[] _footstepSFXs;

    [SerializeField, Min(0)] private float _walkSpeed = 1;
    [SerializeField, Min(0)] private float _drag = 0.1f;

    private Vector2 _movementInput;

    private float _lastFootstepTime;
    private Vector3 _acceleration;

    private Rigidbody _rigidbody;

    private void Start()
    {
        PlayerInput.Input.Movement.Enable();
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        HandleInputs();
        if (_acceleration.magnitude > 1 &&
            Time.time - _lastFootstepTime > 0.8f) PlayFootstepSFX();
    }
    private void FixedUpdate()
    {
        Move();
        UpdatePhysics();
    }

    private void HandleInputs()
    {
        _movementInput = PlayerInput.Input.Movement.Move.ReadValue<Vector2>();

        _camera.SetAdditionalAngle(_movementInput * new Vector2(-1.5f, 2));
    }

    private void Move()
    {
        var forward = _movementInput.y * transform.forward;
        var right = _movementInput.x * transform.right;

        var move = (forward + right).normalized * _walkSpeed;
        _acceleration += move;

    }
    private void UpdatePhysics()
    {
        _acceleration += -_drag * _acceleration;
        _rigidbody.velocity += _acceleration * Time.fixedDeltaTime;
    }
    private void PlayFootstepSFX()
    {
        _footstepSource.PlayOneShot(_footstepSFXs[
            Random.Range(0, _footstepSFXs.Length)]);
        _lastFootstepTime = Time.time;
    }
}
