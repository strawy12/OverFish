using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    Rigidbody _rigid;
    Camera _mainCam;

    Vector3 _currentDir = Vector3.zero;

    [SerializeField] float _currentVelocity = 0f;
    [SerializeField] float _speed = 10f;
    [SerializeField] float _acceleration = 50f;
    [SerializeField] float _deAcceleration = 50f;
    [SerializeField] float _rotateMoveSpeed = 2f;

    bool _isRun = false;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _mainCam = Camera.main;
    }

    public void Move(Vector3 movement)
    {
        _currentDir = movement;

        Vector3 velocity = _currentDir * _currentVelocity;
        velocity.y = _rigid.velocity.y;

        _rigid.velocity = velocity;
        PlayerMovement(_currentDir);
    }
    protected float CalculateSpeed(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0f)
        {
            _currentVelocity += _acceleration * Time.deltaTime;
        }

        else
        {
            _currentVelocity -= _deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0f, _speed);
    }
    public void PlayerMovement(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0) // 움직이고 있을 때
        {
            Vector3 currentDir = _currentDir;
            currentDir.y = 0f;

            if (Vector3.Dot(currentDir, movementInput) < 0)
            {
                _currentVelocity = 0f;
            }
            _currentDir = Vector3.RotateTowards(_currentDir, movementInput, _rotateMoveSpeed * Time.deltaTime, 1000f);
            _currentDir.Normalize();
            RotatePlayer();
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }
    public void RotatePlayer()
    {
        if (_currentVelocity > 0f)
        {
            Vector3 newForward = _rigid.velocity;
            newForward.y = 0f;

            transform.forward = Vector3.Lerp(transform.forward, newForward, _rotateMoveSpeed * Time.deltaTime);
        }
    }
}
