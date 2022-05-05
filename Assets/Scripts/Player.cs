using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    private void Start()
    {
        Game.Instance.CurrentPlayer = this;
    }
}
