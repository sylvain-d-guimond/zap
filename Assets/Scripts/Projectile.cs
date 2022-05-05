using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{
    public Component[] Visuals;
    public VisualEffect Explosion;
    public AudioSource Sound;
    public float SoundOffset;

    //void OnTriggerEnter(Collision collision)
    //{
    //    var enemy = collision.gameObject.GetComponent<Enemy>();
    //    if (enemy == null)
    //    {
    //        Debug.Log($"Trigger with {this.name} and {collision.gameObject.name}");
    //        Explosion.Play();
    //        StartCoroutine(CoDelayedDestroy());
    //    }
    //}

    void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.Log($"Collision with {this.name} and {collision.gameObject.name}");
            Sound.time = SoundOffset;
            Sound.Play();
            Explosion.Play();
            StartCoroutine(CoDelayedDestroy());
        }
    }

    private IEnumerator CoDelayedDestroy()
    {
        foreach (var vis in Visuals) Destroy(vis);
        Destroy(GetComponent<Collider>());

        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
    }
}
