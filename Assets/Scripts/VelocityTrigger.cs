using Microsoft.MixedReality.Toolkit.Utilities;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class VelocityTrigger : MonoBehaviour
{
    public TrackedHandJoint TrackedJoint;
    public Handedness Hand;
    public float Speed;
    public bool Active;
    public bool DeactivateOnTrigger = true;

    [SerializeField] private UnityEvent _onTrigger;

    private Vector3 _previousPosition;
    private bool _init;

    HandManager hand => HandManager.Instance;

    Task _resetVelocity;

    private void Start()
    {
        _resetVelocity = ResetMaxVelocity();
    }

    public void SetActive(bool active)
    {
        if (Active = active) _init = false;
    }

    private void Update()
    {
        if (Active)
        {
            if (!hand.IsHandTracked(Hand)) { _init = false; }

            if (_init)
            {
                var velocity = (hand.Get(Hand, TrackedJoint).position - _previousPosition).magnitude;
                if (velocity > Speed)
                {
                    _onTrigger.Invoke();
                    if (DeactivateOnTrigger) Active = false;
                }

                if (HandDebugPanel.Instance != null && velocity > HandDebugPanel.Instance.MaxVelocity) HandDebugPanel.Instance.MaxVelocity = velocity;
            }
            else _init = true;
        }

        if (HandDebugPanel.Instance != null) HandDebugPanel.Instance.Velocity.text = (hand.Get(Hand, TrackedJoint).position - _previousPosition).magnitude.ToString();
            _previousPosition = hand.Get(Hand, TrackedJoint).position;
        
    }

    async Task ResetMaxVelocity()
    {
        while (Application.isPlaying)
        {
            await Task.Delay(10000);
            if (HandDebugPanel.Instance != null) HandDebugPanel.Instance.MaxVelocity = 0f;
        }
    }
}
