using System;
using System.Threading.Tasks;
using UnityEngine;

public class DeerSwapSign : MonoBehaviour
{
    [SerializeField] private IAnimalSign animalSign;

    private float timer = 1.25f;

    [NonSerialized] public bool IsOpen = false;
    [NonSerialized] public bool CanStart = false;
    [NonSerialized] public bool OnceSwap = true;

    void Awake()
    {
    }

    void Update()
    {
        if (IsOpen && CanStart) timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            if (OnceSwap)
            {
                OnceSwap = false;
                NextSignByTime();
            }
        }
    }

    public void ResetTimer()
    {
        timer = 0.95f;
    }

    private async Task NextSignByTime()
    {
        Animation anim = this.GetComponent<Animation>();

        anim.Play("ChangeShot");

        await Task.Delay((int)(anim["ChangeShot"].length * 1000));

        animalSign.SetRandomSign();
        
        ResetTimer();
        OnceSwap = true;
    }
}
