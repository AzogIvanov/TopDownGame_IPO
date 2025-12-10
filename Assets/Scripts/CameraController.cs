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
        private float shakeMagnitude = 0f;
        private float shakeFadeSpeed = 3f;
        private Vector3 shakeOffset;

        private void Update()
        {
            if (PauseMenu.GameIsPaused) return;
            if (FinishLevelMenu.GameIsPaused) return;
            if (playerTransform == null) return;

            HandleCameraShake();

            // Calculate mouse position in world coordintes then calculates displacement depending on difference between mouse and player position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cameraDisplacement = (mousePosition - playerTransform.position) * displacementMultiplier;

            // Determine final camera position and assign it
            Vector3 finalCameraPosition = playerTransform.position + cameraDisplacement + shakeOffset;

            finalCameraPosition.z = zPosition;
            transform.position = finalCameraPosition;
        }

        private float shakeTime = 0f;

        private void HandleCameraShake()
        {
            if (shakeDuration > 0)
            {
                shakeTime += Time.deltaTime * 10f; // velocidad del ruido
                float x = (Mathf.PerlinNoise(shakeTime, 0f) - 0.5f) * 2f * shakeMagnitude;
                float y = (Mathf.PerlinNoise(0f, shakeTime) - 0.5f) * 2f * shakeMagnitude;
                shakeOffset = new Vector3(x, y, 0f);
                shakeDuration -= Time.deltaTime * shakeFadeSpeed;
            }
            else
            {
                shakeOffset = Vector3.zero;
                shakeDuration = 0;
                shakeTime = 0;
            }
        }


        public void Shake(float duration, float magnitude)
        {
            shakeDuration = duration;
            shakeMagnitude = magnitude;
        }
    }
}
