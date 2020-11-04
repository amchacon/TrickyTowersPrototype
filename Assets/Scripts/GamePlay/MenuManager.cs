using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button soloButton;
    [SerializeField] private Button versusButton;
    public GameConfig gameConfig;

    private void Awake()
    {
        RenderSettings.skybox = gameConfig.currentSkybox = gameConfig.skyboxies[Random.Range(0, gameConfig.skyboxies.Length)];
        DynamicGI.UpdateEnvironment();
    }

    private void OnEnable()
    {
        soloButton.onClick.AddListener(OnSoloModePressed);
        versusButton.onClick.AddListener(OnVersusModePressed);
    }

    private void OnDisable()
    {
        soloButton.onClick.RemoveAllListeners();
        versusButton.onClick.RemoveAllListeners();
    }

    private void OnSoloModePressed()
    {
        gameConfig.gameMode = GameConfig.GameMode.Solo;
        StartCoroutine(LoadingSceneAsync("SoloMode"));
    }

    private void OnVersusModePressed()
    {
        gameConfig.gameMode = GameConfig.GameMode.Versus;
        StartCoroutine(LoadingSceneAsync("VersusMode"));
    }

    private IEnumerator LoadingSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
