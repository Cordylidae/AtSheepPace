using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    Animation playAnimation;

    private void Start()
    {
        playAnimation = GetComponent<Animation>();
    }

    public void PlayOpenAnimation()
    {
        playAnimation.Play("OpenShot");
    }

    public void PlayCloseAnimation()
    {
        playAnimation.Play("CloseShot");
    }
}