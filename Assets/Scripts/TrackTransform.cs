using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTransform : MonoBehaviour
{
    public Transform Target;
    public bool TrackPosition, TrackRotation, TrackScale;

    private void Update()
    {
        if (TrackPosition) transform.position = Target.position;
        if (TrackRotation) transform.rotation = Target.rotation;
        if (TrackScale) transform.localScale = Target.localScale;
    }
}
