using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UIFader))]
public class ImageEntityController : MonoBehaviour
{
    public bool initialEntity;
    public Transform nextImageEntity;
    public float displayTime;
    public float delayTime;
    [Range(0.01f,0.99f)]
    public float entityAudioVolume;
    [HideInInspector]
    public bool isTriggered;

    private bool oneTime = true;
    private bool displayable = false;
    private AudioSource source;

    private void Awake()
    {
        if (GetComponent<AudioSource>() != null)
        {
            source = GetComponent<AudioSource>();
        }
        isTriggered = false;
        oneTime = false;
        DisableComponents();
    }

    private void Start()
    {
        StartCoroutine(DelayEntity());
    }

    private void Update()
    {
        while (displayable)
        {
            EnableComponents();
            if (!oneTime)
            {
                StartCoroutine(DelayFade());
                Destroy(gameObject, displayTime);
                oneTime = true;
            }
            break;
        }
        if (source != null)
        {
            source.volume = GetComponent<CanvasGroup>().alpha * entityAudioVolume;
        }
    }
    
    private IEnumerator DelayEntity()
    {
        if (!initialEntity)
        {
            yield return new WaitUntil(() => isTriggered == true);
        }
        yield return new WaitForSeconds(delayTime);
        displayable = true;
    }

    private IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(displayTime - 1f);
        if (GetComponent<UIFader>() != null) GetComponent<UIFader>().FadeOut();
    }

    private void EnableComponents()
    {
        if (GetComponent<UIFader>() != null) GetComponent<UIFader>().enabled = true;
        if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().enabled = true;
    }

    private void DisableComponents()
    {
        if (GetComponent<UIFader>() != null) GetComponent<UIFader>().enabled = false;
        if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().enabled = false;
    }

    private void OnDestroy()
    {
        if (nextImageEntity != null)
        {
            nextImageEntity.gameObject.SetActive(true);
            if (nextImageEntity.GetComponent<ImageEntityController>() != null)
            {
                nextImageEntity.GetComponent<ImageEntityController>().isTriggered = true;
            }
        }
    }
}
