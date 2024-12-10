using UnityEngine;

public class AnimatorController :MonoBehaviour
{
    public void PlayAnimation(Animator animator,string animationName)
    {
        animator.Play(animationName);
    }
    public void PlayAnimation(Animator animator,string valueName, float speed)
    {
        animator.SetFloat(valueName,speed);
    }
}
