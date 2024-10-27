using UnityEngine;
using UnityEngine.Events;

public class TriggerEventBehaviour : MonoBehaviour
{
    public UnityEvent triggerEnterEvent, onEnableEvent, onDisableEvent, onStartEvent;

    private void OnTriggerEnter(Collider other)
    {
        triggerEnterEvent.Invoke();
    }

    private void OnEnable()
    {
        onEnableEvent.Invoke();
    }

    private void OnDisable()
    {
        onDisableEvent.Invoke();
    }

    private void Start()
    {
        onStartEvent.Invoke();
    }
    
}