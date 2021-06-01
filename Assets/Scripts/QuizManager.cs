using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [Tooltip("An entity (images, characters etc.) that will be displayed after the current one. Leave empty if there is not any.")]
    public Transform nextEntity;
    public Transform quizPanel;
    public Transform buttonPanel;

    public Toggle[] Answers;
    public Transform[] FeedbackPanels;

    private bool isSelected = false;

    private void Start()
    {
        foreach (Toggle item in Answers)
        {
            item.interactable = false;
        }
    }

    private void Update()
    {
        OptionController();
        SelectionCheck(isSelected);
    }

    private void OptionController()
    {
        foreach (Toggle item in Answers)
        {
            if (areAllTrue())
            {
                if (item.isOn)
                {
                    isSelected = true;
                }
                if (FeedbackEntityController.CorrectAnswer)
                {
                    //item.interactable = false;
                    //buttonPanel.gameObject.SetActive(false);
                    quizPanel.gameObject.SetActive(false);
                }
                else
                {
                    item.interactable = true;
                }
            }
        }
    }

    public void CheckResponse()
    {
        for (int k = 0; k < Answers.Length; k++)
        {
            if (Answers[k].isOn)
            {
                FeedbackPanels[k].gameObject.SetActive(true);
            }
        }
    }
    
    private void SelectionCheck(bool value)
    {
        if (value)
        {
            buttonPanel.gameObject.SetActive(true);
        }
        else
        {
            buttonPanel.gameObject.SetActive(false);
        }
    }

    private bool areAllTrue()
    {
        foreach (Toggle a in Answers)
        {
            if (!a.gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    private void OnDestroy()
    {
        if (nextEntity != null)
        {
            nextEntity.gameObject.SetActive(true);            
        }
    }
}
