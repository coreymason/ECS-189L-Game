using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
