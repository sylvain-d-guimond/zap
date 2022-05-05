using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Explosion : MonoBehaviour
{
    public float Duration = 0.5f;
    public VisualEffect Effect;

    private void Start()
    {
        
    }

    private IEnumerator CoDelayDestroy(float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(this.gameObject);
    }
}
