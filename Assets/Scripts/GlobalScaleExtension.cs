using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalScaleExtension
{
    public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
    {
        //transform.localScale = Vector3.one;
        Debug.Log($"Set scale global:{globalScale.x} current:{transform.lossyScale.x}");
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }
}
