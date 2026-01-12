using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    [Header("Score")]
    public TMP_Text scoreTMP;
    public TMP_Text highScoreTMP;

    [Header("hearts")]
    public GameObject[] heartContainerPrefabs;
    HeartContainter[] hearts;

    [Header("Menus")]
    public GameObject GameOverScreen;
    public GameObject[] menus;

    [Header("   ---  Menu  ---   ")]
    public RectTransform menu;
    public float menuSpeed;
    public float offscreenMargin = 20f;
    public AnimationCurve menuEasing = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public bool startHidden = true;
    RectTransform menuParent;
    Vector2 menuAnchoredPos;
    Vector2 menuInAnchoredPos;
    Vector2 menuOutAnchoredPos;
    Vector2 lastParentSize;

    [Header("   ---  ConfigMenu  ---   ")]
    public Dropdown dificultyDropdown;


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
        scoreTMP.text = "Score: 0";
        highScoreTMP.text = "High Score:\n" + PlayerPrefs.GetFloat("HighScore", 0);

        hearts = new HeartContainter[heartContainerPrefabs.Length];
        for (int i = 0; i < heartContainerPrefabs.Length; i++)
        {
            hearts[i] = heartContainerPrefabs[i].GetComponent<HeartContainter>();
        }

        if (menu != null)
        {
            menuParent = menu.parent as RectTransform;
            menuAnchoredPos = menu.anchoredPosition;
            RecalculateMenuAnchoredPositions();

            if (startHidden)
            {
                menu.anchoredPosition = menuOutAnchoredPos;
                menu.gameObject.SetActive(false);
            }
            else
            {
                GameManager.instance.PauseGame();
            }
        }

        dificultyDropdown.onValueChanged.AddListener(delegate { GameManager.instance.ChangeDificulty(dificultyDropdown.value); });
        GameManager.instance.gameDificulty = (GameManager.Dificulty)PlayerPrefs.GetInt("Dificulty", 1);
    }

    void Update()
    {
        scoreTMP.text = "Score:\n" + GameManager.instance.Score.ToString();
        if (GameManager.instance.Score > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", GameManager.instance.Score);
            highScoreTMP.text = "High Score:\n" + GameManager.instance.Score.ToString();
        }

        if (menuParent != null)
        {
            Vector2 currentSize = menuParent.rect.size;
            if (currentSize != lastParentSize)
            {
                RecalculateMenuAnchoredPositions();
                lastParentSize = currentSize;
            }
        }
    }

    public void TriggerUpdateHealth()
    {
        StartCoroutine(UpdateHealth());
    }

    private IEnumerator UpdateHealth()
    {
        int currentHealth = GameManager.instance.PlayerLives;
        bool shieldActive = GameManager.instance.shieldBuffActive;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].TriggerBlink();
        }

        yield return new WaitUntil(() => !hearts[0].isBlinking && !hearts[1].isBlinking && !hearts[2].isBlinking);

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                if (shieldActive)
                    hearts[i].SetHeartShield();
                else
                    hearts[i].SetHeartFull();
            }
            else
            {
                hearts[i].SetHeartEmpty();
            }
        }
    }

    public void ShowMenu()
    {
        if (menu == null) return;
        menu.gameObject.SetActive(true);
        StartCoroutine(AnimateMenuIn());
    }

    private IEnumerator AnimateMenuIn()
    {
        Vector2 startPos = menu.anchoredPosition;
        Vector2 targetPos = menuInAnchoredPos;
        float elapsed = 0f;

        while (elapsed < menuSpeed)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / menuSpeed);
            float eased = menuEasing.Evaluate(t);
            menuAnchoredPos = Vector2.Lerp(startPos, targetPos, eased);
            menu.anchoredPosition = menuAnchoredPos;
            yield return null;
        }

        menu.anchoredPosition = targetPos;
    }

    public void HideMenu()
    {
        if (menu == null) return;
        StartCoroutine(AnimateMenuOut());
    }

    private IEnumerator AnimateMenuOut()
    {
        Vector2 startPos = menu.anchoredPosition;
        Vector2 targetPos = menuOutAnchoredPos;
        float elapsed = 0f;

        while (elapsed < menuSpeed)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / menuSpeed);
            float eased = menuEasing.Evaluate(t);
            menuAnchoredPos = Vector2.Lerp(startPos, targetPos, eased);
            menu.anchoredPosition = menuAnchoredPos;
            yield return null;
        }

        menu.anchoredPosition = targetPos;
        menu.gameObject.SetActive(false);
    }

    private void RecalculateMenuAnchoredPositions()
    {
        if (menu == null || menuParent == null) return;

        float parentHalfWidth = menuParent.rect.width * 0.5f;
        float menuHalfWidth = menu.rect.width * 0.5f;

        float offscreenX = parentHalfWidth + menuHalfWidth + offscreenMargin;

        menuInAnchoredPos = new Vector2(0f, menu.anchoredPosition.y);
        menuOutAnchoredPos = new Vector2(-offscreenX, menu.anchoredPosition.y);
        lastParentSize = menuParent.rect.size;
    }

    public void SetMenuActive(int menuIndex)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        menus[menuIndex].SetActive(true);
    }

    public void SetMenuActive0()
    {
        SetMenuActive(0);
    }
}
