using UnityEngine;

[System.Serializable]
public class AnswerData {
    public int Id;
    public bool IsCorrect;
    public Entity Character;
    public Entity Feedback;
    [TextArea(2, 4)] public string Answer;
}
