using System.Collections;
using UnityEngine;

public class EntityTimer : MonoBehaviour {
    [Tooltip("Entities (images, characters etc.) that will be displayed after this one.")]
    public Transform nextEntity;
    [Range(0, 100)]
    public float delayTime;
    [Tooltip("How long (in second) this entitiy will appear on the screen.")]
    public float displayTime;
    public bool isImage;
    //public bool isDestroyed;

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
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().enabled = true;
        }

        yield return new WaitForSeconds(displayTime);
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
            if (c == GetComponent<EntityTimer>())
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

    private void OnDestroy()
    {
        if (nextEntity != null)
        {
            //nextEntity.GetComponent<EntityManager>().isDestroyed = true;
        }
    }
}
