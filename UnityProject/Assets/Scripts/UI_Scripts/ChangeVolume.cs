using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Slider Volume;
    // NEED TO ATTACH SOUND SOURCE
    public AudioSource GameSounds;
    
    // Update is called once per frame
    void Update()
    {
        GameSounds.volume = Volume.value;
    }
}
