using UnityEngine;

public class CongratulationController : MonoBehaviour
{
    public Transform EntityContent;
    public Transform Credits;

    [HideInInspector]
    public int totalEntities;

    public static int TotalEntities;

    private CongratulationController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        TotalEntities = 0;
    }

    private void Start()
    {
        totalEntities = EntityContent.childCount;
    }

    public void Congratulation()
    {
        Credits.gameObject.SetActive(true);
    }
}
