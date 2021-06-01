using System.Collections;
using UnityEngine;

public class PlaySoundAtStart : MonoBehaviour 
{
    public bool isLoop;
    public bool quizAnswer;
    public bool entitySound;
    public AudioClip currentTrack;
    public float delayTime;

    private void Start()
    {
        CheckLooping();
        if (!quizAnswer)
        {
            StartCoroutine(PlayAfterDelay());
        }
    }

    private void CheckLooping()
    {
        if (!isLoop)
        {
            SoundManager.Instance.BackgroundSource.loop = false;
        }
        else
        {
            SoundManager.Instance.BackgroundSource.loop = true;
        }
    }

    private IEnumerator PlayAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        SoundManager.Instance.PlayBackgroundSound(currentTrack);
        if (entitySound)
        {
            Destroy(gameObject, currentTrack.length);
        }
    }

    private void OnDestroy()
    {
        SoundManager.Instance.PlayBackgroundSound(null);
    }

    public void PlayAudio(AudioClip audioClip){
        if (GetComponent<UnityEngine.UI.Toggle>().isOn)
        {
            SoundManager.Instance.PlayVideoSound(audioClip);
            SoundManager.Instance.MakeNullAfterPlay();
        }
    }
}
