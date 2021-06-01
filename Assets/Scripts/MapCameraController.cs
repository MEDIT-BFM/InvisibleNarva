using UnityEngine;
using UnityEngine.UI;

public class MapCameraController : MonoBehaviour
{    
    public Transform Player;
    public Toggle mapSet;
    public Slider mapZoom;

    public Transform[] mapUITaskSprites;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        SetMapCameraPosition();
        MapNavigationDisplay();
        ZoomController(mapZoom.value);
    }

    private void SetMapCameraPosition()
    {
        cam.transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
    }

    private void MapNavigationDisplay()
    {
        if (mapSet.isOn)
        {
            cam.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Player.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            mapSet.GetComponent<Image>().color = new Color(mapSet.GetComponent<Image>().color.r, mapSet.GetComponent<Image>().color.b, mapSet.GetComponent<Image>().color.g, 0f);
            mapSet.graphic.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Player.rotation.eulerAngles.y);
            for (int i = 0; i < mapUITaskSprites.Length; i++)
            {
                if (mapUITaskSprites[i] != null)
                    mapUITaskSprites[i].rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Player.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
        }
        else
        {
            cam.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0.0f, transform.rotation.eulerAngles.z);
            mapSet.GetComponent<Image>().color = new Color(mapSet.GetComponent<Image>().color.r, mapSet.GetComponent<Image>().color.b, mapSet.GetComponent<Image>().color.g, 1f);
            for (int i = 0; i < mapUITaskSprites.Length; i++)
            {
                if (mapUITaskSprites[i] != null)
                    mapUITaskSprites[i].rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0.0f, transform.rotation.eulerAngles.z);
            }
        }
    }

    private void ZoomController(float value)
    {
        cam.orthographicSize = 60 + (value * 70f);
    }   
}