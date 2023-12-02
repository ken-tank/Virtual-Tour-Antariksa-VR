using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] AudioSource voiceSource;

    Queue<Dialog> dialogQueue;

    void Awake()
    {
        dialogQueue = new Queue<Dialog>();
        foreach (Transform item in transform)
        {
            item.TryGetComponent(out Dialog dialog);
            if (dialog)
            {
                dialog.voiceSource = voiceSource;
                dialogQueue.Enqueue(dialog);
            }
        }
    }
}
