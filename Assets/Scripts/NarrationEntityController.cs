using UnityEngine;

public class NarrationEntityController : MonoBehaviour
{
    public Transform thisHasImage;
    public Transform nextEntity;
    public bool isDelayer;
    public float displayTime;

    private void Awake()
    {
        if (nextEntity != null)
        {
            nextEntity.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if(thisHasImage)
            thisHasImage.gameObject.SetActive(true);

        if (isDelayer)
        {
            Destroy(gameObject, (displayTime + GetComponent<Narrate.TimerNarrationTrigger>().delayPlayingBy));
        }
    }

    private void Update()
    {
        if (GetComponent<Narrate.TimerNarrationTrigger>() == null)
        {
            if (!Narrate.NarrationManager.instance.isPlaying /*&& Narrate.NarrationManager.instance.clipQueue.Count <= 0*/ && !isDelayer)
            {
                Destroy(gameObject);
            }
            if (thisHasImage != null)
            {
                thisHasImage.gameObject.SetActive(true);
            }            
        }
    }

    private void OnDestroy()
    {
        if (nextEntity != null)
        {
            nextEntity.gameObject.SetActive(true);          
        }
    }
}
