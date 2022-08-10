using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Examples.Demos;
using Microsoft.MixedReality.Toolkit.Experimental.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine.Events;
using System;

public class PortalGenerator : DemoSpatialMeshHandler, IMixedRealitySpatialAwarenessObservationHandler<SpatialAwarenessSceneObject>
{
    public GameObject PortalPrefab;
    public GameObject IndicatorPrefab;

    public Transform Content;

    public Vector3 Offset;

    public bool DebugMode;
    public Vector3 DebugOffset;

    public ShowScanRoomEvent ScanRoom;

    private IMixedRealitySceneUnderstandingObserver observer;
    private List<AnimationBoolTrigger> instantiatedPrefabs = new List<AnimationBoolTrigger>();
    private Dictionary<SpatialAwarenessSurfaceTypes, Dictionary<int, SpatialAwarenessSceneObject>> observedSceneObjects = new Dictionary<SpatialAwarenessSurfaceTypes, Dictionary<int, SpatialAwarenessSceneObject>>();
    private AnimationBoolTrigger current, previous;
    protected override void Start()
    {
        observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySceneUnderstandingObserver>();
        observer.UpdateOnDemand();

        if (observer == null)
        {
            Debug.LogError("Couldn't access Scene Understanding Observer!");
        }

        //StartCoroutine(CoMainLoop());
        Debug.Log("Portal generator initialized");
    }

    private IEnumerator CoMainLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);

            PlacePortal();
        }
    }

    public void PlacePortal()
    {

        if (previous != null)
        {
            Destroy(previous.gameObject);
            previous = null;
        }

        if (current != null)
        {
            current.Value = false;
            previous = current;
            current = null;
        }

        Debug.Log($"{observedSceneObjects.Count} spatial awareness objects in scene");
        if (observedSceneObjects.Count > 0)
        {
            var spots = observedSceneObjects[SpatialAwarenessSurfaceTypes.Wall].Values.
                Where(x => { return x.Quads[0].Extents.x * x.Quads[0].Extents.y > 2f; });

            Debug.Log($"{spots.Count()} compatible objects");
            if (spots.Count() > 0)
            {
                var spot = spots.ToArray()[UnityEngine.Random.Range(0, spots.Count())];
                var go = Instantiate(PortalPrefab, Content);
                go.transform.SetPositionAndRotation(spot.Position + Offset, spot.Rotation);
                current = go.GetComponent<AnimationBoolTrigger>();
                current.Value = true;
                Game.Instance.CurrentPortal = go;
                Debug.Log($"Spawing portal at {spot.Position} from {spots.Count()} spots at {Time.time}s");

                //foreach (var quad in spot.Quads)
                //{
                //    //if (quad.Extents.x*quad.Extents.y > 1)
                //    var col = quad.Extents.x * quad.Extents.y > 2 ? Color.green : Color.red;
                //    col.a = 0.5f;
                //    quad.GameObject.GetComponent<Renderer>().material.color = col;

                //    //var indicator = Instantiate(IndicatorPrefab, go.transform);
                //    //indicator.transform.SetPositionAndRotation(spot.Position, spot.Rotation);
                //    //indicator.transform.localScale = new Vector3(spot.Quads[0].Extents.x, spot.Quads[0].Extents.y, .1f);
                //}
            }
        }
        else if (DebugMode)
        {
            var go = Instantiate(PortalPrefab, Content);
            go.transform.position = DebugOffset;
            current = go.GetComponent<AnimationBoolTrigger>();
            current.Value = true;
            Game.Instance.CurrentPortal = go;
            Debug.Log($"Spawing debug portal at {DebugOffset} at time {Time.time}s");
        }
    }

    protected override void OnEnable()
    {
        Debug.Log($"Registering event handlers");
        RegisterEventHandlers<IMixedRealitySpatialAwarenessObservationHandler<SpatialAwarenessSceneObject>, SpatialAwarenessSceneObject>();
    }

    protected override void OnDisable()
    {
        UnregisterEventHandlers<IMixedRealitySpatialAwarenessObservationHandler<SpatialAwarenessSceneObject>, SpatialAwarenessSceneObject>();
    }

    protected override void OnDestroy()
    {
        UnregisterEventHandlers<IMixedRealitySpatialAwarenessObservationHandler<SpatialAwarenessSceneObject>, SpatialAwarenessSceneObject>();
    }


    #region IMixedRealitySpatialAwarenessObservationHandler Implementations

    public void OnObservationAdded(MixedRealitySpatialAwarenessEventData<SpatialAwarenessSceneObject> eventData)
    {
        Debug.Log("Observation added");
        AddToData(eventData.Id);

        if (observedSceneObjects.TryGetValue(eventData.SpatialObject.SurfaceType, out Dictionary<int, SpatialAwarenessSceneObject> sceneObjectDict))
        {
            sceneObjectDict.Add(eventData.Id, eventData.SpatialObject);
        }
        else
        {
            observedSceneObjects.Add(eventData.SpatialObject.SurfaceType, new Dictionary<int, SpatialAwarenessSceneObject> { { eventData.Id, eventData.SpatialObject } });
        }

        ScanRoom.Invoke(observedSceneObjects[SpatialAwarenessSurfaceTypes.Wall].Values.
                Where(x => { return x.Quads[0].Extents.x * x.Quads[0].Extents.y > 2f; }).Count() < 1);
    }

    public void OnObservationUpdated(MixedRealitySpatialAwarenessEventData<SpatialAwarenessSceneObject> eventData)
    {
        UpdateData(eventData.Id);

        if (observedSceneObjects.TryGetValue(eventData.SpatialObject.SurfaceType, out Dictionary<int, SpatialAwarenessSceneObject> sceneObjectDict))
        {
            observedSceneObjects[eventData.SpatialObject.SurfaceType][eventData.Id] = eventData.SpatialObject;
        }
        else
        {
            observedSceneObjects.Add(eventData.SpatialObject.SurfaceType, new Dictionary<int, SpatialAwarenessSceneObject> { { eventData.Id, eventData.SpatialObject } });
        }
    }

    public void OnObservationRemoved(MixedRealitySpatialAwarenessEventData<SpatialAwarenessSceneObject> eventData)
    {
        RemoveFromData(eventData.Id);

        foreach (var sceneObjectDict in observedSceneObjects.Values)
        {
            sceneObjectDict?.Remove(eventData.Id);
        }

        ScanRoom.Invoke(observedSceneObjects[SpatialAwarenessSurfaceTypes.Wall].Values.
                Where(x => { return x.Quads[0].Extents.x * x.Quads[0].Extents.y > 2f; }).Count() < 1);
    }

    #endregion IMixedRealitySpatialAwarenessObservationHandler Implementations

}
[Serializable] public class ShowScanRoomEvent : UnityEvent<bool> { }