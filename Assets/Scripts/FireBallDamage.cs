using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.VFX;

public class FireBallDamage : Damage
{
    public Animator ExplosionAnimation;
    public string ExplosionAnimationName;
    public GameObject FireEffect;
    public VisualEffect FlashEffect;

    private Dictionary<BaseCharacter, List<GameObject>> _effects = new Dictionary<BaseCharacter, List<GameObject>>();
    private bool _triggered;

    protected override BaseCharacter OnTriggerEnter(Collider other)
    {
        var character = base.OnTriggerEnter(other);

        if (character != null)
        {
            _effects.Add(character, new List<GameObject>());

            var fire = Instantiate(FireEffect, character.transform);
            _effects[character].Add(fire);

            character.OnDeath.AddListener(() => RemoveEffect(character));
        }

        return character;
    }

    protected override BaseCharacter OnTriggerExit(Collider other)
    {
        var character = base.OnTriggerExit(other);

        if (character != null)
        {
            character.OnDeath.RemoveListener(() => RemoveEffect(character));
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

    public void Trigger()
    {
        if (!_triggered)
        {
            _triggered = true;
            gameObject.SetActive(true);
            ExplosionAnimation.Play(ExplosionAnimationName);
            FlashEffect.Play();
        }
    }

    private void OnDestroy()
    {
        foreach (var character in _effects.Keys)
        {
            RemoveEffect(character);
        }
        _effects.Clear();
    }
}
