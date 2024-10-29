using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterController characterController;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravity = 10;
        [SerializeField] private GameObject _playerModel;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private Animator _animator;

        private Vector3 _moveDirection;
        private CameraController _cameraController;
        private void Awake()
        {
            _cameraController = new CameraController(Camera.main.transform);
        }

        // Update is called once per frame
        void Update()
        {
            float yStore = _moveDirection.y;
            _moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            _moveDirection.Normalize();
           // _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            _moveDirection = _moveDirection * _moveSpeed;
            _moveDirection.y = yStore;

            if (Input.GetButtonDown("Jump"))
            {
                _moveDirection.y = _jumpForce;
            }

            _moveDirection.y += Physics.gravity.y * Time.deltaTime * _gravity;

            //transform.position = transform.position + (_moveDirection * Time.deltaTime * _moveSpeed);

            characterController.Move(_moveDirection * Time.deltaTime);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                _cameraController.RotateWithPlayer(transform);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(_moveDirection.x, 0, _moveDirection.z));
                //_playerMode.transform.rotation = newRotation;

                _playerModel.transform.rotation = Quaternion.Slerp(_playerModel.transform.rotation, newRotation, _rotateSpeed * Time.deltaTime);
            }

            _animator.SetFloat("Speed", Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.z));
            _animator.SetBool("Grounded", characterController.isGrounded);
        }
    }

}
