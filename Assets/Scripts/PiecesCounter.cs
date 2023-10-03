using System.Collections;
using UnityEngine;
using TMPro;

public class PiecesCounter : MonoBehaviour
{
    private TextMeshProUGUI piecesCountText;
    private int finishedPieces;
    private int totalPieces;

    public void Init(int totalCorrectPieces)
    {
        totalPieces = totalCorrectPieces;
        piecesCountText = GetComponentInChildren<TextMeshProUGUI>();
        UpdatePiecesCount();
    }

    private void OnPiecePlaced()
    {
        finishedPieces++;
        UpdatePiecesCount();
    }

    private void UpdatePiecesCount()
    {
        piecesCountText.text = $"{finishedPieces} / {totalPieces}";
    }

    private void OnEnable()
    {
        Piece.PieceOnPlace += OnPiecePlaced;
    }

    private void OnDisable()
    {
        Piece.PieceOnPlace -= OnPiecePlaced;
    }
}
