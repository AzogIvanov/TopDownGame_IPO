using TopDown.CameraControl;
using UnityEngine;
using static PlayerController;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    public Transform gunPointNormal;
    public Transform gunPointShotgun;
    public Transform gunPointARifle;
    private Transform currentGunPoint;

    public GameObject bulletPrefab;
    public PlayerController playerController;
    public CameraController cameraController;

    [Header("Settings")]
    public float bulletSpeed = 10f;

    [Header("Fire Rates")]
    public float normalFireRate = 0.5f;
    public float shotgunFireRate = 1f;
    public float rifleFireRate = 0.1f;

    [Header("Shotgun")]
    public int shotgunPellets = 5;
    public float shotgunSpread = 15f;
    public float shotgunBulletSpeed = 15f;
    public float shotgunBulletLife = 0.5f;

    [Header("Assault Rifle")]
    public int aRiflePellets = 1;
    public float aRifleSpread = 8f;
    public float aRifleBulletSpeed = 20f;
    public float aRifleBulletLife = 0.5f;

    [Header("Grenade Launcher")]
    public GameObject grenadePrefab;
    public float grenadeFireRate = 1.2f;
    public float grenadeLaunchForce = 12f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shotgunShotReloadClip;
    public float volumeShotgun = 0.3f;
    public AudioClip machinegunShotClip;
    public float volumeMachinegun = 0.3f;
    public AudioClip grenadeLauncherShotClip;
    public float volumeGrenadeLauncher = 0.3f;


    private float nextFireTime = 0f;

    void Start()
    {
        // Por defecto usamos el punto normal
        currentGunPoint = gunPointNormal;
    }

    void Update()
    {
        if (playerController == null) return;

        switch (playerController.currentWeapon)
        {
            // Cambiar gunPoint según arma equipada
            case WeaponType.Shotgun:
            currentGunPoint = gunPointShotgun;
                break;
            case WeaponType.Rifle:
            currentGunPoint = gunPointARifle;
                break;
            case WeaponType.GrenadeLauncher:
                currentGunPoint = gunPointShotgun;
                break;
            default:
            currentGunPoint = gunPointNormal;
                break;
    }

        if (playerController.currentWeapon == WeaponType.None)
            return;

        // Disparo
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + GetCurrentFireRate();
        }
    }

    void Shoot()
    {


        switch (playerController.currentWeapon)
        {
            case WeaponType.Shotgun:
                cameraController.Shake(0.4f, 0.5f);
                ShootShotgun();
                break;

            case WeaponType.Rifle:
                cameraController.Shake(0.40f, 0.09f);
                ShootRifle();
                break;

            case WeaponType.GrenadeLauncher:
                cameraController.Shake(0.40f, 0.09f);
                ShootGrenade();
                break;

            default: // None
                break;
        }

    }

    float GetCurrentFireRate()
    {
        switch (playerController.currentWeapon)
        {
            case WeaponType.Shotgun: return shotgunFireRate;
            case WeaponType.Rifle: return rifleFireRate;
            case WeaponType.GrenadeLauncher: return grenadeFireRate;
            default: return normalFireRate;
        }
    }


    void ShootShotgun()
    {
        if (audioSource != null && shotgunShotReloadClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(shotgunShotReloadClip, volumeShotgun);
        }

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

    void ShootRifle()
    {
        if (audioSource != null && machinegunShotClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(machinegunShotClip, volumeMachinegun);
        }

        float angleOffset = Random.Range(-aRifleSpread, aRifleSpread);
        Quaternion rotation = currentGunPoint.rotation * Quaternion.Euler(0, 0, angleOffset);

        GameObject bullet = Instantiate(bulletPrefab, currentGunPoint.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.linearVelocity = rotation * Vector2.up * aRifleBulletSpeed;

        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
            b.lifeTime = aRifleBulletLife;
    }

    void ShootGrenade()
    {
        if (audioSource != null && grenadeLauncherShotClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(grenadeLauncherShotClip, volumeGrenadeLauncher);
        }

        GameObject grenade = Instantiate(grenadePrefab, currentGunPoint.position, currentGunPoint.rotation);
        Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 dir = currentGunPoint.up.normalized;

            rb.AddForce(dir * grenadeLaunchForce, ForceMode2D.Impulse);
        }
    }

}
