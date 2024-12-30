using UnityEngine.Events;

public interface IToggleable
{
    public UnityEvent OnToggle { get; }
    public UnityEvent OnEnable { get; }
    public UnityEvent OnDisable { get; }

    public bool Enabled { get; }

    public void Toggle();
}