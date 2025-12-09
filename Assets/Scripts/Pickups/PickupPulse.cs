using UnityEngine;

public class PickupPulse : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float pulseAmount = 0.05f;  // cuánto se expande/reduce (5%)
    public float pulseSpeed = 2f;      // velocidad del pulso

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        float scaleOffset = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = initialScale + Vector3.one * scaleOffset;
    }
}
