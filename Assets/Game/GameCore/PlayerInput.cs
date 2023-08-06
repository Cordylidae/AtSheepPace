using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    public Action baseTap;

    private float timerClick = 0.1f;
    private bool lastClickTime = true;
   
    void Update()
    {
        CountDownTimer();
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && lastClickTime)
        {
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            CheckCoreGame();
            
            lastClickTime = false;
        }

#elif UNITY_EDITOR || UNITY_WINDOWS || UNITY_MAC
        if (Input.GetMouseButtonUp(0) && lastClickTime)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            CheckCoreGame();

            lastClickTime = false;
        }
#endif
    }
    private void CountDownTimer()
    {
        if (!lastClickTime) timerClick -= Time.deltaTime;
        if (timerClick < 0.0f)
        {
            lastClickTime = true;
        }
    }

    #region Scene_CoreGame

    void CheckCoreGame() { 
        CheckTapThis();
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

                baseTap?.Invoke();
            }
        }
    }

    #endregion
}
