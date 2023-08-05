using System;
using UnityEngine;

public class PlayerInputLevelMap : MonoBehaviour
{
    public bool inFocus = true;

    private Ray ray;
    private RaycastHit hit;

    public Action<BaseTapHandel> baseTap;

    private float timerClick = 0.1f;
    private bool lastClickTime = true;

    void Update()
    {
        CountDownTimer();
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && lastClickTime)
        {
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            if (SceneManager.GetActiveScene().name == GameSceneName.CoreGame) CheckCoreGame();
            if (SceneManager.GetActiveScene().name == GameSceneName.LevelsMap) CheckLevelMap();
            
            lastClickTime = false;
        }

#elif UNITY_EDITOR || UNITY_WINDOWS || UNITY_MAC
        if (Input.GetMouseButtonUp(0) && lastClickTime)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(inFocus) 
                CheckLevelMap();

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

    #region Scene_LevelMap

    void CheckLevelMap()
    {
        CheckTapOnLevel();
    }

    void CheckTapOnLevel()
    {
        Debug.DrawRay(ray.origin, ray.direction * 40, Color.white, 100f);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "LevelButton")
            {
                Debug.Log("Click on Level");

                Debug.DrawRay(ray.origin, ray.direction * 40, Color.red, 100f);

                BaseTapHandel baseTapHandel = hit.transform.GetComponent<BaseTapHandel>();
                baseTapHandel.Tapped();

                baseTap?.Invoke(baseTapHandel);
            }
        }
    }

    #endregion
}
