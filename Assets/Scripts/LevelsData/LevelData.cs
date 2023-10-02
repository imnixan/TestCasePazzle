using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public Sprite Preview;
    public Sprite BackgroundImage;
    public RectTransform HintManager;
    public Sprite[] CorrectPieces;
    public Sprite[] IncorrentPieces;
}
