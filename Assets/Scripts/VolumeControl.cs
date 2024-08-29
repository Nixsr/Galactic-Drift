// VolumeControl.cs
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = AudioManager.Instance.GetVolume();
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        AudioManager.Instance.SetVolume(volume);
    }
}