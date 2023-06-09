using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector3 pos;
    private Ray ray;
    private RaycastHit hit;

    void Update()
    {
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            CheckTapThis();
        }

#elif UNITY_EDITOR || UNITY_WINDOWS || UNITY_MAC
        if (Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            CheckTapThis();
        }
#endif
    }

    void CheckTapThis()
    {
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.white, 100f);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "AnimalButton")
            {
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 100f);
             
                BaseTapHandel baseTapHandel = hit.transform.GetComponent<BaseTapHandel>();
                baseTapHandel.Tapped();
            }
        }
    }
}
