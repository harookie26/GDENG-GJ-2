using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenuScript : MonoBehaviour
{
    [Header("UI References")]
    public GameObject mainMenuPanel;
    public GameObject loadingScreenPanel;
    public Button playButton;
    public Button exitButton;

    [Header("Intro Video")]
    public VideoPlayer introVideoPlayer;

    [Header("Loading Video")]
    public VideoPlayer loadingVideoPlayer;

    [Header("Game Scene")]
    public string gameSceneName = "MainGame";

    void Awake()
    {
        Debug.Log("MainMenuScript Awake: Initializing UI and video panels.");
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (loadingScreenPanel != null) loadingScreenPanel.SetActive(false);
        if (introVideoPlayer != null) introVideoPlayer.gameObject.SetActive(false);
        if (loadingVideoPlayer != null) loadingVideoPlayer.gameObject.SetActive(false);

        if (playButton != null) playButton.onClick.AddListener(OnPlayPressed);
        if (exitButton != null) exitButton.onClick.AddListener(OnExitPressed);
    }

    void OnPlayPressed()
    {
        Debug.Log("Play button pressed.");
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (introVideoPlayer != null)
        {
            Debug.Log("Starting intro video.");
            introVideoPlayer.gameObject.SetActive(true);
            introVideoPlayer.Play();
            introVideoPlayer.loopPointReached += OnIntroVideoFinished;
        }
        else
        {
            Debug.Log("No intro video found, proceeding to loading screen.");
            ShowLoadingAndLoadGame();
        }
    }

    void OnIntroVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Intro video finished.");
        vp.loopPointReached -= OnIntroVideoFinished;
        vp.gameObject.SetActive(false);
        ShowLoadingAndLoadGame();
    }

    void ShowLoadingAndLoadGame()
    {
        Debug.Log("Showing loading screen and starting loading video.");
        if (loadingScreenPanel != null) loadingScreenPanel.SetActive(true);
        if (loadingVideoPlayer != null)
        {
            loadingVideoPlayer.gameObject.SetActive(true);
            loadingVideoPlayer.isLooping = true;
            loadingVideoPlayer.Play();
            Debug.Log("Loading video started and looping.");
        }
        StartCoroutine(LoadGameSceneAsync());
    }

    System.Collections.IEnumerator LoadGameSceneAsync()
    {
        Debug.Log($"Begin loading scene: {gameSceneName}");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameSceneName);
        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading progress: {asyncLoad.progress * 100f:F1}%");
            yield return null;
        }
        Debug.Log("Scene loaded, stopping loading video.");
        if (loadingVideoPlayer != null)
        {
            loadingVideoPlayer.Stop();
            loadingVideoPlayer.gameObject.SetActive(false);
        }
    }

    void OnExitPressed()
    {
        Debug.Log("Exit button pressed. Quitting application.");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
