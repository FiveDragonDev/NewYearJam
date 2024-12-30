using UnityEngine;

[System.Serializable]
public sealed class Dialogue
{
    [TextArea(3, 8)] public string Text;
    [Min(0)] public float Duration = 1;
    [Min(0)] public float CloseTime = 1;
}
public class PlayerDialogue : MonoBehaviour
{
    public static bool HasDialogue => Time.time < _instance._closeTime;

    private float _closeTime = -1;
    private static PlayerDialogue _instance;

    private void Start()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
    }
    private void Update() => PlayerCanvas.SetDialogueBoxActive(Time.time < _closeTime);

    public static void OpenDialogue(string text, float duration, float openDuration)
    {
        _instance._closeTime = Time.time + openDuration;
        PlayerCanvas.SetDialogueBoxText(text, duration);
    }
}
