using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private LevelData levelData;

    [SerializeField]
    private RectTransform canvas,
        contentBox;

    [SerializeField]
    private Piece piecePrefab;

    [SerializeField]
    private Image bg;

    private List<Piece> pieces = new List<Piece>();

    void Start()
    {
        bg.sprite = levelData.BackgroundImage;

        Instantiate(levelData.HintManager, canvas).anchoredPosition = Vector2.zero;

        FillPieces();
        FillContent();
    }

    private void FillPieces()
    {
        int correctPieces = 0;
        for (int i = 0; i < levelData.CorrectPieces.Length; i++)
        {
            Piece piece = Instantiate(piecePrefab);
            piece.Init(i, levelData.CorrectPieces[i]);
            pieces.Add(piece);
            correctPieces++;
        }
        for (int i = 0; i < levelData.IncorrentPieces.Length; i++)
        {
            Piece piece = Instantiate(piecePrefab);
            piece.Init(i + correctPieces, levelData.IncorrentPieces[i]);
            pieces.Add(piece);
        }
    }

    private void FillContent()
    {
        pieces.Shuffle();
        foreach (var piece in pieces)
        {
            piece.SetDropped(contentBox);
        }
    }
}
