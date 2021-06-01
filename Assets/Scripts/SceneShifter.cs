using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneShifter : MonoBehaviour {
    public string sceneName;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        
    }

    public void ChangeScene() {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void FadeTransition() {
        animator.SetTrigger("FadeTransition");
    }

    public void CompletedLevel(int index) {
        AvatarController.Avatar_isCompleted[index - 1] = true;
    }

    public void StopInteraction() {
        SoundManager.Instance.PlayVideoSound(null);
        SoundManager.Instance.PlayBackgroundSound(null);
        SoundManager.Instance.VideoPlayer.clip = null;
        SoundManager.Instance.VideoPlayer.targetTexture = null;
        SoundManager.Instance.VideoPlayer.Stop();
        EntityTrigger.isInteracted = false;
        StopAllCoroutines();
        Destroy(Narrate.NarrationManager.instance.gameObject);
    }

    public void ResetSingletons() {
        Destroy(SoundManager.Instance.gameObject);
        Destroy(Narrate.NarrationManager.instance.gameObject);
        StopInteraction();
    }
}