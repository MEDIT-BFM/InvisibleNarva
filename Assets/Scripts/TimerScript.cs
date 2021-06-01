using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [Range(0,20)]
    public float delayTime;
    public SceneShifter sceneShifter;

    void Start ()
    {     
        Invoke("SplashScreen", delayTime);
    }

    private void SplashScreen()
    {
        sceneShifter.FadeTransition();
    }
}
