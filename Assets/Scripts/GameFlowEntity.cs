using System.Collections;
using UnityEngine;

public class GameFlowEntity : MonoBehaviour
{
    [Range(0, 100)]
    public float delayTime;
    [Range(0, 100)]
    public float showTime;
    public bool isImage;
    private MonoBehaviour[] comps;   

    private void Awake()
    {
        comps = GetComponents<MonoBehaviour>();
        DisableComponents();
    }

    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().enabled = false;
        }
        StartCoroutine(DelayTime());
    }

    private IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(delayTime);
        EnableComponents();
        //Only for Images
        if (isImage)
        {
            GetComponent<FadeImage>().FadeIn();
        }
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().enabled = true;
        }

        yield return new WaitForSeconds(showTime);
        if (isImage)
        {
            GetComponent<FadeImage>().FadeOut();
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void DisableComponents()
    {
        foreach (MonoBehaviour c in comps)
        {
            if (c == GetComponent<GameFlowEntity>())
            {
                continue;
            }
            else
            {
                c.enabled = false;
            }
        }
    }

    private void EnableComponents()
    {
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = true;
        }
    }
}
