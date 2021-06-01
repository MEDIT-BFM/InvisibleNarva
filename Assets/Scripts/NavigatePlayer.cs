using System.Collections;
using UnityEngine;
public class NavigatePlayer : MonoBehaviour
{
    public NavigationArrowController navigationArrow;
    public TMPro.TMP_Text displayText;
    public string navigationText;
    public Transform NavigateCanvas;
    public Transform touchJoystick;
    public Transform returnMenuUI;
    public Transform headingNarrationPoint;
    public float delayTime;

    void Start()
    {
        headingNarrationPoint.gameObject.SetActive(true);
        StartCoroutine(DisplayUntil());        
    }

    private IEnumerator DisplayUntil()
    {
        yield return new WaitForSeconds(delayTime);
        navigationArrow.target = headingNarrationPoint;
        navigationArrow.gameObject.SetActive(true);
        touchJoystick.gameObject.SetActive(true);
        returnMenuUI.gameObject.SetActive(true);
        NavigateCanvas.gameObject.SetActive(true);
        displayText.text = navigationText;
        //yield return new WaitUntil(() => headingNarrationPoint.GetComponent<EntityTrigger>().isTriggered == true);
        navigationArrow.gameObject.SetActive(false);
        NavigateCanvas.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
