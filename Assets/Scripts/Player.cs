using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _charController;
    private Vector3 _direction;
    private Vector3 _velocity;

    [SerializeField] private float _gravity = 1.0f; // in real world val is -9.81f
    [SerializeField] private float _speed = 5.0f; // meters per sec
    [SerializeField] private float _jumpHeight = 15.0f;
    [SerializeField] private float _yVelocity; // var to cache y velocity to another var

    private bool _canDoubleJump = false;


    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // 1 do all calcs..
        // get horizontal input
        float _horizontalInput = Input.GetAxis("Horizontal");

        // define direction based on this input
        _direction = new Vector3(_horizontalInput, 0, 0);

        // define velocity
        _velocity = _direction * _speed;

        // after apply velocity, chk if grounded.
        if (_charController.isGrounded == true)
        {
            // if i hit space jump; define jump height
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight; // set y velocity to 15
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_canDoubleJump == true)
                {
                    _yVelocity += _jumpHeight; // doubles jump height; dbl jump
                    _canDoubleJump = false;
                }
            }
            // apply gravity
            _yVelocity -= _gravity;
        }

        // cached vel = 15; goes back up to line 25 calc next fr
        // cached val is 14 cuz subtracted 1 from gravity
        _velocity.y = _yVelocity; 

        // 2 after calcs update...
        // call move method; MOVE based on the direction
        _charController.Move(_velocity * Time.deltaTime);
    }
}
