using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

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

    public UnityEngine.SceneManagement.Scene GetActiveScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }

    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        GameManager.instance.FindBlocksInScene();
    }

    public void ReloadCurrentScene()
    {
        int currentScene = GetActiveScene().buildIndex;
        LoadScene(currentScene);
    }

    public void LoadNextScene()
    {
        int currentScene = GetActiveScene().buildIndex;
        int nextScene = (currentScene + 1) % UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        LoadScene(nextScene);
    }

}
