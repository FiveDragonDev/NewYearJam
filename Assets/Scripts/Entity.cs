using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour, IEntity
{
    public UnityEvent OnLose => _onLose;

    private readonly UnityEvent _onLose = new();

    private void Update()
    {
        if (transform.position.y < -5) OnLose.Invoke();
    }
}
