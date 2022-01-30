using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeightedAveragePosition : MonoBehaviour
{
    public Transform[] Positions;

    private Vector3[] _pos;
    private float[] _weight;

    private void Start()
    {
        _pos = new Vector3[Positions.Length];
        _weight = new float[Positions.Length];
    }

    public void Update()
    {
        var totalWeight = 0f;
        var i = 0;
        foreach (var pos in Positions)
        {
            var weight = 0f;
            foreach (var pos2 in Positions)
            {
                if (!pos.Equals(pos2))
                {
                    weight += (pos2.position - pos.position).sqrMagnitude;
                }
            }
            totalWeight += weight;
            _weight[i] = weight;
            //Debug.Log($"Weight{i}[{pos.name}]={weight}");

            _pos[i] = pos.position;
            i++;
        }
        //Debug.Log($"TotalWeight={totalWeight}");

        var avg = new Vector3();
        for (i = 0; i < _pos.Length; i++)
        {
            avg += _pos[i] * _weight[i];
            //Debug.Log($"Pos[{Positions[i].name}]={_pos[i]}*{_weight[i]}={_pos[i] * _weight[i]}");
        }
        avg /= ( totalWeight);
        //Debug.Log($"Avg={avg}");

        transform.position = avg;
    }
}
