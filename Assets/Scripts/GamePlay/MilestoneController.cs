using System.Collections;
using UnityEngine;

public class MilestoneController : MonoBehaviour
{
    //TODO: change it for a Data SO?? 
    [SerializeField] private int firstMilestoneHeight = 10;
    [SerializeField] private int difficultyFactor = 10;
    [SerializeField] private float highestHeightThreshould = 0.05f;
    [SerializeField] private Transform winningFlag; //TODO: maybe change it for a prefab instantiated in runtime to avoid ref to obj in scene
    
    [Header("Game Events")]
    [Space(10)]
    public GameEvent OnHeightIncreased;
    public GameEvent OnMilestoneReached;
    
    [Header("SyncVars")]
    [Space(10)]
    [SerializeField] private IntValue currentTowerHeight;
    [SerializeField] private IntValue currentMilestoneHeight;

    private void Awake()
    {
        currentTowerHeight.value = 0;
        currentMilestoneHeight.value = firstMilestoneHeight;
        winningFlag.position = new Vector3(0, currentMilestoneHeight.value, 0);
    }

    public void OnPiecePlacedHandler()
    {
        var height = ActivePieces.GetHighestActivePiece();

        if (height > currentTowerHeight.value + highestHeightThreshould)
        {
            currentTowerHeight.value = height;
            OnHeightIncreased.Raise();
        }

        if (height > currentMilestoneHeight.value)
        {
            currentMilestoneHeight.value += difficultyFactor;
            StartCoroutine(MoveWinningMarker());
            OnMilestoneReached.Raise();
        }
    }

    private IEnumerator MoveWinningMarker()
    {
        yield return new WaitForEndOfFrame();
        //TODO: use an object and tween this movement
        winningFlag.position = new Vector3(0, currentMilestoneHeight.value, 0);
    }
}
