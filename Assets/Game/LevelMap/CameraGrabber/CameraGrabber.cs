using DG.Tweening;
using UnityEngine;

public class CameraGrabber : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;

    bool grabbing = true;

    private void Awake()
    {
        startPos = endPos = Camera.main.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && grabbing)
        {
            grabbing = false;

            startPos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;

            Debug.Log("Start grabbing");
        }

        if (Input.GetMouseButtonUp(0) && !grabbing)
        {
            Debug.Log("End grabbing");

            grabbing = true;

            endPos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;

            GrabbCamera();

            Debug.Log(startPos);
            Debug.Log(endPos);
        }
    }

    void GrabbCamera()
    {
        Vector2 offset = startPos - endPos;

        //Camera.main.transform.DOLocalMove
        //    (Camera.main.transform.position
        //    - new Vector3(offset.x, offset.y, 0.0f), 0.5f, true);

        Camera.main.transform.position += new Vector3(offset.x, offset.y, 0.0f);
    }
}
