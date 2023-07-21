using System;
using UnityEngine;

public class CircleOfSubs : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private GameObject SubButton;

    public void MakeSubs(int count)
    {
        GameObject subList = new GameObject("SubList");
        subList.transform.SetParent(this.transform);
        subList.transform.localPosition = Vector3.zero;

        GameObject sub;

        for (int i = 0; i < count; i++)
        {
            sub = Instantiate(
                            SubButton,
                            Vector3.zero,
                            SubButton.transform.rotation);

            sub.transform.SetParent(subList.transform);
            sub.transform.localPosition = PositionByAngle(360 / count * i);

            SubView subView = sub.GetComponent<SubView>();

            if (subView != null)
            {
                subView.BaseTapHandel.isTap += () =>
                {
                    subView.Tapped();
                };

            }
        }
    }

    private Vector3 PositionByAngle(float angle)
    {
        Vector3 positionSub = new Vector3(Mathf.Sin(angle * Mathf.PI / 180), Mathf.Cos(angle * Mathf.PI / 180), -0.01f);
        
        positionSub = positionSub.normalized * circleCollider.radius;

        return positionSub;
    }
}
