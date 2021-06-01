using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrate;

public class TapCount : MonoBehaviour {

    public NarrationCountElement count;

    private void OnEnable()
    {
        EventManager.OnBegin += ColorShift;
    }
    private void OnDisable()
    {
        EventManager.OnBegin -= ColorShift;
    }

    private void ColorShift()
    {
        if (EventManager.isHit(transform))
        {
            count.Increment(1f);
            gameObject.SetActive(false);
        }
    }
}
