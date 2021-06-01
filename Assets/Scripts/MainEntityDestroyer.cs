using UnityEngine;

public class MainEntityDestroyer : MonoBehaviour
{
    public CongratulationController congratulation;

    private void OnDestroy()
    {
        if (congratulation)
        {
            if (CongratulationController.TotalEntities < congratulation.totalEntities)
            {
                CongratulationController.TotalEntities++;
            }
            if (CongratulationController.TotalEntities >= congratulation.totalEntities)
            {
                congratulation.Credits.gameObject.SetActive(true);
            }
        }       
    }
}
