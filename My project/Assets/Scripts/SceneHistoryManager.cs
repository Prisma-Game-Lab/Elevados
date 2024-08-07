using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHistoryManager : MonoBehaviour
{
    public static SceneHistoryManager instance;

    private string previousScene;

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

    public void SetPreviousScene(string sceneName)
    {
        previousScene = sceneName;
    }

    public string GetPreviousScene()
    {
        return previousScene;
    }
}
