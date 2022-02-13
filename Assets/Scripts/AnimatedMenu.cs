using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedMenu : Menu
{
    public string OpenParameter;
    public Animator Animator;

    public void Open()
    {
        if (Animator != null) Animator.SetBool(OpenParameter, true);
        Show();
    }

    public void Close()
    {
        if (Animator != null) Animator.SetBool(OpenParameter, false);
        Hide();
    }
}
