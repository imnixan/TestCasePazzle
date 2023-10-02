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

    public void EndGame()
    {
        timer.Stop();
        int time = timer.GetTime();
        if (time < PlayerPrefs.GetInt(StaticData.RecordPrefs, int.MaxValue))
        {
            PlayerPrefs.SetInt(StaticData.RecordPrefs, time);
            PlayerPrefs.Save();
        }
        endGameTable.DOAnchorPos(Vector2.zero, StaticData.AnimationSpeed).PlayForward();
    }
}
