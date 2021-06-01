using UnityEngine;

public class EntityTrigger : MonoBehaviour
{
    public Transform entities;
    public static bool isInteracted;
    public int interactionCount;
    public Transform uiArthur;

    private void Awake()
    {
        if (entities != null)
        {
            entities.gameObject.SetActive(false);
        }
        else
        {
            interactionCount++;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (uiArthur)
                uiArthur.gameObject.SetActive(true);
        }
        if (other.gameObject.tag == "Player" && interactionCount == 0 && !isInteracted && entities != null)
        {
            entities.gameObject.SetActive(true);
            isInteracted = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (uiArthur)
                uiArthur.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Player" && !isInteracted && entities != null)
        {
            interactionCount++;
        }
    }
}
