using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private TMP_Dropdown _qualityDropdown;

    private Resolution[] _resolutions;
    private int _currentResolutionIndex;

    private void Start()
    {
        InitializeResolutions();
    }

    private void InitializeResolutions()
    {
        _resolutions = Screen.resolutions;

        _resolutionDropdown.ClearOptions();
        List<string> resolutions = new();
        for (int i = 0; i < _resolutions.Length; i++)
        {
            var resolution = _resolutions[i];
            resolutions.Add($"{resolution.width} x {resolution.height}");
            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
                _currentResolutionIndex = i;
        }
        _resolutionDropdown.AddOptions(resolutions);
        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void Play() => SceneManager.LoadScene(1);

    public void SetQuality(int index) => QualitySettings.SetQualityLevel(index);
    public void SetResolution(int index) =>
        Screen.SetResolution(_resolutions[index].width,
        _resolutions[index].height, Screen.fullScreen);
}
