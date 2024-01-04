using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform look;
    [SerializeField] Transform img;

    void Start()
    {
        transform.parent = target;
        transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        transform.LookAt(look);
        float z = Vector3.Distance(transform.position, look.position) - 2;
        img.localPosition = new Vector3(0, 0, Mathf.Clamp(z, 1, 10));
    }
}
