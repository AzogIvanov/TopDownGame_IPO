using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : MonoBehaviour
{
    public PlayerController.WeaponType weaponToUnlock;
    public GameObject pressEPopup; // UI "Pulsa E para recoger"

    private bool playerInRange = false;
    private PlayerController player;

    void Start()
    {
        if (pressEPopup != null)
            pressEPopup.SetActive(false); // Ocultar texto al inicio
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            playerInRange = true;
            if (pressEPopup != null)
                pressEPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            playerInRange = false;
            if (pressEPopup != null)
                pressEPopup.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickupWeapon();
        }

        // --- Efecto flotante del popup ---
        if (pressEPopup != null && pressEPopup.activeSelf)
        {
            RectTransform rectTransform = pressEPopup.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                float yOffset = Mathf.Sin(Time.time * 2f) * 10f; // Ajusta el 10f según necesites
                Vector2 newPos = rectTransform.anchoredPosition;
                newPos.y = yOffset + 50f;
                rectTransform.anchoredPosition = newPos;
            }
        }
    }

    private void PickupWeapon()
    {
        // Activar arma en PlayerController
        if (weaponToUnlock == PlayerController.WeaponType.Shotgun)
            player.shotgunAvailable = true;

        if (weaponToUnlock == PlayerController.WeaponType.Rifle)
            player.rifleAvailable = true;

        // Equipar automáticamente
        player.currentWeapon = weaponToUnlock;
        player.animator.SetFloat("weaponIndex", (float)weaponToUnlock);

        // Ocultar popup y destruir el objeto del suelo
        if (pressEPopup != null)
            pressEPopup.SetActive(false);

        Destroy(gameObject);
    }
}
