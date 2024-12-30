using UnityEngine;

public class Bro : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Dialogue _startDialogue;
    [SerializeField] private Dialogue _idleDialogue;
    [SerializeField] private Dialogue _finishDialogue;
    [SerializeField] private Dialogue _preFinishDialogue;
    [SerializeField] private Dialogue _postFinishDialogue;

    [SerializeField, Min(0)] private float _radius = 1;

    private bool _firstTime = true;
    private bool _complete;

    private void Update()
    {
        transform.forward = _player.position - transform.position;

        bool hasPlayer = false;
        bool hasChampagne = false;
        var colliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (var collider in colliders)
        {
            if (collider.GetComponentInParent<PlayerDialogue>())
            {
                hasPlayer = true;
                continue;
            }
            if (collider.GetComponent<Champagne>())
            {
                hasChampagne = true;
                continue;
            }
            if (hasPlayer && hasChampagne) break;
        }

        if (hasPlayer && !PlayerDialogue.HasDialogue)
        {
            Dialogue dialogue;
            if (_complete) dialogue = _postFinishDialogue;
            else
            {
                if (!hasChampagne)
                {
                    if (_firstTime) dialogue = _startDialogue;
                    else dialogue = _idleDialogue;
                }
                else
                {
                    if (_firstTime) dialogue = _preFinishDialogue;
                    else dialogue = _finishDialogue;
                    _complete = true;
                }
            }
            PlayerDialogue.OpenDialogue(dialogue.Text, dialogue.Duration, dialogue.CloseTime);
        }

        if (hasPlayer && _firstTime) _firstTime = false;
    }
    private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, _radius);
}
