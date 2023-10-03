using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    private LevelData levelData;

    [SerializeField]
    private RectTransform canvas,
        contentBox;

    [SerializeField]
    private Piece piecePrefab;

    [SerializeField]
    private Image bg;

    [SerializeField]
    private Timer timer;

    [SerializeField]
    private SlideBar slideBar;

    private List<Piece> pieces = new List<Piece>();

    void Start()
    {
        levelData = StaticData.LevelData;
        bg.sprite = levelData.BackgroundImage;

        Instantiate(levelData.HintManager, canvas).anchoredPosition = Vector2.zero;
        slideBar.Init(levelData.CorrectPieces.Length);
        FillPieces();
        FillContent();
        timer.StartTimer();
    }

    private void FillPieces()
    {
        int correctPieces = 0;
        float contentBoxHeight = contentBox.rect.height;
        for (int i = 0; i < levelData.CorrectPieces.Length; i++)
        {
            Piece piece = Instantiate(piecePrefab);
            piece.Init(i, levelData.CorrectPieces[i], contentBoxHeight);
            pieces.Add(piece);
            correctPieces++;
        }
        for (int i = 0; i < levelData.IncorrentPieces.Length; i++)
        {
            Piece piece = Instantiate(piecePrefab);
            piece.Init(i + correctPieces, levelData.IncorrentPieces[i], contentBoxHeight);
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
