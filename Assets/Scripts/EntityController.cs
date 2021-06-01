using UnityEngine;
using System.Collections;

public class EntityController : MonoBehaviour
{
    [Tooltip("Check if this entity is the first order under the Entities panel.")]
    public bool initialEntity;

    [Tooltip("Check if the next entity is a Quiz.")]
    public bool nextIsQuiz;

    public bool nextIsNarration;

    [Tooltip("An entity (images, characters etc.) that will be displayed after the current one. Leave empty if there is not any.")]
    public Transform nextEntity;

    

    public Transform narrationEntity;
    [Tooltip("How long time(second) this entitiy will appear on the screen.")]
    public float displayTime;
    [Tooltip("How much time(second) this entitiy will be delayed after the previous entity disappeared.")]
    public float delayTime;
    [HideInInspector]
    public bool isTriggered;

    private bool oneTime = true;
    private bool displayable = false;
    

    private void Awake()
    {
        isTriggered = false;
        oneTime = false;
        if (!initialEntity)
        {
            gameObject.SetActive(false);
        }
        //if (nextIsQuiz && nextEntity != null)
        //{
        //    nextEntity.gameObject.SetActive(false);
        //}
        //if (GetComponent<UnityEngine.Video.VideoPlayer>() != null)
        //{
        //    videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        //}
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
       // if (videoPlayer != null) videoPlayer.enabled = true;
    }

    private void DisableComponents()
    {
        if (GetComponent<UIFader>() != null) GetComponent<UIFader>().enabled = false;
        //if (videoPlayer != null) videoPlayer.enabled = false;
    }

    private void OnDestroy()
    {
        if (nextEntity != null)
        {
            nextEntity.gameObject.SetActive(true);
            if (nextEntity.GetComponent<EntityController>() != null)
            {
                nextEntity.GetComponent<EntityController>().isTriggered = true;
            }
            //if (nextIsQuiz)
            //{
            //    nextEntity.gameObject.SetActive(true);
            //}
            //if (nextEntity.name == "Navigator")
            //{
            //    nextEntity.gameObject.SetActive(true);
            //}
            //if (nextIsNarration && narrationEntity != null)
            //{
            //    if (narrationEntity.GetComponent<Narrate.TimerNarrationTrigger>() != null)
            //        narrationEntity.GetComponent<Narrate.TimerNarrationTrigger>().enabled = true;

            //    if (narrationEntity.GetComponent<Narrate.OnEnableNarrationTrigger>() != null)
            //        narrationEntity.GetComponent<Narrate.OnEnableNarrationTrigger>().enabled = true;
            //}
           
        }
    }
}
