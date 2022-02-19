using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] int secondsToCount = 3;
    [SerializeField] Text text;
    float timePassed = 0;
    private float milisecondsToCount = 0;
    private int counter = 1;
    private const int MILISECONDS_IN_SECOND = 1000;
    public Action onCounterEnded;

    void Awake()
    {
        milisecondsToCount = secondsToCount * MILISECONDS_IN_SECOND;
        text.text = secondsToCount.ToString();
    }

    void Reset()
    {
        
    }
    
    void Update()
    {
        if (timePassed < milisecondsToCount)
        {
            timePassed += Time.unscaledTime;
            if (timePassed > counter * MILISECONDS_IN_SECOND)
            {
                counter++;
                secondsToCount--;
                if (secondsToCount == 0)
                {
                    gameObject.SetActive(false);
                    onCounterEnded?.Invoke();
                }
                else
                {
                    text.text = secondsToCount.ToString();
                }
            }
        }
    }
}
