using System;
using UnityEngine;

public class Space : MonoBehaviour
{
    public static Space instance;

    public float spaceTime = 1;
    [Tooltip("Space Scale Unit per Kilometer")]
    public float spaceScale = 1000;
    
    [System.Serializable]
    public class Time {
        public int year, month, day, hour, minutes, second;
    }

    public Time time;
    TimeSpan timeSpan;

    void Awake()
    {
        instance = this;
        s = getSecond();
    }

    double s;
    void Update()
    {
        s += UnityEngine.Time.deltaTime * spaceTime;
        timeSpan = TimeSpan.FromSeconds(s);
        time.day = timeSpan.Days;
        time.hour = timeSpan.Hours;
        time.minutes = timeSpan.Minutes;
        time.second = timeSpan.Seconds;
    }

    void FixedUpdate()
    {
        Shader.SetGlobalFloat("_CloudSpeedMult", spaceTime);
    }

    void AddTime(int Y, int M, int D, int h, int m, int s) 
    {
        time.year += Y;
        time.month += M;
        time.day += D;
        time.hour += h;
        time.minutes += m;
        time.second += s;

        if (time.second >= 60) 
        {
            time.minutes ++ ; 
            time.second = 0;
        }
        if (time.minutes >= 60) 
        {
            time.hour ++ ; 
            time.minutes = 0;
        }
        if (time.hour >= 24) 
        {
            time.day ++ ; 
            time.hour = 0;
        }
        if (time.day >= 30) 
        {
            time.month ++ ; 
            time.day = 0;
        }
        if (time.month >= 12) 
        {
            time.year ++ ; 
            time.month = 0;
        }
    }

    public void SetTime(int Y, int M, int D, int h, int m, int s)
    {
        time.year = Y;
        time.month = M;
        time.day = D;
        time.hour = h;
        time.minutes = m;
        time.second = s;
    }

    double getSecond() 
    {
        double v = 0;
        v += time.day * 86400;
        v += time.hour * 3600;
        v += time.minutes * 60;
        v += time.second;
        return v;
    }
}
