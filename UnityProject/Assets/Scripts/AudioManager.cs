using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    // TODO: actually manage audio lol
    
    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
