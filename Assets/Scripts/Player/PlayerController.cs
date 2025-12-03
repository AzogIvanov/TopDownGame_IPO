using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    Vector2 movement;

    [Header("References")]
    public Camera cam;
    public Animator animator;

    Rigidbody2D rb;

    // Control de arma
    private bool hasShotgun = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (cam == null)
            cam = Camera.main;

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // --- Cambiar arma ---
        if (Input.GetKeyDown(KeyCode.Tab)) // ahora con Tab
        {
            hasShotgun = !hasShotgun; // alterna el modo escopeta
            if (animator != null)
                animator.SetBool("hasShotgun", hasShotgun);

            Debug.Log("Cambió modo escopeta: " + hasShotgun); // debug para consola
        }

        // --- MOVIMIENTO WASD ---
        movement.x = Input.GetAxisRaw("Horizontal");  // A-D
        movement.y = Input.GetAxisRaw("Vertical");    // W-S

        // --- ROTACIÓN HACIA EL RATÓN ---
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90f; // Ajuste según sprite

        // --- ANIMACIÓN ---
        if (animator != null)
        {
            bool isMoving = movement != Vector2.zero;
            animator.SetBool("isMoving", isMoving);
        }
    }

    void FixedUpdate()
    {
        Vector2 moveDir = movement.normalized;
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    public bool HasShotgun { get { return hasShotgun; } }
}
