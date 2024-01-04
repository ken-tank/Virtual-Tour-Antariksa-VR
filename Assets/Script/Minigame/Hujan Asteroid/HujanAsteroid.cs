using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HujanAsteroid : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Bot_KillerMode bot;
    [SerializeField] ParticleSystem charging;
    [SerializeField] LineRenderer laser;
    [SerializeField] AudioSource laserSound;

    [SerializeField] float speed;
    [SerializeField] float timeout = 10;

    public UnityEvent onCompleate;

    bool once = true;
    bool compleated = false;
    void Update()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform item in transform)
            {
                item.position += transform.forward * speed * Time.deltaTime;
            }
        }
        else
        {
            if (once)
            {
                onCompleate.Invoke();
                once = false;
                compleated = true;
                Destroy(gameObject, 1);
            }
        }

        if (timeout >= 0)
        {
            if (!compleated) timeout -= Time.deltaTime;
        }
        else
        {
            foreach (Transform item in transform)
            {
                item.GetComponent<AsteroidMinigame>().Hide();
            }
            onCompleate.Invoke();
            Destroy(gameObject);
        }

        if (Physics.Raycast(player.position, player.forward, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out AsteroidMinigame asteroid))
            {
                BotTarget(asteroid);
            }
            else
            {
                chargingDuration = 3;
                charging.gameObject.SetActive(false);
                target = null;
            }
        }
        else
        {
            chargingDuration = 3;
            charging.gameObject.SetActive(false);
            target = null;
        }
    }

    AsteroidMinigame target = null;
    public void BotTarget(AsteroidMinigame target)
    {
        bot.transform.LookAt(target.transform);
        this.target = target;
        if (this.target != null) Charging();
    }

    float chargingDuration = 3;
    void Charging() 
    {
        charging.gameObject.SetActive(true);
        chargingDuration -= Time.deltaTime;
        if (chargingDuration <= 0)
        {
            StartCoroutine(Shot());
            target.Explode();
            charging.gameObject.SetActive(false);
            target = null;
        }
    }

    IEnumerator Shot()
    {
        laserSound.Play();
        laser.gameObject.SetActive(true);
        laser.widthMultiplier = 0;
        float time = 0;
        float duration = 0.3f;
        while (time <= duration)
        {
            time += Time.deltaTime;
            float t = time/duration;
            float width = Mathf.Lerp(0, 0.25f, new AnimationCurve(new Keyframe(0,0), new Keyframe(0.1f, 1), new Keyframe(1, 0)).Evaluate(t));
            laser.widthMultiplier = width;
            yield return null;
        }
        laser.widthMultiplier = 0;
        laser.gameObject.SetActive(false);
    }
}
