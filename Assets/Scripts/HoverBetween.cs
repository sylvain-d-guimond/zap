using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class HoverBetween : MonoBehaviour
{
    public Transform[] Positions;

    public float Damping = 1;

    public void LateUpdate()
    {
        if (Positions.Length > 0)
        {
            var newPos = new Vector3();

            foreach (var pos in Positions)
            {
                newPos += pos.position;
            }
            newPos /= Positions.Length;

            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime / Damping);
        }
    }
}
