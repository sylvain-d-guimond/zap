using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransitionToMagic : Transition
{
    public void Set()
    {
        var magic = MagicManager.Instance.GetPreparing();
        Set(magic.transform);
        magic.Activate();
    }
}
