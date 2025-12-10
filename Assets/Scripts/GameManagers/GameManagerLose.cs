using UnityEngine;
using System.Collections;

public class GameManagerLose : MonoBehaviour
{
    public bool playerIsDead = false;
    public FinishLevelMenu finishMenu;

    private bool loseTriggered = false;

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
        if (finishMenu != null)
            finishMenu.Lose();
    }
}
