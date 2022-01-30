using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMagic : MonoBehaviour
{
    public void Call()
    {
        MagicManager.Instance.GetPreparing().Activate();
    }
}
