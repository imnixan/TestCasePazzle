using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuGameStarter : MonoBehaviour
{
    [SerializeField]
    private LevelData[] levels;

    [SerializeField]
    private TextMeshProUGUI timeRecord;

    [SerializeField]
    private Transform contentBox;

    [SerializeField]
    private Button buttonPrefab;

    private void Start()
    {
        float record = PlayerPrefs.GetInt(StaticData.RecordPrefs, int.MaxValue);
        if (record < int.MaxValue)
        {
            timeRecord.text = string.Format(
                "{0:d2} : {1:d2}",
                (int)(record / 60),
                (int)(record % 60)
            );
        }
        else
        {
            timeRecord.text = "";
        }
        FillLevels();
    }

    private void FillLevels()
    {
        int levelNum;
        for (int i = 0; i < levels.Length; i++)
        {
            levelNum = i;
            Button levelButton = Instantiate(buttonPrefab, contentBox);
            levelButton.image.sprite = levels[i].Preview;

            levelButton.onClick.AddListener(() => StartLevel(levelNum));
        }
    }

    public void StartLevel(int levelNum)
    {
        StaticData.LevelData = levels[levelNum];
        SceneManager.LoadSceneAsync("GameScene");
    }
}
