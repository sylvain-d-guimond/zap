using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MagicManager : MonoBehaviour
{
    public static MagicManager Instance;

    public UnityEvent OnActivate;

    public List<Magic> Magic = new List<Magic>();

    private int counter;

    private void Start()
    {
        Instance = this;
    }

    public void Activate()
    {
        OnActivate.Invoke();
    }

    public void Add(Magic magic)
    {
        magic.gameObject.name += $" {++counter}";
        Debug.Log($"Added: {magic.name}");
        Magic.Add(magic);
    }

    public void Remove(Magic magic)
    {
        Magic.Remove(magic);
    }

    public void Clear()
    {
        var magics = FindObjectsOfType<Magic>();
        Debug.Log($"Clear {magics.Length} magics");

        foreach (var magic in magics)
        {
            if (Magic.Contains(magic)) Magic.Remove(magic);
            Destroy(magic.gameObject);
        }
    }

    public void Stop()
    {
        Debug.Log($"Stop {Magic.Count} magics");
        var magics = Magic.ToArray();

        foreach (var magic in magics)
        {
            Debug.Log($"Magic {magic.gameObject.name} is {magic.Stage}");
            if (magic.Stage != MagicStage.Thrown)
            {
                Magic.Remove(magic);
                Destroy(magic.gameObject);
            }
        }
    }

    public Magic GetPreparing()
    {
        var magic = Magic.Where((magic) => { return magic.Stage == MagicStage.Prepare; }).First();
        Debug.Log($"Get preparing: {magic.name}");
        return magic;
    }

    public void ActivateMagic()
    {
        Magic.ForEach(magic => magic.Activate());
    }
}
