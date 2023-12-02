using UnityEngine;

public class Bot_AnimatorController : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMechineAnimation(float value)
    {
        animator.SetLayerWeight(1, value);
    }
}
