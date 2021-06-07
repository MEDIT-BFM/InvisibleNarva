using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MinimapUI : MonoBehaviour {
    [SerializeField] private PlayerController player;
    [SerializeField] private Camera minimapCamera;
    [SerializeField] private Toggle compass;
    [SerializeField] private Slider zoomSlider;
    [SerializeField] private Transform[] tasks;

    private Image _compassImage;
    private Transform _dynamicCompass;
    private Transform _mapCamTransform;
    private Vector3 _cameraPosition;

    private void Awake() {
        _compassImage = GetComponent<Image>();
        _dynamicCompass = compass.graphic.transform;
        _mapCamTransform = minimapCamera.transform;
        _cameraPosition = new Vector3(player.Transform.position.x, _mapCamTransform.position.y, player.Transform.position.z);
    }

    private void OnEnable() {
        //compass.onValueChanged.AddListener(CompassChangedHandler);
        zoomSlider.onValueChanged.AddListener(ZoomValueChangedHandler);
    }

    private void LateUpdate() {
        if (player.VelocityMagnitude == 0) {
            return;
        }

        _mapCamTransform.position = _cameraPosition;
        _mapCamTransform.rotation = player.Transform.rotation;
    }

    private void ZoomValueChangedHandler(float value) {
        minimapCamera.orthographicSize = 60 + (value * 70f);
    }

    //private void CompassChangedHandler(bool isOn) {
    //    if (isOn) {
    //        _compassImage.color = Color.clear;

    //        var compassRotation = new Vector3(0, 0, player.Transform.rotation.y);
    //        _dynamicCompass.DORotate(compassRotation, 0).SetLoops(-1);

    //        var camereRotate = new Vector3(_mapCamTransform.rotation.x, player.Transform.rotation.y, _mapCamTransform.rotation.z);
    //        _mapCamTransform.DORotate(camereRotate, 0);
    //        var taskrotate = new Vector3(_mapCamTransform.rotation.x, player.Transform.rotation.y, _mapCamTransform.rotation.z);

    //        //for (int i = 0; i < tasks.Length; i++) {
    //        //    if (tasks[i] != null) {
    //        //        tasks[i].DORotate(taskrotate, 0).SetLoops(-1);
    //        //    }
    //        //}
    //    }
    //    else {
    //        _compassImage.color = Color.white;

    //        var rotate = new Vector3(_mapCamTransform.rotation.x, 0, _mapCamTransform.rotation.z);
    //        _mapCamTransform.DORotate(rotate, 0);

    //        var taskrotate = new Vector3(_mapCamTransform.rotation.x, 0, _mapCamTransform.rotation.z);

    //        //for (int i = 0; i < tasks.Length; i++) {
    //        //    if (tasks[i] != null) {
    //        //        tasks[i].DORotate(taskrotate, 0);
    //        //    }
    //        //}
    //    }
    //}

    private void OnDisable() {
        compass.onValueChanged.RemoveAllListeners();
        zoomSlider.onValueChanged.RemoveAllListeners();
    }
}
