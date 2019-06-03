using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Zenject;

public class Player : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth = 100f;

    public float Health { get; private set; }
    public bool CanControl { get; private set; }

    private float _crossDist = 0.5f; 

    private GameObject _crosshair;
    private GameObject _player;
    private InputManager _inputManager;

    Vector3 DistVector;
    void Start()
    {
        Health = maxHealth;
        CanControl = true;

        _player = this.transform.Find("PlayerController").gameObject; 
        _crosshair = this.transform.Find("Crosshair").gameObject;
        //where cross hair art is placed
        var crosshairArt = _crosshair.GetComponent<SpriteRenderer> ();
        var basicCrosshairSprite = Resources.Load<Sprite>("crosshair");
        crosshairArt.sprite = basicCrosshairSprite;
    }

    public void Damage(float amount, float type)
    {
        Health = Mathf.Max(0f, Health - amount);
    }

    public void Heal(float amount)
    {
        Health = Mathf.Min(maxHealth, Health + amount);
    }
    
    
    public class Factory : PlaceholderFactory<Player>
    {
    }

    private void FixedUpdate()
    {
        CrossHairTrack();
    }
    
    
    //Calculate location of crosshair
    private void CrossHairTrack()
    {
        _inputManager = this.transform.Find("PlayerController").GetComponent<InputManager>();
        Vector3 PlayerPos = player.transform.position; 
        Vector3 mousePos = _inputManager.mousePos;
        
        var t = mousePos.x - PlayerPos.x;
        var u = mousePos.y - PlayerPos.y;
        

        var theta = Mathf.Atan(u / t);

        var crossX = _crossDist * Mathf.Cos(theta);
        var crossY = _crossDist * Mathf.Sin(theta);
        
        if (t >= 0)
        {
            DistVector = new Vector3(PlayerPos.x + crossX, PlayerPos.y + crossY, 0.0f);
        }
        else
        {
            DistVector = new Vector3(PlayerPos.x - crossX, PlayerPos.y - crossY, 0.0f);
        }
        _crosshair.transform.position = DistVector;
    }


}