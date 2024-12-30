using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Pickupable : MonoBehaviour, IEntity, IInteractable
{
    public UnityEvent OnLose => _onLose;

    public UnityEvent OnInteract => _onInteract;
    public UnityEvent OnThrow => _onThrow;
    public UnityEvent OnPlace => _onPlace;

    private readonly UnityEvent _onLose = new();

    private readonly UnityEvent _onInteract = new();
    private readonly UnityEvent _onThrow = new();
    private readonly UnityEvent _onPlace = new();

    private void Update()
    {
        if (transform.position.y < -5) OnLose.Invoke();
    }

    public void Interact() => OnInteract.Invoke();
    public void Throw() => OnThrow.Invoke();
    public void Place() => OnPlace.Invoke();
}
