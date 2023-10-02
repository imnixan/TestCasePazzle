using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TimerUi timerUi;

    private int currentTime;

    private WaitForSecondsRealtime waitForSeconds;

    public void StartTimer()
    {
        waitForSeconds = new WaitForSecondsRealtime(1);
        StartCoroutine(CountTime());
    }

    private IEnumerator CountTime()
    {
        while (true)
        {
            yield return waitForSeconds;
            currentTime++;
            timerUi.ShowTime(currentTime);
        }
    }

    public int GetTime()
    {
        return currentTime;
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}
