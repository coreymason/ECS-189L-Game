using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class ChangeVolume : MonoBehaviour
{
    private AudioManager _audioManager;
    [FormerlySerializedAs("Volume")] public Slider volume;

    [Inject]
    private void Init(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    
    void Update()
    {
        _audioManager.setVolume(volume.value);
    }
}
