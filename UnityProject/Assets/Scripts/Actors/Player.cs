using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Zenject;

public class Player : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth = 100f;

    public float Health { get; private set; }
    public bool CanControl { get; private set; }

    private float crossDist = 0.5f; 

    private GameObject Crosshair;
    private GameObject player;
    private InputManager _inputManager;

    Vector3 DistVector;
    void Start()
    {
        Health = maxHealth;
        CanControl = true;

        player = this.transform.Find("PlayerController").gameObject; 
        Crosshair = this.transform.Find("Crosshair").gameObject;
        //where cross hair art is placed
        var crosshair_art = Crosshair.GetComponent<SpriteRenderer> ();
        var basic_crosshair_sprite = Resources.Load<Sprite>("crosshair");
        crosshair_art.sprite = basic_crosshair_sprite;
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
        Vector3 mousePos;
        mousePos = _inputManager.mousePos; //new Vector3(_inputManager.FireHorizontal, _inputManager.FireVertical,0); 
        // mousePos.x = Input.GetAxis("Mouse X")*100*Time.deltaTime;
        // mousePos.y = _inputManager.FireVertical; 
        // mousePos.y = Input.GetAxis("Mouse Y")*100*Time.deltaTime;
        // mousePos.z = 0; 

       // mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        
        float t = mousePos.x - PlayerPos.x;
        float u = mousePos.y - PlayerPos.y;
        

        var theta = Mathf.Atan(u / t);

        float crossX = crossDist * Mathf.Cos(theta);
        float crossY = crossDist * Mathf.Sin(theta);
        
        if (t >= 0)
        {
            DistVector = new Vector3(PlayerPos.x + crossX, PlayerPos.y + crossY, 0.0f);
        }
        else
        {
            DistVector = new Vector3(PlayerPos.x - crossX, PlayerPos.y - crossY, 0.0f);
        }
        Crosshair.transform.position = DistVector;
    }


}