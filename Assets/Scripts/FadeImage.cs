using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour {
    public float FadeRate;

    private Image image;
    private float targetAlpha;

    void Start() {
        image = GetComponent<Image>();
        if (image == null) {
            Debug.LogError("Error: No image on " + name);
        }
        targetAlpha = image.color.a;
    }

    void Update() {
        Color curColor = image.color;
        float alphaDiff = Mathf.Abs(curColor.a - targetAlpha);
        if (alphaDiff > 0.01f) {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            image.color = curColor;
        }
    }

    public void FadeOut() {
        targetAlpha = 0.0f;
    }

    public void FadeIn() {
        targetAlpha = 1.0f;
    }
}