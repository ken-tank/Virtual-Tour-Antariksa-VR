using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitle;

    public void EnableSubtitle(bool status)
    {
        if (subtitle) subtitle.gameObject.SetActive(status);
    }

    public void SetSubtitle(string text) 
    {
        subtitle.text = text;
    }

    public void CreateSubtitle(string text)
    {
        SetSubtitle(text);
        EnableSubtitle(true);
    }

    public void HideSubtitle()
    {
        SetSubtitle("");
        EnableSubtitle(false);
    }
}
