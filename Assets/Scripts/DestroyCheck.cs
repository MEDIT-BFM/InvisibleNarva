using UnityEngine;

public class DestroyCheck : MonoBehaviour {
    public bool isDestroyed = false;
    private void OnDestroy()
    {
        isDestroyed = true;
    }
}
