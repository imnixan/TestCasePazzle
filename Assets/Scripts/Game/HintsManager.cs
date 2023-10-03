using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HintsManager : MonoBehaviour
{
    private GameSceneManager gameSceneManager;

    public static event UnityAction HintsEnd;

    private void Start()
    {
        GameObject hint = InitHint(0);
        for (int i = 1; i < transform.childCount; i++)
        {
            hint = InitHint(i);
            hint.SetActive(false);
        }
    }

    private GameObject InitHint(int hintIndex)
    {
        GameObject hint = transform.GetChild(hintIndex).gameObject;
        hint.GetComponent<Hint>().Init(hintIndex, this);
        return hint;
    }

    public void ShowNextHint(int currentId)
    {
        currentId++;

        if (currentId >= transform.childCount)
        {
            HintsEnd?.Invoke();
        }
        else
        {
            transform.GetChild(currentId).gameObject.SetActive(true);
            transform.GetChild(currentId).GetComponent<Hint>().Show();
        }
    }
}
