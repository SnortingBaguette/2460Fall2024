using UnityEngine;
using UnityEngine.Events;

public class TriggerEventBehaviour : MonoBehaviour
{
    public UnityEvent triggerEnterEvent, onEnableEvent, onDisableEvent, onStartEvent, triggerLeaveEvent, collisionEnterEvent;

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

    private void OnTriggerExit(Collider other)
    {
        triggerLeaveEvent.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collisionEnterEvent.Invoke();
        }
        
    }

}
