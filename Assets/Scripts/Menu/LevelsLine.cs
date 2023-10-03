using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class LevelsLine : MonoBehaviour
{
    [SerializeField]
    private Button nextLevelBtn,
        prevLevelBtn;
    private float dragDirection;
    private List<RectTransform> levels = new List<RectTransform>();
    private int _currentLevel;
    private RectTransform rt;

    public int CurrentLevel
    {
        get { return _currentLevel; }
        set
        {
            _currentLevel = value;
            if (_currentLevel >= levels.Count)
            {
                _currentLevel = 0;
            }
            if (_currentLevel < 0)
            {
                _currentLevel = levels.Count - 1;
            }
        }
    }

    public void Init(List<RectTransform> levels)
    {
        rt = GetComponent<RectTransform>();
        this.levels.AddRange(levels);
        InitButtons();

        SetDefaultLevel();
    }

    private void InitButtons()
    {
        if (levels.Count < 2)
        {
            nextLevelBtn.gameObject.SetActive(false);
            prevLevelBtn.gameObject.SetActive(false);
        }
        else
        {
            RectTransform nextRt = nextLevelBtn.GetComponent<RectTransform>();
            RectTransform prevRt = prevLevelBtn.GetComponent<RectTransform>();

            nextRt.anchoredPosition = new Vector2(rt.sizeDelta.x + nextRt.sizeDelta.x, 0);
            prevRt.anchoredPosition = nextRt.anchoredPosition * -1;
        }
    }

    public void NextLevel()
    {
        CurrentLevel++;
        ShowItem();
    }

    public void PrevLevel()
    {
        CurrentLevel--;
        ShowItem();
    }

    private void SetDefaultLevel()
    {
        int totalLevels = levels.Count;
        for (int i = 0; i < totalLevels; i++)
        {
            if (PlayerPrefs.GetInt(StaticData.RecordPrefs + i, int.MaxValue) == int.MaxValue)
            {
                CurrentLevel = i;
                ShowItem();
                break;
            }
        }
    }

    public void HideButtons()
    {
        nextLevelBtn.gameObject.SetActive(false);
        prevLevelBtn.gameObject.SetActive(false);
    }

    public void ShowItem()
    {
        RectTransform item = levels[CurrentLevel];
        rt.DOAnchorPosX(-item.anchoredPosition.x, StaticData.AnimationLength);
    }
}
