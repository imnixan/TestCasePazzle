using System.Collections;
using UnityEngine;
using TMPro;

public class TimerUi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerCount;

    public void ShowTime(int time)
    {
        int secs = time % 60;
        int minutes = time / 60;
        string timeText = string.Format("{0:d2} : {1:d2}", minutes, (secs));
        timerCount.text = timeText;
    }
}
