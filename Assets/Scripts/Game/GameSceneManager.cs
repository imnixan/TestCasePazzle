using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform endGameTable;

    [SerializeField]
    private Timer timer;

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMenu();
        }
    }

    private void OnHintsEnd()
    {
        timer.Stop();
        int time = timer.GetTime();
        if (time < PlayerPrefs.GetInt(StaticData.RecordPrefs + StaticData.LevelNum, int.MaxValue))
        {
            PlayerPrefs.SetInt(StaticData.RecordPrefs + StaticData.LevelNum, time);
            PlayerPrefs.Save();
        }
        endGameTable.DOAnchorPos(Vector2.zero, StaticData.AnimationLength).PlayForward();
    }

    private void OnEnable()
    {
        HintsManager.HintsEnd += OnHintsEnd;
    }

    private void OnDisable()
    {
        HintsManager.HintsEnd -= OnHintsEnd;
    }
}
