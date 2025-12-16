using UnityEngine;
using System.Collections;

public class GameManagerLose : MonoBehaviour
{
    public bool playerIsDead = false;
    public FinishLevelMenu finishMenu;

    private bool loseTriggered = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip losingSoundClip;
    public float volumeLosingSound = 0.3f;

    void Update()
    {
        if (loseTriggered) return;

        if (PlayerHealth.isDead)
        {
            loseTriggered = true;
            StartCoroutine(TriggerLoseWithDelay(2f));
        }
    }

    IEnumerator TriggerLoseWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Game Over — HAS MUERTO");

        if (audioSource != null && losingSoundClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(losingSoundClip, volumeLosingSound);
        }

        if (finishMenu != null)
            finishMenu.Lose();
    }
}
