using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GazeMenu : MonoBehaviour
{
    public SetPosition SelectionIndicator;
    public Transform NoSelection;
    public Menu Menu;
    public List<GazeSelectionTarget> Targets = new List<GazeSelectionTarget>();

    public UnityEvent OnOptionSelected;

    public GazeSelectionTarget CurrentTarget
    {
        get => _currentTarget;
        set => _currentTarget = value;
    }

    private GazeSelectionTarget _currentTarget;

    private void Awake()
    {
        foreach (var target in GetComponentsInChildren<GazeSelectionTarget>())
        {
            Targets.Add(target);
            target.OnSelect.AddListener(() => { Select(target); });
            target.OnDeselect.AddListener(() => { Deselect(target); });
            Menu.OnShow.AddListener(target.Open);
            Menu.OnHide.AddListener(target.Close);
        }
        Menu.OnShow.AddListener(() => SelectionIndicator.GetComponent<AnimationBoolTrigger>().Value = true);
        Menu.OnHide.AddListener(() => SelectionIndicator.GetComponent<AnimationBoolTrigger>().Value = false);
    }

    private void Select(GazeSelectionTarget target)
    {
        _currentTarget = target;
        SelectionIndicator.Call(target.transform);
    }

    private void Deselect(GazeSelectionTarget target)
    {
        if (_currentTarget == target)
        {
            _currentTarget = null;
            SelectionIndicator.Call(NoSelection);
        }
    }

    public void Call()
    {
        if (_currentTarget != null)
        {
            Debug.Log($"GazeMenu: call {_currentTarget.name}");
            _currentTarget.Call();
            OnOptionSelected.Invoke();
        }
    }
}
