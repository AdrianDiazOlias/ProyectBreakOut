using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    public TMP_Text scoreTMP;
    public TMP_Text highScoreTMP;

    public GameObject[] heartContainerPrefab;
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
        UpdateHealth(GameManager.instance.PlayerLives);
        scoreTMP.text = "Score: 0";
        highScoreTMP.text = "High Score: " + PlayerPrefs.GetFloat("HighScore", 0);
    }

    void Update()
    {
        scoreTMP.text = "Score: " + GameManager.instance.Score.ToString();
        if (GameManager.instance.Score > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", GameManager.instance.Score);
            highScoreTMP.text = "High Score: " + GameManager.instance.Score.ToString();
        }
    }

    void FixedUpdate()
    {
        GameManager.instance.Score += 1f;
    }

    public void UpdateHealth(int currentHealth)
    {
        if (currentHealth == 3)
        {
            heartContainerPrefab[0].SetActive(true);
            heartContainerPrefab[1].SetActive(true);
            heartContainerPrefab[2].SetActive(true);
        }
        else if (currentHealth == 2)
        {
            heartContainerPrefab[0].SetActive(true);
            heartContainerPrefab[1].SetActive(true);
            heartContainerPrefab[2].SetActive(false);
        }
        else if (currentHealth == 1)
        {
            heartContainerPrefab[0].SetActive(true);
            heartContainerPrefab[1].SetActive(false);
            heartContainerPrefab[2].SetActive(false);
        }
        else
        {
            heartContainerPrefab[0].SetActive(false);
            heartContainerPrefab[1].SetActive(false);
            heartContainerPrefab[2].SetActive(false);
        }
    }
}
