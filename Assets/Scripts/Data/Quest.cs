using UnityEngine;

public class Quest : MonoBehaviour {
    public bool IsCompleted;
    public Transform InitialPlayerTransform;
    public Sprite CompletedSprite;
    [TextArea(1, 3)] public string QuestInfoText;
}