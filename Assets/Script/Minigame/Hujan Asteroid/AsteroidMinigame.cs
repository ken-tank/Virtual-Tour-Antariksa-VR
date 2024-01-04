using UnityEngine;

public class AsteroidMinigame : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] Follow follow;

    Vector3 randomDir;

    void Start()
    {
        randomDir = Random.onUnitSphere;
    }

    public void Explode() 
    {
        particle.gameObject.SetActive(true);
        particle.transform.parent = null;
        Destroy(follow.gameObject);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.localEulerAngles += randomDir * Random.Range(50, 90) * Time.deltaTime;
    }

    public void Hide() 
    {
        Destroy(follow.gameObject);
    }
}
