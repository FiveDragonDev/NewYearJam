using UnityEngine;

public class PlayerBreath : MonoBehaviour
{
    [SerializeField, Min(0)] private float _duration = 3;

    private AudioSource _audio;
    private float _nextBreathTime;

    private void Start() => _audio = GetComponent<AudioSource>();
    private void Update()
    {
        if (Time.time > _nextBreathTime)
        {
            _audio.Play();
            _nextBreathTime = Time.time + _duration;
        }
    }
}
