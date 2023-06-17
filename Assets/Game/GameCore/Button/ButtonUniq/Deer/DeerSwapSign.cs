using System;
using System.Threading.Tasks;
using UnityEngine;

public class DeerSwapSign : MonoBehaviour
{
    private IAnimalSign animalSign;

    private float timer = 2.4f;

    [NonSerialized] public bool IsOpen = false;
    [NonSerialized] public bool CanStart = false;

    void Awake()
    {
        animalSign = GetComponentInChildren<IAnimalSign>();  
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            if (IsOpen && CanStart) NextSignByTime();
            ResetTimer();
        }
    }

    public void ResetTimer()
    {
        timer = 1.35f;
    }

    private async Task NextSignByTime()
    {
        Debug.Log("im there");

        Animation anim = this.GetComponent<Animation>();
        anim.Play("ChangeShot");

        await Task.Delay((int)(anim["ChangeShot"].length * 1000));
        animalSign.SetRandomSign(); 
    }
}
