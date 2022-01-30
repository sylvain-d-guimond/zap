using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTargetSetter : MonoBehaviour
{
    public Transform TargetTransform;

    private IMixedRealityEyeGazeProvider _eyeGaze;

    void Start()
    {
        _eyeGaze = CoreServices.InputSystem?.EyeGazeProvider;
    }

    // Update is called once per frame
    void Update()
    {
        if (_eyeGaze != null)
        {
            var ray = new Ray(CameraCache.Main.transform.position, _eyeGaze.GazeDirection.normalized);
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);
            TargetTransform.position = hitInfo.point;
        }
    }
}
