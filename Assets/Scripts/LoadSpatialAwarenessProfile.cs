using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.Experimental.SpatialAwareness;

public class LoadSpatialAwarenessProfile : MonoBehaviour
{
    public void Call()
    {
        var awareness = CoreServices.SpatialAwarenessSystem as IMixedRealityDataProviderAccess;

        var meshObserver = awareness.GetDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
        var sceneUnderstanding = awareness.GetDataProvider<IMixedRealitySceneUnderstandingObserver>();
    }
}
