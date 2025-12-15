using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum WeaponType
    {
        None = 0,
        Shotgun = 1,
        Rifle = 2,
        GrenadeLauncher = 3
    }

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    Vector2 movement;

    [Header("References")]
    public Camera cam;
    public Animator animator;

    Rigidbody2D rb;

    public bool shotgunAvailable = true;
    public bool rifleAvailable = true;
    public bool grenadeLauncherAvailable = true;

    public WeaponType currentWeapon = WeaponType.None;

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
        // CAMBIO DE ARMA
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(WeaponType.None);
        if (Input.GetKeyDown(KeyCode.Alpha2) && shotgunAvailable) ChangeWeapon(WeaponType.Shotgun);
        if (Input.GetKeyDown(KeyCode.Alpha3) && rifleAvailable) ChangeWeapon(WeaponType.Rifle);
        if (Input.GetKeyDown(KeyCode.Alpha4) && grenadeLauncherAvailable) ChangeWeapon(WeaponType.GrenadeLauncher);

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
            float speedPercent = movement.magnitude; // 0 quieto, 1 moviendo
            animator.SetFloat("speed", speedPercent);
            animator.SetFloat("weaponIndex", (float)currentWeapon);
        }
    }

    void FixedUpdate()
    {
        Vector2 moveDir = movement.normalized;
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    private void ChangeWeapon(WeaponType newWeapon)
    {
        // Si intenta cambiar a arma que NO tiene => ignoramos
        if (newWeapon == WeaponType.Shotgun && !shotgunAvailable) return;
        if (newWeapon == WeaponType.Rifle && !rifleAvailable) return;
        if (newWeapon == WeaponType.GrenadeLauncher && !grenadeLauncherAvailable) return;

        currentWeapon = newWeapon;
        animator.SetFloat("weaponIndex", (float)newWeapon);
        Debug.Log("Arma actual: " + newWeapon);
    }
}
