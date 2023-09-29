using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public Sprite[] Hints;
    public Sprite[] CorrectPieces;
    public Sprite[] IncorrentPieces;
}
