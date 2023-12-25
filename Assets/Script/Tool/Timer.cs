using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] [Tooltip("Lama Game dalam Detik")] int durasi;
    [SerializeField] bool useTimeFormat = true;
    [SerializeField] TextMeshProUGUI text;

    public UnityEvent onEnd;
    int startDur;

    void Awake()
    {
        startDur = durasi;
    }

    void Start()
    {
        StartCoroutine(Engine());
    }

    IEnumerator Engine() 
    {
        SetTime(durasi);
        while (true)
        {
            yield return new WaitForSeconds(1);
            durasi -= 1;
            SetTime(durasi);
            if (durasi <= 0)
            {
                break;
            }
        }
        End();
    }

    void SetTime(int second)
    {
        var waktu = TimeSpan.FromSeconds(second);
        if (useTimeFormat) text.text = $"{waktu.Hours.ToString("00")}:{waktu.Minutes.ToString("00")}:{waktu.Seconds.ToString("00")}";
        else text.text = durasi.ToString();
    }

    public void End()
    {
        StopAllCoroutines();
        SetTime(durasi);
        onEnd.Invoke();
    }

    public void ResetTimer(int value = 0)
    {
        if (value == 0) durasi = startDur;
        else durasi = value;

        StopAllCoroutines();
        SetTime(durasi);
        StartCoroutine(Engine());
    }

    public void AddTime(int value)
    {
        durasi += value;
        SetTime(durasi);
    }

    public void StopTime() 
    {
        StopAllCoroutines();
    }
}
