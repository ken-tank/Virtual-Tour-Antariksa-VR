using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    [SerializeField] ScrollRect scrollView;

    [SerializeField] float speed;

    void Start()
    {
        scrollView.verticalNormalizedPosition = 1;
    }

    void Update()
    {
        if (scrollView.verticalNormalizedPosition >= 0) scrollView.verticalNormalizedPosition -= speed * Time.deltaTime;
    }
}
