using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KKSpeech;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Command : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultText;

    Dictionary<string, string[]> commands = new Dictionary<string, string[]>()
    {
        {"quit", new string[] {
            "keluar aplikasi", "tutup aplikasi", "aplikasi keluar", "aplikasi tutup",
            "keluar dari aplikasi", "keluar", "tutup", "close", "quit"
        }},
        {"next scene", new string[] {
            "skip level", "level selanjutnya", "scene selanjutnya", "next scene",
            "next level", "skip level ini"
        }}
    };

    [System.Serializable] 
    public class CustomCommand {
        public string name;
        public string[] commands;
        public UnityEvent onResult;
    }

    public CustomCommand[] customCommands;

    void Awake()
    {
        SpeechRecognizerListener listener = FindObjectOfType<SpeechRecognizerListener>();
        listener.onFinalResults.AddListener(CalculateResult);
        listener.onErrorDuringRecording.AddListener(OnError);
        SpeechRecognizer.RequestAccess();
    }

    public void CalculateResult(string value) 
    {
        Debug.Log(value.ToLower());
        Result(value.ToLower());
        FindObjectOfType<VoiceListener>().ResetUI();
        GameSettings.audioMixer.SetFloat("Voice_Volume", 0);
    }

    void Result(string value) 
    {
        StartCoroutine(Resulting(value));
    }

    IEnumerator Resulting(string value) 
    {
        float delay = 1.5f;
        resultText.gameObject.SetActive(true);

        if (commands["quit"].Contains(value))
        {
            resultText.text = "Menutup Aplikasi";
            yield return new WaitForSecondsRealtime(delay);
            Quit(); 
        }
        else if (commands["next scene"].Contains(value))
        {
            resultText.text = "Scene Berikutnya";
            yield return new WaitForSecondsRealtime(delay);
            NextScene();
        }
        else
        {
            bool isContain = false;
            foreach (var item in customCommands)
            {
                if (item.commands.Contains(value))
                {
                    resultText.text = item.name;
                    yield return new WaitForSecondsRealtime(delay);
                    item.onResult.Invoke();
                    isContain = true;
                }
            }

            if (!isContain)
            {
                resultText.text = "Perintah Belum Terdaftar";
                yield return new WaitForSecondsRealtime(delay);
            }
        }

        resultText.gameObject.SetActive(false);
    }

    public void Quit() 
    {
        GameManager.instance.Quit();
    }

    public void NextScene() 
    {
        GameManager.instance.sceneSettings.NextScene();
    }

    public void OnError(string error)
    {
        Debug.LogError(error);
    }
}
