using UnityEngine;

[System.Serializable]
public class QuestionData {
    public Entity Character;
    [TextArea(2, 4)] public string Question;
    public AnswerData[] Answers;
}
