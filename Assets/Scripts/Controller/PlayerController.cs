using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity = 10;
    
    private Vector3 _moveDirection;

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = _moveDirection.y;
        _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0 , Input.GetAxisRaw("Vertical"));
        _moveDirection = _moveDirection * _moveSpeed;
        _moveDirection.y = yStore;

        if (Input.GetButtonDown("Jump"))
        {
            _moveDirection.y = _jumpForce;
        }

        _moveDirection.y += Physics.gravity.y * Time.deltaTime * _gravity;

        //transform.position = transform.position + (_moveDirection * Time.deltaTime * _moveSpeed);

        characterController.Move(_moveDirection * Time.deltaTime);
    }
}
