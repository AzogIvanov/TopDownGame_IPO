using UnityEngine;
using System.Collections;

public class GameManagerVictory : MonoBehaviour
{
    public bool bunkerDestroyed = false;
    public FinishLevelMenu finishMenu;

    private bool victoryTriggered = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip winningSoundClip;
    public float volumeWinningSound = 0.3f;

    void Update()
    {
        if (victoryTriggered) return;

        BunkerHealth[] bunkers = GameObject.FindObjectsByType<BunkerHealth>(FindObjectsSortMode.None);
        EnemyHealth[] enemies = GameObject.FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
        ExplosiveEnemyHealth[] explosiveEnemies = GameObject.FindObjectsByType<ExplosiveEnemyHealth>(FindObjectsSortMode.None);
        BossEnemyHealth[] bossEnemies = GameObject.FindObjectsByType<BossEnemyHealth>(FindObjectsSortMode.None);
        GooTank[] gooTanks = GameObject.FindObjectsByType<GooTank>(FindObjectsSortMode.None);

        bool allDestroyed = true;
        bool allEnemiesDead = true;
        bool allExplosiveEnemiesDead = true;
        bool allBossEnemiesDead = true;
        bool allGooTankDead = true;

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
                allEnemiesDead = false;
                break;
            }
        }

        foreach (var explosiveEnemie in explosiveEnemies)
        {
            if (!explosiveEnemie.isDead)
            {
                allExplosiveEnemiesDead = false;
                break;
            }
        }

        foreach (var bossEnemie in bossEnemies)
        {
            if (!bossEnemie.isDead)
            {
                allBossEnemiesDead = false;
                break;
            }
        }

        foreach (var gooTank in gooTanks)
        {
            if (!gooTank.isDestroyed)
            {
                allGooTankDead = false;
                break;
            }
        }

        if (allEnemiesDead && allDestroyed && allExplosiveEnemiesDead && allBossEnemiesDead && allGooTankDead)
        {
            victoryTriggered = true;
            StartCoroutine(TriggerVictoryWithDelay(2f));
        }
    }

    IEnumerator TriggerVictoryWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Victoria — ZONA DESPEJADA");

        if (audioSource != null && winningSoundClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(winningSoundClip, volumeWinningSound);
        }

        if (finishMenu != null)
            finishMenu.Win();
    }
}
