using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour {

    //public static PuzzleController instance;

    public const string DRAGGABLE_TAG = "UIDraggable";
    public Transform[] puzzleTiles;
    public Transform[] gridTiles;

    private bool dragging = false;

    private Vector2 originalPosition;
    private Transform objectToDrag;
    private Image objectToDragImage;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    private bool[] checkedLocation;
    private System.Random rand = new System.Random();

    private void Awake()
    {
        //if (instance)
        //{
        //    instance = this;
        //}
        checkedLocation = new bool[puzzleTiles.Length];
        for (int i = 0; i < checkedLocation.Length; i++)
        {
            checkedLocation[i] = false;
        }
        Shuffle();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetDraggableTransformUnderMouse();

            if (objectToDrag != null)
            {
                dragging = true;

                objectToDrag.SetAsLastSibling();

                originalPosition = objectToDrag.position;
                objectToDragImage = objectToDrag.GetComponent<Image>();
                objectToDragImage.raycastTarget = false;
            }
        }

        if (dragging)
        {
            objectToDrag.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (objectToDrag != null)
            {
                var objectToReplace = GetDraggableTransformUnderMouse();

                if (objectToReplace != null)
                {
                    objectToDrag.position = objectToReplace.position;
                    objectToReplace.position = originalPosition;
                }
                else
                {
                    objectToDrag.position = originalPosition;
                }

                objectToDragImage.raycastTarget = true;
                objectToDrag = null;
            }

            dragging = false;
            PuzzleChecked();
        }
    }

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects.First().gameObject;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        var clickedObject = GetObjectUnderMouse();

        // get top level object hit
        if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
        {
            return clickedObject.transform;
        }
        return null;
    }

    //private IEnumerator CalculateDistance()
    //{
    //    Canvas.ForceUpdateCanvases();
    //    yield return new WaitForEndOfFrame();
    //    Canvas.ForceUpdateCanvases();
    //    PuzzleChecked();
    //}

    private void Shuffle()
    {
        //Canvas.ForceUpdateCanvases();
        //yield return new WaitForEndOfFrame();
        //Canvas.ForceUpdateCanvases();
        for (int i = puzzleTiles.Length - 1; i > 0; i--)
        {
            int n = rand.Next(i + 1);
            Vector2 temp = puzzleTiles[i].position;
            puzzleTiles[i].position = puzzleTiles[n].position;
            puzzleTiles[n].position = temp;
        }
    }

    public bool PuzzleChecked()
    {
        for (int i = 0; i < checkedLocation.Length; i++)
        {
            if (Vector2.Distance(puzzleTiles[i].position, gridTiles[i].position) <= 0.1f)
            {
                checkedLocation[i] = true;
            }
            if (!checkedLocation[i])
            {
                return false;
            }
        }
        return true;
    }
}
