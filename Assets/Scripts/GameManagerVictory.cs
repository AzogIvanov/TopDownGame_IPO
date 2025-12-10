using UnityEngine;
using System.Collections;

public class GameManagerVictory : MonoBehaviour
{
    public bool bunkerDestroyed = false;
    public FinishLevelMenu finishMenu;

    private bool victoryTriggered = false;

    void Update()
    {
        if (victoryTriggered) return;

        BunkerHealth[] bunkers = GameObject.FindObjectsByType<BunkerHealth>(FindObjectsSortMode.None);
        EnemyHealth[] enemies = GameObject.FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

        bool allDestroyed = true;
        bool allDead = true;

        foreach (var bunker in bunkers)
        {
            if (!bunker.isDestroyed)
            {
                allDestroyed = false;
                break;
            }
        }

        foreach (var enemie in enemies)
        {
            if (!enemie.isDead)
            {
                allDead = false;
                break;
            }
        }

        if (allDead && allDestroyed)
        {
            victoryTriggered = true;
            StartCoroutine(TriggerVictoryWithDelay(2f));
        }
    }

    IEnumerator TriggerVictoryWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Victoria — ZONA DESPEJADA");
        if (finishMenu != null)
            finishMenu.Win();
    }
}
