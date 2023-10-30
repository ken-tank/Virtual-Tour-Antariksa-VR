using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class FrontCanvas : MonoBehaviour
{
    public CanvasGroup fade;

    void Start() 
    {
        DontDestroyOnLoad(gameObject);
    }

    public async Task Dim(float alphaValue, Color? color = null) 
    {
        try
        {
            float time = 0, t = 0;
            float duration = 0.5f;

            if (alphaValue == 1) fade.gameObject.SetActive(true);

            float startAlpha = alphaValue == 0 ? 1 : 0;

            if (color != null) fade.GetComponent<Image>().color = color.Value;
            
            while (time <= duration)
            {
                time += Time.unscaledDeltaTime;
                t = time/duration;
                fade.alpha = Mathf.Lerp(startAlpha, alphaValue, t);
                await Task.Yield();
            }

            fade.alpha = alphaValue;
            if (alphaValue == 0) fade.gameObject.SetActive(false);
        } catch {}
    }
}
