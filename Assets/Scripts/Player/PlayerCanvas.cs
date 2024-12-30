using System.Collections;
using TMPro;
using UnityEngine;

public sealed class PlayerCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup _crosshair;

    [SerializeField] private TextMeshProUGUI _shortInfoText;

    [SerializeField] private CanvasGroup _dialogueBox;
    [SerializeField] private TextMeshProUGUI _dialogueBoxText;

    private bool _dialogueBoxActive;
    private bool _crosshairActive;

    private float _shortInfoTime = 1;
    private CanvasGroup _shortInfo;

    private static PlayerCanvas _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        _shortInfo = _shortInfoText.GetComponent<CanvasGroup>();

        _dialogueBox.alpha = 0;
        _shortInfo.alpha = 0;
        _crosshair.alpha = 0;
    }
    private void Update()
    {
        _shortInfoTime += Time.deltaTime;
        _shortInfo.alpha = GetAlpha(_shortInfoTime);

        _crosshair.alpha = Mathf.Lerp(_crosshair.alpha,
            _crosshairActive ? 1 : 0, Time.deltaTime * 8);

        _dialogueBox.alpha = Mathf.Lerp(_dialogueBox.alpha,
            _dialogueBoxActive ? 1 : 0, Time.deltaTime * 8);
    }

    public static void SetDialogueBoxActive(bool active) => _instance._dialogueBoxActive = active;
    public static void SetDialogueBoxText(string text, float duration = 1) =>
        _instance.StartCoroutine(_instance.DrawText(text, duration));

    public static void SetCrosshair(bool active) => _instance._crosshairActive = active;

    public static void SetShortInfo(string text)
    {
        _instance._shortInfoText.text = text;
        _instance._shortInfoTime = 0;
    }

    private float GetAlpha(float time) => 1 - Mathf.Clamp01(time * time);
    private IEnumerator DrawText(string text, float duration)
    {
        _dialogueBoxText.text = string.Empty;
        float charDrawTime = Mathf.Max(duration, 0) / text.Length;
        if (charDrawTime > 0)
        {
            foreach (var @char in text)
            {
                _dialogueBoxText.text += @char;
                yield return new WaitForSeconds(charDrawTime);
            }
        }
        _dialogueBoxText.text = text;
    }
}
