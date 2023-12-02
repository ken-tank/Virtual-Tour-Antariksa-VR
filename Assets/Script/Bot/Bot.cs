using System.Threading.Tasks;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [System.Serializable] 
    class SoundManager {
        public AudioSource effectSource;
    }

    [SerializeField] DialogController dialogController;
    [SerializeField] ParticleSystem spawnParticle;
    [SerializeField] SoundManager soundManager;
    
    Bot_AnimatorController animatorController;

    void Awake()
    {
        animatorController = GetComponent<Bot_AnimatorController>();
    }

    void OnEnable()
    {
        PlaySpawnParticle();
    }

    void OnDisable()
    {
        PlaySpawnParticle();
    }

    public void PlaySpawnParticle()
    {
        Instantiate(spawnParticle, soundManager.effectSource.transform.position, soundManager.effectSource.transform.rotation);
    }

    void PlaySoundFX(AudioClip clip)
    {
        if (clip)
        {
            soundManager.effectSource.PlayOneShot(clip, 0.5f);
        }
        else
        {
            Debug.Log($"{clip.name} Not Found");
        }
    }
}
