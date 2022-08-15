using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBallDamage : Damage
{
    public GameObject ZapPrefab;
    public GameObject FlashPrefab;

    public float NormalRadius = 20;
    public float SuperChargedRadius = 200;

    private SphereCollider _collider;
    private bool _supercharged;

    private Dictionary<BaseCharacter, List<GameObject>> _effects = new Dictionary<BaseCharacter, List<GameObject>>();

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    protected override BaseCharacter OnTriggerEnter(Collider collider)
    {
        var character = base.OnTriggerEnter(collider);

        if (character != null && character is Enemy && !_effects.ContainsKey(character))
        {
            var zap = Instantiate(ZapPrefab, Room.Instance.transform);
            _effects.Add(character, new List<GameObject>());
            _effects[character].Add(zap);
            var zapEffect = zap.GetComponent<LightningFX>();
            zapEffect.PointA = transform;
            zapEffect.PointB = character.transform;

            var flash = Instantiate(FlashPrefab, character.transform);
            _effects[character].Add(flash);

            character.OnDeath.AddListener(()=>RemoveEffect(character));
        }

        return character;
    }

    protected override BaseCharacter OnTriggerExit(Collider other)
    {
        var character = base.OnTriggerExit(other);

        if (character != null)
        {
            character.OnDeath.RemoveListener(()=>RemoveEffect(character));
            RemoveEffect(character);
        }

        return character;
    }

    private void RemoveEffect(BaseCharacter character)
    {
        if (_effects.ContainsKey(character))
        {
            foreach (var effect in _effects[character])
            {
                Destroy(effect);
            }
            _effects.Remove(character);
        }
    }

    public void SetSupercharged(bool active)
    {
        if (active != _supercharged)
        {
            _collider.radius = active ? SuperChargedRadius : NormalRadius;
            _supercharged = active;

            if (active)
            {
                StopAllCoroutines();
                StartCoroutine(CoOffTimer());
            }
            else
            {
                OnDisable();
            }
        }
    }

    private IEnumerator CoOffTimer()
    {
        yield return new WaitForSeconds(1.5f);
        SetSupercharged(false);
    }

    protected override void OnDisable()
    {
        foreach (var effect in _effects)
        {
            RemoveEffect(effect.Key);
        }
        _effects.Clear();
        base.OnDisable();
    }
}
