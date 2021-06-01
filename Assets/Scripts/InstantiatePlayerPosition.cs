using UnityEngine;

public class InstantiatePlayerPosition : MonoBehaviour 
{
    public GameObject PlayerInitialPositionPrefab;

    private void Awake()
    {
        Instantiate(PlayerInitialPositionPrefab);
    }
}
