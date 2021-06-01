using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FadeImageTransition : MonoBehaviour {
    private Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load("AnimatorController/IMG_Entity") as RuntimeAnimatorController;
    }

    public void FadeOut()
    {
        animator.SetBool("isFadeOut", true);
    }
}
