using Narrate;
using UnityEngine;

public class TimeSetEntity : MonoBehaviour
{
    public Transform entity;

    private OnEnableNarrationTrigger narration;

    private void Awake()
    {
        narration = GetComponent<OnEnableNarrationTrigger>();
        entity.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (narration != null)
            {
                narration.enabled = true;
            }
            entity.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player") Destroy(gameObject);
    }
}
