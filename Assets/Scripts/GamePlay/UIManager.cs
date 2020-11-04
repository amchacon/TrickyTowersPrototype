using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public UIData uiData;
    public SpriteValue nextPieceSprite;

    [Header("Lives Indicator"), Space(5)]
    [SerializeField] private IntValue currentLives;
    public Image[] livesImages;

    [Header("Height"), Space(5)]
    [SerializeField] private IntValue currentHighestHeight;
    public TextMeshProUGUI heightText;
    public RectTransform trophyRect;

    [Header("NextPiece"), Space(5)]
    public Image nextPieceImage;

    [Header("Menus"), Space(5)]
    public Canvas popUpCanvas;
    public TextMeshProUGUI titleText;
    public Button restartBtn;
    public Button mainMenuBtn;
    public Button pauseBtn;
    public GameObject livesIndicatorPanel;
    public BoolValue paused;

    private void Awake()
    {
        ClosePopUps();
        heightText.SetText($"{currentHighestHeight.value}m");
        foreach (Image img in livesImages)
        {
            img.sprite = uiData.liveSprite;
        }
        paused.value = false;
    }

    public void OnGameStartedHandler()
    {
        heightText.SetText($"{currentHighestHeight.value}m");
        RefreshPieceIndicator();
        ClosePopUps();
    }

    public void OnLivesChangedHandler()
    {
        livesImages[currentLives.value].transform.DOScale(1.5f, 1f).SetEase(Ease.OutElastic);
        livesImages[currentLives.value].transform.DOScale(0f, 0.5f).SetEase(Ease.InOutElastic).SetDelay(0.5f);
        livesImages[currentLives.value].DOFade(0, 0.5f).SetDelay(1f);
    }

    public void OnGameOverHandler()
    {
        nextPieceImage.enabled = false;
        titleText.SetText($"{currentHighestHeight.value * uiData.heightMultiplier}m");
        livesIndicatorPanel.SetActive(false);
        pauseBtn.gameObject.SetActive(false);
        trophyRect.gameObject.SetActive(false);
        popUpCanvas.enabled = true;
    }

    public void OnNewHighScoreReachedHandler()
    {
        trophyRect.gameObject.SetActive(true);
        heightText.SetText("New High Score!!!");
        trophyRect.DOLocalMove(new Vector3(-400, -630, 0), 1.25f).SetEase(Ease.OutBounce);
    }

    public void OnMilestoneReachedHandler()
    {
        //Some cool effect here!
    }

    public void OnHeightIncreasedHandler()
    {
        heightText.SetText($"{currentHighestHeight.value * uiData.heightMultiplier}m");
        heightText.GetComponent<RectTransform>().DOShakeScale(.4f, 1, 10, 90);
    }

    public void OnPiecePlacedHandler() => RefreshPieceIndicator();
    

    private void RefreshPieceIndicator()
    {
        nextPieceImage.DOColor(new Color(1f, 1f, 1f, 0f), 0f);
        nextPieceImage.sprite = nextPieceSprite.value;
        nextPieceImage.DOColor(Color.white, 0.5f);
    }

    private void ClosePopUps()
    {
        popUpCanvas.enabled = false;
        titleText.SetText("");
        trophyRect.gameObject.SetActive(true);
        nextPieceImage.enabled = true;
        livesIndicatorPanel.SetActive(true);
    }

    private void OnEnable()
    {
        pauseBtn.onClick.AddListener(PauseGame);
        restartBtn.onClick.AddListener(RestartScene);
        mainMenuBtn.onClick.AddListener(BackToMenu);
    }

    private void OnDisable()
    {
        pauseBtn.onClick.RemoveListener(PauseGame);
        restartBtn.onClick.RemoveListener(RestartScene);
        mainMenuBtn.onClick.RemoveListener(BackToMenu);
    }

    private void PauseGame()
    {
        if (!paused.value)
        {
            trophyRect.gameObject.SetActive(false);
            paused.value = true;
            nextPieceImage.enabled = false;
            titleText.SetText($"{uiData.pauseTitleText}");
            livesIndicatorPanel.SetActive(false);
            popUpCanvas.enabled = true;

        }
        else
        {
            paused.value = false;
            ClosePopUps();
            Time.timeScale = 1;
        }
    }

    private void BackToMenu()
    {
        StartCoroutine(LoadingSceneAsync("MainMenu"));
    }

    private void RestartScene()
    {
        paused.value = false;
        StartCoroutine(LoadingSceneAsync(SceneManager.GetActiveScene().name));
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
