using UnityEngine;
using UnityEngine.Timeline;

public class tes : MonoBehaviour
{
    [System.Serializable]
    public struct Grub {
        public string name;
        public float a;
    }

    public Grub group;

    void Awake()
    {
        group.a = 5;
    }
}
