using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private string moveState = "walking";
    [SerializeField] private float Speed = 1.0f;
    //Serialized private fields flag a warning as of v2018.3. 
    //This pragma disables the warning in this one case.
    #pragma warning disable 0649
    
    //actual speed of the player
    private float ModifiedSpeed = 1.0f;
    //Speed of the actual dash
    private float DashSpeed = 5.0f;
    //length in time of the dash
    private float dashTimer = 0.1f;
    //just a timer to compare to the dashTimer
    private float timer = 0.0f;
    
    private Vector3 MovementDirection; 
    private InputManager _inputManager;
    private Player _player;

    private Vector3 mousePos;
    

    [Range(0, 1f)] [SerializeField] private float velocitySmoothing = 0.05f;
    [SerializeField] private float baseSpeed = 20f;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _velocity = Vector3.zero;

    public GameObject Arrow;

    [Inject]
    
    private void Init(Player player, InputManager inputManager)
    {
        _inputManager = inputManager;
        _player = player;
        _player.tag = "Player";
    }

    public Vector3 GetPlayerPosition()
    {
        return this.transform.position;
        
    }
    
    private void Start()
    {
        //where you place the main character art
        var player_art = this.GetComponent<SpriteRenderer> ();
        var basic_char_sprite = Resources.Load<Sprite>("basic_char");
        player_art.sprite = basic_char_sprite;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_player.CanControl)
        {
            
            Move();
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Fire(); 
        }
    }

    private void Move()
    {
        if (moveState == "walking")
        {
            this.ModifiedSpeed = this.Speed;
            this.MovementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            this.gameObject.transform.Translate(this.MovementDirection * Time.deltaTime * this.ModifiedSpeed);

            if (Input.GetKeyDown("space")) //dash
            {
                this.ModifiedSpeed = this.DashSpeed;
                moveState = "dashing";
            }
        }
        else if (moveState == "dashing")
        {
            this.ModifiedSpeed = this.DashSpeed;
            this.gameObject.transform.Translate(this.MovementDirection * Time.deltaTime * this.ModifiedSpeed);
            timer += Time.deltaTime;
            if (timer >= dashTimer)
            {
                moveState = "walking";
                timer = 0.0f; 
            }
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float t = mousePos.x - this.transform.position.x;
            float u = mousePos.y - this.transform.position.y;
        

            var theta = Mathf.Atan(u / t);
            var degtheta = theta * Mathf.Rad2Deg;
            if (t < 0)
            {
                degtheta = degtheta - 180;
            }
            
            GameObject CurrentArrow = Instantiate(Arrow, GetPlayerPosition(), Quaternion.Euler(0, 0, degtheta));
            Debug.Log("Here is the current player position");
            Debug.Log(mousePos);
        }
        
    }
}
