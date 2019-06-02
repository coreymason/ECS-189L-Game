using UnityEngine;
using Zenject;

public class Crosshair : MonoBehaviour
{

    public bool playerGun;
    
    private void Start()
    {
        playerGun = true; 
    }
    
    public class Factory : PlaceholderFactory<Crosshair>
    {
    }
    
}
