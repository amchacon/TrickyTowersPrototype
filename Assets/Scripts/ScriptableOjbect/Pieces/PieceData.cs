using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "Miniclip/Piece/PieceData")]
public class PieceData : ScriptableObject
{
    public PieceShape shapeID;
    public GameObject prefab;
    public Sprite icon;
}
