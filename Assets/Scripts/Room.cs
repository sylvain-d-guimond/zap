using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public static Room Instance;

    private void OnEnable()
    {
        Instance = this;
    }
}
