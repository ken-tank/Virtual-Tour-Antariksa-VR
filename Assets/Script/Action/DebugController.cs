using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float speed = 5;
    
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        head.localEulerAngles += new Vector3(-y, x, 0) * speed;
    }
}
