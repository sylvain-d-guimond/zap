using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    private bool _deactivating;

    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    public UnityEvent OnDeactivateFinished;

    public UnityEvent OnShow;
    public UnityEvent OnShowFinished;

    public UnityEvent OnHide;
    public UnityEvent OnHideFinished;

    public float Delay = 2f;

    public void Activate()
    {
        OnActivate.Invoke();
    }

    public void Deactivate()
    {
        OnDeactivate.Invoke();
    }

    public void DeactivateFinished()
    {
        OnDeactivateFinished.Invoke();
    }

    public void Show()
    {
        if (DebugMode.instance.DebugLevel <= DebugLevels.Debug) Debug.Log($"Show {gameObject.name}");
        OnShow.Invoke();
    }

    public void DelayedHide()
    {
        StartCoroutine(CoDelay());
    }

    private IEnumerator CoDelay()
    {
        yield return new WaitForSeconds(Delay);
        Hide();
    }

    public void Hide()
    {
        if (DebugMode.instance.DebugLevel <= DebugLevels.Debug) Debug.Log($"Hide {gameObject.name}");
        OnHide.Invoke();
    }

    public void ShowFinished()
    {
        if (DebugMode.instance.DebugLevel <= DebugLevels.Debug) Debug.Log($"Show {gameObject.name} finished");
        OnShowFinished.Invoke();
    }

    public void HideFinished()
    {
        OnHideFinished.Invoke();

        if (_deactivating)
        {
            OnDeactivateFinished.Invoke();

            _deactivating = false;
        }
    } 

    public void HideAndDeactivate()
    {
        _deactivating = true;

        Hide();
    }
}