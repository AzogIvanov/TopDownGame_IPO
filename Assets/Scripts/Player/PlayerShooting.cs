using TopDown.CameraControl;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    public Transform gunPointNormal;       // Punto para armas normales
    public Transform gunPointShotgun;      // Punto para escopeta
    private Transform currentGunPoint;     // Punto activo de disparo

    public GameObject bulletPrefab; // Prefab de la bala
    public PlayerController playerController; // Tiene HasShotgun
    public CameraController cameraController;

    [Header("Settings")]
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;

    // Ajustes escopeta
    public int shotgunPellets = 5;
    public float shotgunSpread = 15f;
    public float shotgunBulletSpeed = 15f;
    public float shotgunBulletLife = 0.5f;

    private float nextFireTime = 0f;

    void Start()
    {
        // Por defecto usamos el punto normal
        currentGunPoint = gunPointNormal;
    }

    void Update()
    {
        if (playerController == null) return;

        // Cambiar gunPoint según arma equipada
        if (playerController.HasShotgun)
            currentGunPoint = gunPointShotgun;
        else
            currentGunPoint = gunPointNormal;

        // Disparo
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (playerController.HasShotgun)
        {
            cameraController.Shake(0.30f);
            // ESCOPETA
            for (int i = 0; i < shotgunPellets; i++)
            {
                float angleOffset = Random.Range(-shotgunSpread, shotgunSpread);
                Quaternion rotation = currentGunPoint.rotation * Quaternion.Euler(0, 0, angleOffset);

                GameObject bullet = Instantiate(bulletPrefab, currentGunPoint.position, rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                float randomSpeed = Random.Range(shotgunBulletSpeed * 0.85f, shotgunBulletSpeed * 1.15f);
                rb.linearVelocity = rotation * Vector2.up * randomSpeed;

                Bullet b = bullet.GetComponent<Bullet>();
                if (b != null)
                    b.lifeTime = shotgunBulletLife;

            }
        }
        else
        {
            // ARMA NORMAL
            GameObject bullet = Instantiate(bulletPrefab, currentGunPoint.position, currentGunPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = currentGunPoint.up * bulletSpeed;
        }
    }
}
