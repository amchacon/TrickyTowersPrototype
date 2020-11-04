using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PieceSpawner : MonoBehaviour
{
    public GameConfig gameConfig;
    public List<PieceData> pieceDatas;
    public float delayInSeconds;
    
    [Header("GameEvents")]
    [Space(10)]
    public GameEvent OnPieceSpawned;
    
    [Header("SyncVars")]
    [Space(10)]
    [SerializeField] private IntValue currentMilestoneHeight;
    [SerializeField] private FloatValue currentPieceSpeed;
    public SpriteValue nextPieceSprite;
    public BoolValue paused;

    private WaitForSeconds spawnDelay;
    private Vector3 spawnPosition;
    private bool canSpawn;
    private PieceData NextPieceData { get; set; }

    public void EnableSpawner()
    {
        currentPieceSpeed.value = gameConfig.initialPieceSpeed;
        spawnPosition = Vector3.zero;
        spawnPosition.y = currentMilestoneHeight.value + gameConfig.cameraOffset;
        //if (gameConfig.gameMode == GameConfig.GameMode.Versus)
        //    spawnPosition.z = 10;
        spawnDelay = new WaitForSeconds(delayInSeconds);
        NextPieceData = GetRandomData();
        canSpawn = true;
        SpawnPiece(); //Spawns the first piece
    }

    public void DisableSpawner()
    {
        StopAllCoroutines();
        canSpawn = false;
        nextPieceSprite.value = null;
    }

    public void OnMilestoneReachedHandler()
    {
        StopAllCoroutines();
        canSpawn = false;
        currentPieceSpeed.value++; //TODO: Hard code :( I need to change that!
        StartCoroutine(StartingNewMilestone());
    }

    private PieceData GetRandomData() 
    {
        var sortedIndex = Random.Range(0, pieceDatas.Count);
        var data = pieceDatas[sortedIndex];
        nextPieceSprite.value = data.icon;
        OnPieceSpawned.Raise();
        return data;
    }

    public void SpawnPiece()
    {
        if (NextPieceData == null || !canSpawn || paused.value) return;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        //TODO: Play spawn VFX/particles
        yield return spawnDelay;
        var currentPieceData = NextPieceData;
        var pieceController = Instantiate(currentPieceData.prefab, spawnPosition, Quaternion.identity).GetComponent<PieceController>();
        ActivePieces.AddNewPiece(pieceController);
        NextPieceData = GetRandomData();
    }

    private IEnumerator StartingNewMilestone()
    {
        yield return spawnDelay;
        EnableSpawner();
    }
}
