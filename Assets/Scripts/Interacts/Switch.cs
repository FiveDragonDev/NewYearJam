using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour, IInteractable, IToggleable
{
    public bool Enabled => _enabled;

    public UnityEvent OnToggle => _onToggle;
    public UnityEvent OnEnable => _onEnable;
    public UnityEvent OnDisable => _onDisable;

    public UnityEvent OnInteract => _onInteract;

    private bool _enabled;

    private readonly UnityEvent _onToggle = new();
    private readonly UnityEvent _onEnable = new();
    private readonly UnityEvent _onDisable = new();

    private readonly UnityEvent _onInteract = new();

    public void Interact()
    {
        Toggle();
        OnInteract.Invoke();
    }
    public void Toggle()
    {
        _enabled = !_enabled;
        if (_enabled) OnEnable.Invoke();
        else OnDisable.Invoke();
        OnToggle.Invoke();
    }
}
