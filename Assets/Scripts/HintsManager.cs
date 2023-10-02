using System.Collections.Generic;
using UnityEngine;

public class HintsManager : MonoBehaviour
{
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

    public void ShowNextHint(int prevId)
    {
        transform.GetChild(prevId).gameObject.SetActive(false);
        if (prevId >= transform.childCount)
        {
            Debug.Log("EndGame");
        }
        else
        {
            transform.GetChild(prevId++).gameObject.SetActive(true);
        }
    }
}
