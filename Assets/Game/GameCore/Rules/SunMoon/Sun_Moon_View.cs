using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class Sun_Moon_View : MonoBehaviour
{
    [SerializeField] private GameObject Sun;
    [SerializeField] private TextMeshProUGUI SunCount;

    [SerializeField] private GameObject Moon;
    [SerializeField] private TextMeshProUGUI MoonCount;

    [SerializeField] private SpriteRenderer background;

    public RuleDayTime ruleDay; 

    private Color sunColor = new Color(0.86f, 0.94f, 1.0f);
    private Color moonColor = new Color(0.0f, 0.16f, 0.42f);

    private void Awake()
    {
        ruleDay = new RuleDayTime(RuleDayTime.Time.Sun);
        ruleDay.changeCountTime += SetCountTime;
        ruleDay.changeDayTime += SetDayTime;

        SetDayTime(ruleDay.DayTime);
        SetCountTime(ruleDay.TimeCount);
    }

    private void SetDayTime(string state)
    {
        switch (state)
        {
            case RuleDayTime.Time.Sun:
                {
                    Debug.Log("Now Sun");

                    Sun.SetActive(true);
                    Moon.SetActive(false);

                    SetBackground(sunColor);
                }
                return;
            case RuleDayTime.Time.Moon: 
                {
                    Debug.Log("Now Moon");

                    Moon.SetActive(true);
                    Sun.SetActive(false);

                    SetBackground(moonColor);
                }
                return;
        }


        throw new NotImplementedException();
    }

    private void SetBackground(Color color)
    {
        background.DOColor(color, 0.5f);
    }

    private void SetCountTime(int count)
    {
       SunCount.text = count.ToString();
       MoonCount.text = count.ToString();
    }

    private void OnDestroy()
    {
        ruleDay.changeCountTime -= SetCountTime;
        ruleDay.changeDayTime -= SetDayTime;
    }

}

public class RuleDayTime
{
    public class Time
    {
        public const string Sun = "Sun";
        public const string Moon = "Moon";
    }

    private int timeCount;
    public int TimeCount
    {
        private set { 
            timeCount = value;
            changeCountTime?.Invoke(timeCount);
            checkDayTime(); 
        }
        get { return timeCount; }
    }
    public string dayTime;
    
    public string DayTime
    { 
        private set 
        { 
            dayTime = value;
            changeDayTime?.Invoke(dayTime);
        }
        get { return dayTime; } 
    }

    public Action<string> changeDayTime;
    public Action<int> changeCountTime;

    private System.Random random = new System.Random();

    public RuleDayTime(string dT)
    {
        DayTime = dT;
        TimeCount = random.Next(3, 12);
    }

    private void checkDayTime()
    {
        if (TimeCount == 0)
        {
            if (DayTime == Time.Sun)
            {
                DayTime = Time.Moon;
            }
            else { DayTime = Time.Sun; }

            TimeCount = random.Next(3, 12);
        }
    }

    public void DecriseTimeCount()
    {
        TimeCount--;
    }
};

