using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ArcLightning : MonoBehaviour
{
    public Transform StartPos;
    public Transform EndPos;
    public Transform Waypoint1;
    public Transform Waypoint2;

    public VFXGroup FX;

    public Vector3 Waypoint1Rotation;
    public Vector3 Waypoint2Rotation;

    public bool UpdateEndPoint = true;

    private bool _isOn;
    private IMixedRealityEyeGazeProvider _gazeProvider;

    private void Start()
    {
        _gazeProvider = CoreServices.InputSystem?.EyeGazeProvider;
    }

    public void SetActive(bool active)
    {
        _isOn = active;
        if (active) FX.StartFX(); 
        else FX.StopFX();
    }

    private void Update()
    {
        if (_isOn && _gazeProvider != null)
        {
            if (UpdateEndPoint)
            {
                var ray = new Ray(CameraCache.Main.transform.position, _gazeProvider.GazeDirection.normalized);
                RaycastHit hitInfo;
                Physics.Raycast(ray, out hitInfo);
                EndPos.position = hitInfo.point;
            }

            Waypoint1.position = StartPos.position + (EndPos.position - StartPos.position) * (1f / 3f);
            Waypoint2.position = StartPos.position + (EndPos.position - StartPos.position) * (2f / 3f);

            Waypoint1.Rotate(Waypoint1Rotation * Time.deltaTime, Space.Self);
            Waypoint2.Rotate(Waypoint2Rotation * Time.deltaTime, Space.Self);
        }
    }
}
