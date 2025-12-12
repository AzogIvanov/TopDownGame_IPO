using UnityEngine;

public class ReadSign : MonoBehaviour
{
    public GameObject pressFPopup;
    public GameObject signText;

    private bool playerInRange = false;
    private bool textOpen = false;

    private void Start()
    {
        if (pressFPopup != null) pressFPopup.SetActive(false);
        if (signText != null) signText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            playerInRange = true;
            pressFPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            playerInRange = false;
            pressFPopup.SetActive(false);

            // Cerrar el texto si está abierto
            if (textOpen)
            {
                signText.SetActive(false);
                textOpen = false;
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            ToggleText();
        }
    }

    private void ToggleText()
    {
        textOpen = !textOpen;
        signText.SetActive(textOpen);
    }
}
