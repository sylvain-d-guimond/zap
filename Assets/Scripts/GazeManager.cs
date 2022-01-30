using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class GazeManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        var eyeGazeProvider = CoreServices.InputSystem?.EyeGazeProvider;
        if (eyeGazeProvider != null)
        {
            var target = EyeTrackingTarget.LookedAtEyeTarget;

            if (target != null)
            {

            }
        }
    }
}
