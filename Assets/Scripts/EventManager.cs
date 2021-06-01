using UnityEngine;

public class EventManager : MonoBehaviour {

    public static EventManager eventManager;

    public delegate void Gesture();
    public static event Gesture OnBegin;
    public static event Gesture OnStationary;
    public static event Gesture OnMoved;
    public static event Gesture OnEnd;
    public static event Gesture OnCanceled;

    private void Awake()
    {
        if (eventManager == null)
            eventManager = this;

        DontDestroyOnLoad(eventManager);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
               
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (OnBegin != null) { OnBegin(); }
                        break;

                    case TouchPhase.Stationary:
                        if (OnStationary != null) { OnStationary(); }
                        break;

                    case TouchPhase.Moved:
                        if (OnMoved != null) { OnMoved(); }
                        break;

                    case TouchPhase.Ended:
                        if (OnEnd != null) { OnEnd(); }
                        break;

                    case TouchPhase.Canceled:
                        if (OnCanceled != null) { OnCanceled(); }
                        break;
                }
            }
        }
    }
    public static bool isHit(Transform trans)
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                bool isHit = Physics.Raycast(ray, out hit, 500f);
                if (isHit && hit.transform == trans)
                {
                    return true;
                }
            }
        }
        return false;
    }
}