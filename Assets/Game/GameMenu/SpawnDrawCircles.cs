using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnDrawCircles : MonoBehaviour
{

    [SerializeField] private GameObject CircleEndles;
    [SerializeField] private int count;
    [SerializeField] private float ticTime;

    private List<DrawCircleUndlessMenu> circles = new List<DrawCircleUndlessMenu>();

    private void Awake()
    {
        CreatSpawn();
    }

    private void Start()
    {
        StartSpawn();
    }

    void CreatSpawn()
    {
        GameObject circleEntity;

        for (int i = 1; i <= count; i++)
        {
            circleEntity = Instantiate(
                            CircleEndles,
                            Vector3.zero,
                            CircleEndles.transform.rotation);

            circleEntity.transform.SetParent(this.transform);

            circles.Add(circleEntity.GetComponent<DrawCircleUndlessMenu>());
        }
    }

    async void StartSpawn()
    {
        int i = 1;

        foreach (DrawCircleUndlessMenu circle in circles)
        {
            circle.GetStart();

            await Task.Delay((int)(ticTime * 1000 * i));
            
            i++;
        }
    }
}
