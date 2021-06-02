using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneShifter : MonoBehaviour {
    public string sceneName;

    private Animator animator;
    private SoundManager soundManager;

    private void Awake() {
        animator = GetComponent<Animator>();
        soundManager = SoundManager.Instance;
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
        soundManager.PlayVideoSound(null);
        soundManager.PlayBackgroundSound(null);
        soundManager.VideoPlayer.clip = null;
        soundManager.VideoPlayer.targetTexture = null;
        soundManager.VideoPlayer.Stop();
        EntityTrigger.isInteracted = false;
        StopAllCoroutines();
        Destroy(Narrate.NarrationManager.instance.gameObject);
    }

    public void ResetSingletons() {
        Destroy(soundManager.gameObject);
        Destroy(Narrate.NarrationManager.instance.gameObject);
        StopInteraction();
    }
}