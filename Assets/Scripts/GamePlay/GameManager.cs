using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameConfig gameConfig;

    [Header("Game Events")]
    [Space(10)]
    public GameEvent OnGameStarted;
    public GameEvent OnLivesChanged;
    public GameEvent OnGameOver;
    [Header("SyncVars")]
    [Space(10)]
    [SerializeField] private IntValue currentLives;

    private void Awake()
    {
        currentLives.value = gameConfig.initialLives;
        RenderSettings.skybox = gameConfig.currentSkybox;
        DynamicGI.UpdateEnvironment();
    }

    private void Start()
    {
        OnGameStarted.Raise();
    }

    public void OnPieceDestroyedHandler()
    {
        currentLives.value = Mathf.Clamp(currentLives.value -= 1, 0, gameConfig.initialLives);
        OnLivesChanged.Raise();
        if (currentLives.value <= 0)
        {
            OnGameOver.Raise();
        }
    }
}
