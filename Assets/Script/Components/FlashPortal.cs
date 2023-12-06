using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashPortal : MonoBehaviour
{
    [SerializeField] float duration = 1;
    [SerializeField] AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    IEnumerator Transtition(float scale) 
    {
        Vector3 start = scale == 1 ? Vector3.zero : Vector3.one;
        Vector3 end = scale == 1 ? Vector3.one : Vector3.zero;
        transform.localScale = start;
        float time = 0;
        while (time <= duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time/duration);
            transform.localScale = Vector3.Lerp(start, end, animationCurve.Evaluate(t));
            yield return null;
        }
        transform.localScale = end;
    }

    IEnumerator TranstitionToScene(string name)
    {
        yield return StartCoroutine(Transtition(1));
        SceneManager.LoadSceneAsync(name);
    }
    IEnumerator TranstitionToScene(int index)
    {
        yield return StartCoroutine(Transtition(1));
        SceneManager.LoadSceneAsync(index);
    }

    void Start()
    {
        StartCoroutine(Transtition(0));
    }

    public void GoToScene(string name) 
    {
        StartCoroutine(TranstitionToScene(name));
    }

    public void NextScene() 
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(TranstitionToScene(index));
    }
    public void PreviousScene() 
    {
        int index = SceneManager.GetActiveScene().buildIndex - 1;
        StartCoroutine(TranstitionToScene(index));
    }
}
