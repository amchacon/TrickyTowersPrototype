using UnityEngine;

//TODO: Persistency should be treated in a specific component
public class HighScoreController : MonoBehaviour
{
    [Header("Game Events")]
    public GameEvent OnNewHighScoreReached;
    
    [Header("SyncVars")]
    public IntValue currentTowerHeight;

    public void OnGameOverHandler()
    {
        var previous = PlayerPrefs.GetInt("HighScore");
        if(currentTowerHeight.value > previous)
        {
            //new high score!!!
            PlayerPrefs.SetInt("HighScore", currentTowerHeight.value);
            OnNewHighScoreReached.Raise();
        }
    }
}
