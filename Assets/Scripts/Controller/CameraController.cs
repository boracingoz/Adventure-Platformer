using UnityEngine;

namespace Controller
{
    public class CameraController
    {
        private Transform _camTransform;

        public CameraController(Transform camTransform)
        {
            _camTransform = camTransform;
        }

        public void RotateWithPlayer(Transform playerTransform)
        {
            playerTransform.rotation = Quaternion.Euler(0f,_camTransform.rotation.eulerAngles.y, 0f);
        }
    }
}