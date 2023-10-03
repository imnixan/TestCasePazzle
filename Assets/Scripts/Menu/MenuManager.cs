using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private LevelData[] levelsData;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private LevelsLine levelsLine;

    private List<RectTransform> buttonsRt = new List<RectTransform>();

    private void Start()
    {
        FillLevels();
        levelsLine.Init(buttonsRt);
        buttonsRt.Clear();
    }

    private void FillLevels()
    {
        int levelNum;
        for (int i = 0; i < levelsData.Length; i++)
        {
            levelNum = i;
            RectTransform level = Instantiate(buttonPrefab, levelsLine.transform)
                .GetComponent<RectTransform>();
            SetLevelImage(level, levelsData[i].Preview);
            level.anchoredPosition = new Vector2(i * level.sizeDelta.x * 1.1f, 0);

            int bestTime = PlayerPrefs.GetInt(StaticData.RecordPrefs + i, int.MaxValue);

            TextMeshProUGUI bestLevelTime = level.GetComponentInChildren<TextMeshProUGUI>();
            if (bestTime < int.MaxValue)
            {
                bestLevelTime.text = string.Format("{0:d2} : {1:d2}", bestTime / 60, bestTime % 60);
            }
            else
            {
                bestLevelTime.text = "";
            }

            Button levelButton = level.GetComponentInChildren<Button>();
            levelButton.onClick.AddListener(() => StartLevel(levelNum));
            buttonsRt.Add(level);
        }
    }

    private void SetLevelImage(RectTransform level, Sprite levelPreview)
    {
        Image levelImage = level.GetComponent<Image>();
        levelImage.sprite = levelPreview;
        Vector2 textureSize = new Vector2(
            levelImage.sprite.texture.width,
            levelImage.sprite.texture.height
        );
        float maxWidth = level.parent.GetComponent<RectTransform>().sizeDelta.x;
        float maxHeight = textureSize.y / (textureSize.x / maxWidth);
        level.sizeDelta = new Vector2(maxWidth, maxHeight);
    }

    private void StartLevel(int levelNum)
    {
        StaticData.LevelNum = levelNum;
        StaticData.LevelData = levelsData[levelNum];
        levelsLine.HideButtons();
        Sequence endAnim = DOTween
            .Sequence()
            .Append(
                levelsLine
                    .GetComponent<RectTransform>()
                    .DOAnchorPosY(1000, StaticData.AnimationLength)
            )
            .AppendCallback(() =>
            {
                SceneManager.LoadSceneAsync("GameScene");
            });
    }
}
