using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private string _moveState = "walking";
    [SerializeField] private float Speed = 1.0f;
    //Serialized private fields flag a warning as of v2018.3. 
    //This pragma disables the warning in this one case.
    #pragma warning disable 0649
    
    //actual speed of the player
    private float modifiedSpeed = 1.0f;
    //Speed of the actual dash
    private float dashSpeed = 5.0f;
    //length in time of the dash
    private float dashTimer = 0.1f;
    //just a timer to compare to the dashTimer
    private float timer;
    
    private Vector3 _movementDirection; 
    private InputManager _inputManager;
    private Player _player;

    private Vector3 mousePos;


    public GameObject Arrow;

    [Inject]
    
    private void Init(Player player, InputManager inputManager)
    {
        _inputManager = inputManager;
        _player = player;
        _player.tag = "Player";
    }

    private Vector3 GetPlayerPosition()
    {
        return transform.position;
        
    }
    
    private void Start()
    {
        //where you place the main character art
        var player_art = GetComponent<SpriteRenderer> ();
        var basic_char_sprite = Resources.Load<Sprite>("basic_char");
        player_art.sprite = basic_char_sprite;
    }

    private void FixedUpdate()
    {
        if (_player.CanControl)
        {
                Move();
                 
                //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                // Debug.Log("x" + Input.GetAxis("Mouse X")); 
                // Debug.Log("y" + mousePos.y); 

                Fire(); 
        }
    }

    private void Move()
    {
        if (_moveState == "walking")
        {
            modifiedSpeed = Speed;
            _movementDirection = new Vector3(_inputManager.Horizontal, _inputManager.Vertical, 0.0f);
            gameObject.transform.Translate(_movementDirection * Time.deltaTime * modifiedSpeed);

            if (_inputManager.Dash) //dash
            {
                modifiedSpeed = dashSpeed;
                _moveState = "dashing";
            }
        }
        else if (_moveState == "dashing")
        {
            modifiedSpeed = dashSpeed;
            gameObject.transform.Translate(_movementDirection * Time.deltaTime * modifiedSpeed);
            timer += Time.deltaTime;
            if (timer >= dashTimer)
            {
                _moveState = "walking";
                timer = 0.0f; 
            }
        }
    }

    private void Fire()
    {
        mousePos = _inputManager.mousePos;  //new Vector3(_inputManager.FireHorizontal, _inputManager.FireVertical,0); 
        if (_inputManager.Fire)
        {
            float t = mousePos.x - transform.position.x;
            float u = mousePos.y - transform.position.y;
            var theta = Mathf.Atan(u / t);
            var degtheta = theta * Mathf.Rad2Deg;
            if (t < 0)
            {
                degtheta -= 180;
            }
            
            GameObject CurrentArrow = Instantiate(Arrow, GetPlayerPosition(), Quaternion.Euler(0, 0, degtheta));
        }
        
    }
}
