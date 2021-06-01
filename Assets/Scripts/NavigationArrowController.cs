using UnityEngine;

public class NavigationArrowController : MonoBehaviour
{
    public Transform target;

    private Vector3 targetPosition;

    private void Update()
    {
        targetPosition = target.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
    }
}
