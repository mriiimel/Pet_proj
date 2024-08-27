using UnityEngine;

public class AnimatorController :MonoBehaviour
{
    public void PlayAnimation(Animator animator,string animationName)
    {
        animator.Play(animationName);
    }
}
