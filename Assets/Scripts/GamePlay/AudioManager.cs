using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class AudioManager : MonoBehaviour
{
    private AudioSource[] soundFXSources = new AudioSource[4];
    private AudioSource[] musicSources = new AudioSource[2];
    private int soundFXIndex = 0;
    private int musicIndex = 0;
    public float musicVolume = 0.4f;
    public float soundFXVolume = 1.0f;

    //Move it to another place (probably an SO too)
    #region Audio Clips
    [Header("Audio Clips")]
    [SerializeField] private AudioClip theme;
    [SerializeField] private AudioClip gameStarted;
    [SerializeField] private AudioClip piecePlaced;
    [SerializeField] private AudioClip pieceRotated;
    [SerializeField] private AudioClip pieceDestroyed;
    [SerializeField] private AudioClip milestoneReached;
    [SerializeField] private AudioClip newHighScore;
    [SerializeField] private AudioClip gameOver;
    #endregion

    private void Awake()
    {
        for (int i = 0; i < soundFXSources.Length; i++)
        {
            soundFXSources[i] = gameObject.AddComponent<AudioSource>();
            soundFXSources[i].loop = false;
            soundFXSources[i].volume = soundFXVolume;
        }

        for (int i = 0; i < musicSources.Length; i++)
        {
            musicSources[i] = gameObject.AddComponent<AudioSource>();
            musicSources[i].loop = true;
            musicSources[i].volume = musicVolume;
        }
    }

    private void Start()
    {
        PlayMusic(theme);
    }

    private void PlaySoundFX(AudioClip clip)
    {
        if (!clip)
            return;
        soundFXSources[soundFXIndex].PlayOneShot(clip);
        soundFXIndex = ++soundFXIndex % soundFXSources.Length;
    }

    private void PlayMusic(AudioClip clip)
    {
        if (!clip)
            return;

        musicIndex = ++musicIndex % musicSources.Length;
        musicSources[musicIndex].clip = clip;
        musicSources[musicIndex].Play();
        musicSources[musicIndex].volume = musicVolume;
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < soundFXSources.Length; i++)
        {
            soundFXSources[i].Stop();
            soundFXSources[i].clip = null;
        }

        for (int i = 0; i < musicSources.Length; i++)
        {
            musicSources[i].Stop();
            musicSources[i].clip = null;
        }
    }

    #region Game Event Handlers
    public void OnGameOverHandler()
    {
        PlaySoundFX(gameOver);
    }

    public void OnPiecePlacedHandler()
    {
        PlaySoundFX(piecePlaced);
    }

    public void OnPieceDestroyedHandler()
    {
        PlaySoundFX(pieceDestroyed);
    }
    public void OnRotateHandler()
    {
        PlaySoundFX(pieceRotated);
    }

    public void OnMilestoneReachedHandler()
    {
        PlaySoundFX(milestoneReached);
    }

    public void OnNewHighScoreReachedHandler()
    {
        PlaySoundFX(newHighScore);
    }

    public void OnGameStartedHandler()
    {
        PlaySoundFX(gameStarted);
    }
    #endregion
}