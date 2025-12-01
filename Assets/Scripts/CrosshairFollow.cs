using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        Cursor.visible = false; // Oculta cursor nativo
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Mantener en 2D
        transform.position = mousePos;
    }
}
