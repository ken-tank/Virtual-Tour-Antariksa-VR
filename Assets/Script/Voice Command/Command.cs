using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
        }},
        {"back scene", new string[] {
            "kembali", "level sebelumnya", "kembali ke level sebelumya", "kembali ke scene sebelumnya",
            "scene sebelumnya", "sebelumnya"
        }},
        {"admin mode", new string[] {
            "140401", "1 4 0 4 0 1", "14 04 01", "14 4 1", "1441",
            "seratus empat puluh ribu empat ratus satu", "empat belas kosong empat kosong satu",
            "satu empat kosong empat kosong satu"
        }},
        {"goto scene", new string[] {
            "pergi ke tujuan 1", "ke tujuan 1", "tujuan 1", "menuju tujuan 1", "menuju ke tujuan 1",
            "pergi ke tujuan 2", "ke tujuan 2", "tujuan 2", "menuju tujuan 2", "menuju ke tujuan 2",
            "pergi ke tujuan 3", "ke tujuan 3", "tujuan 3", "menuju tujuan 3", "menuju ke tujuan 3",
            "pergi ke tujuan 4", "ke tujuan 4", "tujuan 4", "menuju tujuan 4", "menuju ke tujuan 4",
            "pergi ke tujuan 5", "ke tujuan 5", "tujuan 5", "menuju tujuan 5", "menuju ke tujuan 5",
            "pergi ke tujuan 6", "ke tujuan 6", "tujuan 6", "menuju tujuan 6", "menuju ke tujuan 6",
            "pergi ke tujuan 7", "ke tujuan 7", "tujuan 7", "menuju tujuan 7", "menuju ke tujuan 7",
            "pergi ke tujuan 8", "ke tujuan 8", "tujuan 8", "menuju tujuan 8", "menuju ke tujuan 8",
            "pergi ke tujuan 9", "ke tujuan 9", "tujuan 9", "menuju tujuan 9", "menuju ke tujuan 9",
            "pergi ke tujuan 10", "ke tujuan 10", "tujuan 10", "menuju tujuan 10", "menuju ke tujuan 10",
            "pergi ke tujuan 11", "ke tujuan 11", "tujuan 11", "menuju tujuan 11", "menuju ke tujuan 11"
        }}
    };

    [System.Serializable] 
    public class CustomCommand {
        public string name;
        public bool enable = true;
        [HideInInspector] public bool admin = false;
        public string[] commands;
        public UnityEvent onResult;

        [Header("Editor Only")] public KeyCode sortcutKey;

        public IEnumerator Engine() 
        {
            while (true)
            {
                if (Input.GetKeyDown(sortcutKey)) onResult.Invoke();
                yield return null;
            }
        }
    }

    public CustomCommand[] customCommands;
    public UnityEvent onSuccess, onFailed;
    bool adminMode = false;

    public void EnableCommand(int index) { customCommands[index].enable = true; }
    public void DisableCommand(int index) { customCommands[index].enable = false; }

    void Awake()
    {
        SpeechRecognizerListener listener = FindObjectOfType<SpeechRecognizerListener>();
        listener.onFinalResults.AddListener(CalculateResult);
        listener.onErrorDuringRecording.AddListener(OnError);
        SpeechRecognizer.RequestAccess();
        if (VoiceListener.admin) AdminMode();
    }

    public void CalculateResult(string value) 
    {
        Debug.Log(value.ToLower());
        Result(value.ToLower());
        FindObjectOfType<VoiceListener>().ResetUI();
        GameSettings.audioMixer.SetFloat("Voice_Volume", 0);
        GameSettings.audioMixer.SetFloat("Music_Volume", 0);
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
        else if (commands["next scene"].Contains(value) && adminMode)
        {
            resultText.text = "Scene Berikutnya";
            yield return new WaitForSecondsRealtime(delay);
            onSuccess.Invoke();
            NextScene();
        }
        else if (commands["back scene"].Contains(value) && adminMode)
        {
            resultText.text = "Kembali";
            yield return new WaitForSecondsRealtime(delay);
            onSuccess.Invoke();
            BackScene();
        }
        else if (commands["goto scene"].Contains(value) && adminMode)
        {
            resultText.text = "Menuju Scene ... ";
            string[] splits = value.Split();
            string name = $"{splits[^2]} {splits[^1]}";
            yield return new WaitForSecondsRealtime(delay);
            onSuccess.Invoke();
            GoToScene(name);
        }
        else if (commands["admin mode"].Contains(value))
        {
            resultText.text = "Admin Mode";
            yield return new WaitForSecondsRealtime(delay);
            onSuccess.Invoke();
            AdminMode();
        }
        else
        {
            bool isContain = false;
            foreach (var item in customCommands)
            {
                if (item.commands.Contains(value) && item.enable)
                {
                    #if(UNITY_EDITOR)
                    StartCoroutine(item.Engine());
                    #endif
                    resultText.text = item.name;
                    yield return new WaitForSecondsRealtime(delay);
                    item.onResult.Invoke();
                    onSuccess.Invoke();
                    isContain = true;
                }
            }

            if (!isContain)
            {
                resultText.text = "Perintah Belum Terdaftar";
                yield return new WaitForSecondsRealtime(delay);
                onFailed.Invoke();
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
        FindObjectOfType<FlashPortal>().NextScene();
    }

    public void BackScene() 
    {
        FindObjectOfType<FlashPortal>().PreviousScene();
    }

    public void GoToScene(string name)
    {
        TextInfo textInfo = new CultureInfo("id-ID").TextInfo;
        string title = textInfo.ToTitleCase(name);
        FindObjectOfType<FlashPortal>().GoToScene(title);
    }

    public void OnError(string error)
    {
        Debug.LogError(error);
    }

    public void AdminMode() 
    {
        adminMode = true;
        foreach (var item in customCommands)
        {
            item.enable = true;
            item.admin = true;
        }
        VoiceListener.admin = true;
    }
}
