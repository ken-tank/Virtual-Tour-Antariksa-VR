using UnityEngine;
using UnityEngine.Events;

public class VRPointerEvent : MonoBehaviour
{
    [System.Serializable]
    public class Events {
        public UnityEvent onEnter, onExit, onClick;
    }

    public Events events;

    public void OnPointerEnter() 
    {
        events.onEnter.Invoke();
    }

    public void OnPointerExit() 
    {
        events.onExit.Invoke();
    }

    public void OnPointerClick() 
    {
        events.onClick.Invoke();
    }
}
