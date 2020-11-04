using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Miniclip/GameConfig")]
public class GameConfig : ScriptableObject
{
    public enum GameMode
    {
        Solo,
        Versus
    }

    public GameMode gameMode = GameMode.Solo;
    public int initialLives = 5;
    public float initialPieceSpeed = 4;
    public float cameraOffset = 15;
    public Material[] skyboxies;
    public Material currentSkybox;
}