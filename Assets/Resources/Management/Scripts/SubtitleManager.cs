using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitle;
    [SerializeField] TextMeshPro subtitle3D;

    public void EnableSubtitle(bool status)
    {
        if (subtitle) subtitle.gameObject.SetActive(status);
        if (subtitle3D) subtitle3D.gameObject.SetActive(status);
    }

    public void SetSubtitle(string text) 
    {
        if (subtitle) subtitle.text = text;
        if (subtitle3D) subtitle3D.text = text;
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
