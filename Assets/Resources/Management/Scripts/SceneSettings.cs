using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] AudioClip musicClip = null;
    [SerializeField] float musicVolume = 1;
    [SerializeField] Color dimColor = Color.black;

    [Space(20)]
    public UnityEvent onStartScene;

    void Start() 
    {
        Time.timeScale = 1;
        onStartScene.Invoke();
        if (musicClip != null) PlayMusic();
        
        SetMusicVolume(musicVolume);
    }

    public void Pause(bool value)
    {
        Time.timeScale = value ? 0 : 1;
        GameManager.instance.audioManager.voice.pitch = value ? 0 : 1;
    }

    public void PlayMusic()
    {
        GameManager.instance.audioManager.PlayMusic(musicClip, true);
    }

    public void SetMusicVolume(float value)
    {
        IEnumerator Slide() 
        {
            float time = 0, t =0;
            float duration = 1;
            float startVolume = GameManager.instance.audioManager.music.volume;
            while (time <= duration)
            {
                time += Time.deltaTime;
                t = time/duration;
                GameManager.instance.audioManager.music.volume = Mathf.Lerp(startVolume, value, t);
                yield return null;
            }
            GameManager.instance.audioManager.music.volume = value;
        }
        
        StartCoroutine(Slide());
    }

    public void StopMusic()
    {
        GameManager.instance.audioManager.music.Stop();
    }

    bool onLoading = false;
    public void LoadScene(string SceneName)
    {
        async void load()
        {
            onLoading = true;
            AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);
            operation.allowSceneActivation = false;

            await GameManager.instance.frontCanvas.Dim(1, dimColor);

            while (true)
            {
                if (operation.progress >= 0.9f)
                {
                    break;
                }

                await Task.Yield();
            }

            operation.allowSceneActivation = true;
            await GameManager.instance.frontCanvas.Dim(0, dimColor);
            onLoading = false;
        }

        if (onLoading == false)
        {
            load();
        }
    }
    public void LoadScene(int build)
    {
        async void load()
        {
            onLoading = true;
            AsyncOperation operation = SceneManager.LoadSceneAsync(build);
            operation.allowSceneActivation = false;

            await GameManager.instance.frontCanvas.Dim(1, dimColor);

            while (true)
            {
                if (operation.progress >= 0.9f)
                {
                    break;
                }

                await Task.Yield();
            }

            operation.allowSceneActivation = true;
            await GameManager.instance.frontCanvas.Dim(0, dimColor);
            onLoading = false;
        }

        if (onLoading == false)
        {
            load();
        }
    }

    public void RestartScene() 
    {
        string name = SceneManager.GetActiveScene().name;
        LoadScene(name);
    }

    public void NextScene() 
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        LoadScene(index + 1);
    }
}
