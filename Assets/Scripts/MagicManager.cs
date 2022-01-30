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
        Magic.Add(magic);
    }

    public void Remove(Magic magic)
    {
        Magic.Remove(magic);
    }

    public void Clear()
    {
        var magics = Magic.ToArray();

        foreach (var magic in magics)
        {
            Magic.Remove(magic);
            Destroy(magic);
        }
    }

    public Magic GetPreparing()
    {
        return Magic.Where((magic) => { return magic.Stage == MagicStage.Preparing; }).First();
    }

    public void ActivateMagic()
    {
        Magic.ForEach(magic => magic.Activate());
    }
}
