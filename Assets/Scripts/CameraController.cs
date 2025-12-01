using UnityEngine;

namespace TopDown.CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float displacementMultiplier = 0.15f;

        private float zPosition = -10;

        // SHAKE
        private float shakeDuration = 0f;
        private float shakeMagnitude = 0.45f;
        private float shakeFadeSpeed = 3f;
        private Vector3 shakeOffset;

        private void Update()
        {
            HandleCameraShake();

            // Calculate mouse position in world coordintes then calculates displacement depending on difference between mouse and player position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cameraDisplacement = (mousePosition - playerTransform.position) * displacementMultiplier;

            // Determine final camera position and assign it
            Vector3 finalCameraPosition = playerTransform.position + cameraDisplacement + shakeOffset;

            finalCameraPosition.z = zPosition;
            transform.position = finalCameraPosition;
        }

        private void HandleCameraShake()
        {
            if (shakeDuration > 0)
            {
                shakeOffset = Random.insideUnitCircle * shakeMagnitude;
                shakeDuration -= Time.deltaTime * shakeFadeSpeed;
            }
            else
            {
                shakeDuration = 0;
                shakeOffset = Vector3.zero;
            }
        }

        public void Shake(float duration)
        {
            shakeDuration = duration;
        }
    }
}
