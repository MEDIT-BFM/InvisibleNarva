using UnityEngine;

public class PuzzleEntityController : MonoBehaviour {

    public PuzzleController puzzle;
    //public Transform feedbackEntity;
    public Transform tileArea;
    public Transform nextEntity;

    //private void Awake()
    //{
    //    if (feedbackEntity != null)
    //    {
    //        feedbackEntity.gameObject.SetActive(false);
    //    }
    //}

    private void OnEnable()
    {
        puzzle.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (puzzle.PuzzleChecked() && gameObject.activeInHierarchy)
        {
            if (nextEntity != null)
            {
                //nextEntity.gameObject.SetActive(true);
                Destroy(gameObject, 2f);
                tileArea.gameObject.GetComponentInChildren<UnityEngine.UI.Image>().raycastTarget = false;
            }
            return;
        }
    }

    private void OnDestroy()
    {
        if (nextEntity != null)
        {
            nextEntity.gameObject.SetActive(true);
        }
    }
}
