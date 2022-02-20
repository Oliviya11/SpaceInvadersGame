using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] int secondsToCount = 3;
    [SerializeField] Text text;
    int secondsToCountInternal;
    float timePassed = 0;
    private float milisecondsToCount = 0;
    private int counter = 1;
    private const int MILISECONDS_IN_SECOND = 1000;
    public Action onCounterEnded;
    public bool IsLaunched;

    public void Init()
    {
        Reset();
        onCounterEnded += delegate { IsLaunched = false; };
    }

    public void Reset()
    {
        milisecondsToCount = secondsToCount * MILISECONDS_IN_SECOND;
        text.text = secondsToCount.ToString();
        secondsToCountInternal = secondsToCount;
        timePassed = 0;
        counter = 1;
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        IsLaunched = true;
    }
    
    void Update()
    {
        if (!IsLaunched)
        {
            return;
        }
        
        if (timePassed < milisecondsToCount)
        {
            timePassed += Time.unscaledTime;
            if (timePassed > counter * MILISECONDS_IN_SECOND)
            {
                counter++;
                secondsToCountInternal--;
                if (secondsToCountInternal == 0)
                {
                    gameObject.SetActive(false);
                    onCounterEnded?.Invoke();
                }
                else
                {
                    text.text = secondsToCountInternal.ToString();
                }
            }
        }
    }
}
