using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSelectionIndicator : MonoBehaviour
{
    public GameObject[] Objects;

    private List<GameObject> _myObjects = new List<GameObject>();

    public GazeSelectionIndicator Get()
    {
        foreach (var obj in Objects)
        {
            var newObj = Instantiate(obj, this.transform);
            newObj.transform.localPosition = Vector3.zero;

            _myObjects.Add(newObj);
        }

        return this;
    }

    public void Put()
    {
        foreach (var obj in _myObjects)
        {
            Destroy(obj);
        }

        _myObjects.Clear();
        transform.localPosition = Vector3.zero;
    }
}
