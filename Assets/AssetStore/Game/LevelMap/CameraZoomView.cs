using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class CameraZoomView : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform zoomPoint;

    private void Awake()
    {
        _camera = Camera.main;
    }

    [Button]
    void MoveToPosition()
    {
        _camera.transform.DOLocalMove(new Vector3(zoomPoint.position.x, zoomPoint.position.y, _camera.transform.position.z), 1);
    }

}
