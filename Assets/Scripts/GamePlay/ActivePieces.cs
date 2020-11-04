using System.Collections.Generic;
using System.Linq;

public static class ActivePieces
{
    private static HashSet<PieceController> activePieces = new HashSet<PieceController>();

    internal static int GetHighestActivePiece()
    {
        int highestHeight = 0;
        foreach (var p in activePieces)
        {
            var pHeight = p.transform.GetColliderHighetsY();
            if (pHeight > highestHeight)
                highestHeight = pHeight;
        }
        return highestHeight;
    }

    internal static void AddNewPiece(PieceController piece)
    {
        activePieces.Add(piece);
//#if UNITY_EDITOR
//        Debug.LogWarning("Count Pieces: " + activePieces.Count);
//        Debug.LogWarning("Highest Height (active pieces): " + GetHighestActivePiece());
//#endif
    }

    internal static void RemovePiece(PieceController controller)
    {
        var pieceToRemove = activePieces.FirstOrDefault(p => p == controller);
        activePieces.Remove(pieceToRemove);
    }
}