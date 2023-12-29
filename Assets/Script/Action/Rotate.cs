using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float multiply = 1;
    [SerializeField, Tooltip("Angle Per Second")] Vector3 speed;
    [SerializeField] bool isLocal = true;

    void Update()
    {
        if (isLocal) 
        {
            transform.localEulerAngles += speed * multiply * Time.deltaTime;
        }
        else
        {
            transform.Rotate(speed, multiply * Time.deltaTime, UnityEngine.Space.World);
        }
    }
}
