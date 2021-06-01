using UnityEngine;

public class SceneShifter : MonoBehaviour {
    public string sceneName;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeScene()
    {        
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void FadeTransition()
    {
        animator.SetTrigger("FadeTransition");
    }

    public void CompletedLevel(int index)
    {
        AvatarController.Avatar_isCompleted[index-1] = true;
    }

    public void StopInteraction()
    {
        //Narrate.NarrationManager.instance.subManager.Stop();
        //Narrate.NarrationManager.instance.src.Stop();
        SoundManager.Instance.PlayVideoSound(null);
        SoundManager.Instance.PlayBackgroundSound(null);
        SoundManager.Instance.VideoPlayer.clip = null;
        SoundManager.Instance.VideoPlayer.targetTexture = null;
        SoundManager.Instance.VideoPlayer.Stop();
        EntityTrigger.isInteracted = false;
        //Narrate.NarrationManager.instance.clipQueue.Clear();
        StopAllCoroutines();
        Destroy(Narrate.NarrationManager.instance.gameObject);
        //Narrate.NarrationManager.instance.gameObject.SetActive(false);
        //Debug.Log(Narrate.NarrationManager.instance.clipQueue.Capacity);
    }

    public void ResetSingletons()
    {
        Destroy(SoundManager.Instance.gameObject);
        Destroy(Narrate.NarrationManager.instance.gameObject);
        StopInteraction();
    }
}