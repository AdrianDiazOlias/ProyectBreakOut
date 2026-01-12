using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ballPrefab;
    public GameObject[] blocksInScene;

    [Header("Player Stats")]
    public int PlayerLives = 3;
    public float Score = 0;

    [Header("Game States")]
    public bool isGameOver = false;
    public bool isGamePaused = false;
    public bool isGameStarted = false;

    [Header("Game Settings")]
    public int setFrameRate = 60;
    public Dificulty gameDificulty = Dificulty.normal;
    public enum Dificulty
    {
        easy,
        normal,
        hard
    }

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

    void Start()
    {
        SceneManager.instance.LoadScene(0);
        Application.targetFrameRate = setFrameRate;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (!isGamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        FindBlocksInScene();
        if (blocksInScene.Count() == 0)
        {
            OnLevelComplete();
        }
    }

    public void OnLevelComplete()
    {
        isGameStarted = false;
        isGamePaused = false;
        ballPrefab.GetComponent<Ball>().ResetBallIntoPlayer();
        SceneManager.instance.LoadNextScene();
        Debug.Log("Level Completed!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isGameOver = false;
        goldBuffActive = false;
        shieldBuffActive = false;
        Score = 0;
        PlayerLives = 3;
        ScreenManager.instance.TriggerUpdateHealth();
        ScreenManager.instance.GameOverScreen.SetActive(false);
        isGamePaused = false;
        ScreenManager.instance.HideMenu();
        ScreenManager.instance.Invoke(nameof(ScreenManager.SetMenuActive0), 0.2f);
        SceneManager.instance.LoadScene(0);
        Debug.Log("Game Restarted");
    }

    public void PauseGame()
    {
        if (!isGamePaused && !isGameOver)
        {
            Time.timeScale = 0;
            isGamePaused = true;
            ScreenManager.instance.ShowMenu();
            Debug.Log("Game Paused");
        }
    }

    public void ResumeGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
            ScreenManager.instance.HideMenu();
            ScreenManager.instance.Invoke("SetMenuActive0", 0.2f);
            Debug.Log("Game Resumed");
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        isGameOver = true;
        ScreenManager.instance.GameOverScreen.SetActive(true);
        ScreenManager.instance.ShowMenu();
        ScreenManager.instance.SetMenuActive(2);
        Debug.Log("Game Over");
    }

    public void PlayerDamaged()
    {
        if (shieldBuffActive)
        {
            shieldBuffActive = false;
            ScreenManager.instance.TriggerUpdateHealth();
            Debug.Log("Shield absorbed the damage!");
            return;
        }

        PlayerLives--;
        ScreenManager.instance.TriggerUpdateHealth();
        Debug.Log("Player took damage. Remaining lives: " + PlayerLives);

        if (PlayerLives <= 0)
        {
            Debug.Log("Player has no lives left.");
            GameOver();
        }
    }

    public void PlayerHealed()
    {
        if (PlayerLives < 3)
        {
            PlayerLives++;
            ScreenManager.instance.TriggerUpdateHealth();
            Debug.Log("Player healed. Current lives: " + PlayerLives);
        }
        else
        {
            Debug.Log("Player is already at maximum lives.");
        }
    }

    public static void AddPoints(float puntos)
    {
        if (GameManager.instance.goldBuffActive)
        {
            puntos *= GameManager.instance.goldBuffMultiplier;
            GameManager.instance.goldBuffActive = false;
        }

        GameManager.instance.Score += puntos;
        Debug.Log("Points Added: " + puntos + " | Total Score: " + GameManager.instance.Score);
    }

    public void ChangeDificulty(int NuevaDificultad)
    {
        gameDificulty = (Dificulty)NuevaDificultad;
        PlayerPrefs.SetInt("Dificulty", NuevaDificultad);
    }

    public void FindBlocksInScene()
    {
        blocksInScene = GameObject.FindGameObjectsWithTag("Block");
    }
}
