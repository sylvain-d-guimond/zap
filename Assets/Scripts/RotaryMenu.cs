using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryMenu : Menu
{
    public string OpenParameter;
    public Animator Animator;

    public void Open()
    {
        Animator.SetBool(OpenParameter, true);
    }

    public void Close()
    {
        Animator.SetBool(OpenParameter, false);
    }
}
