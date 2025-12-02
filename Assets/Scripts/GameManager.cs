using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Player Stats")]
    public int PlayerLives = 3;
    public float Score = 0;

    [Header("Game States")]
    public bool isGameOver = false;
    public bool isGamePaused = false;
    public bool isGameStarted = false;
    public bool isLevelCompleted = false;

    [Header("Buffs & Debuffs")]
    [Space(5)]
    [Header("    — Gold Buff —")]
    public bool goldBuffActive = false;
    public float goldBuffMultiplier = 2f;
    [Space(5)]
    [Header("    — Shield Buff —")]
    public bool shieldBuffActive = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void OnLevelComplete()
    {
        isLevelCompleted = true;
    }

    public void ResetGame()
    {
        isGameOver = false;
        isLevelCompleted = false;
        Score = 0;
        PlayerLives = 3;
        goldBuffActive = false;
        shieldBuffActive = false;
        Time.timeScale = 1;
        Debug.Log("Game Reset");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        Debug.Log("Game Resumed");
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over");
    }

    public void PlayerDamaged()
    {
        if (shieldBuffActive)
        {
            shieldBuffActive = false;
            Debug.Log("Shield absorbed the damage!");
            return;
        }

        PlayerLives--;
        Debug.Log("Player took damage. Remaining lives: " + PlayerLives);

        if (PlayerLives <= 0)
        {
            Debug.Log("Player has no lives left.");
            GameOver();
        }
    }
}
