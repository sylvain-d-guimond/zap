using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePortal : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Room;

    public void Call()
    {
        if (Physics.Raycast(CoreServices.InputSystem.EyeGazeProvider.GazeOrigin, CoreServices.InputSystem.EyeGazeProvider.GazeDirection, out var hit, 20f))
        {
            var go = Instantiate(Prefab, Room);
            go.GetComponent<AnimationBoolTrigger>().Value = true;

            go.transform.position = hit.point;
            go.transform.rotation = hit.transform.rotation;

            Debug.Log($"Place portal at {hit.point}");
        }
    }
}
