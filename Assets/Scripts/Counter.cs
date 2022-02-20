using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] int secondsToCount = 3;
    [SerializeField] Text text;
    public Action OnCounterEnded;
    float elapsed = 0f;
    int counter = 0;

    public void Reset()
    {
        counter = 0;
        elapsed = 1;
        text.text = secondsToCount.ToString();
        text.gameObject.SetActive(true);
    }
    
    void Update()
    {
        elapsed += Time.unscaledDeltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            OutputTime();
        }
    }
    void OutputTime()
    {
        int res = secondsToCount - counter++;
        if (res <= 0)
        {
            OnCounterEnded?.Invoke();
            text.gameObject.SetActive(false);
        }
        else
        {
            text.text = res.ToString();
        }
    }
}
