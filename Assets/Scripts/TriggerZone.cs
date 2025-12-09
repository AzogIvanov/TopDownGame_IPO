using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [Header("Objetos a activar")]
    public GameObject[] objectsToActivate;

    [Header("Opcional: desactivar al salir")]
    public GameObject[] objectsToDeactivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Solo el jugador
        {
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null) obj.SetActive(true);
            }

            foreach (GameObject obj in objectsToDeactivate)
            {
                if (obj != null) obj.SetActive(false);
            }

            Debug.Log("Jugador entró en la zona");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador salió de la zona");
        }
    }
}
