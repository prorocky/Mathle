using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DailyTimer : MonoBehaviour
{
    DateTime current;
    DateTime tomorrow;

    TimeSpan timeUntilNextDay;

    public Text timerCountdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current = DateTime.Now;
        tomorrow = DateTime.Now.AddDays(1).Date;
        timeUntilNextDay = (tomorrow - current);
        timerCountdown.text = timeUntilNextDay.ToString("hh':'mm':'ss");
    }
}
