using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private SignalBus _signalBus;
    private InputManager _inputManager;
    private Player _player;
    private Projectile.Factory _projectileFactory;

    [SerializeField] private GameObject arrow;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float dashSpeed = 4.0f;
    [SerializeField] private float dashTime = 0.05f;
    
    private Rigidbody2D rb;

    private Vector3 _velocity; 
    private string _moveState = "walking";
    private Vector3 _movementDirection;
    private float _timer;
    public Animator animator;
    
    [Inject]
    private void Init(SignalBus signalBus, InputManager inputManager, Player player, Projectile.Factory projectileFactory)
    {
        _signalBus = signalBus;
        _inputManager = inputManager;
        _player = player;
        _projectileFactory = projectileFactory;
    }

    private void Start()
    {    
        rb = GetComponent<Rigidbody2D>();
        _signalBus.Fire(new CameraFollowTargetSignal() {Target = gameObject});
    }

    private void FixedUpdate()
    {
        if (_player.CanControl)
        {
            Move();
            Fire();
        }
    }

    // TODO: Fix state change bugs, explore the original velocity + smoothing method vs translate, fix MKB/controller bug as in the original
    private void Move()
    {
        float actualSpeed=0;
        Vector2 moveVelocity = new Vector2(_inputManager.Horizontal, _inputManager.Vertical);
        if (_moveState == "walking")
        {
            actualSpeed = moveSpeed;
            animator.SetFloat("speed_front",actualSpeed*_inputManager.Vertical);
            animator.SetFloat("speed_right",actualSpeed*_inputManager.Horizontal);
            _movementDirection = new Vector2(_inputManager.Horizontal, _inputManager.Vertical);
            _velocity = _movementDirection * actualSpeed;
            rb.velocity = _velocity;
            
            if (_inputManager.Dash)
            {
                _moveState = "dashing";
            }
        }
        else if (_moveState == "dashing")
        {
            actualSpeed = dashSpeed;
            animator.SetFloat("speed_front",actualSpeed*_movementDirection.y);
            animator.SetFloat("speed_right",actualSpeed*_movementDirection.x);
            _movementDirection = new Vector3(_inputManager.Horizontal, _inputManager.Vertical);
            _velocity = _movementDirection * actualSpeed;
            rb.velocity = _velocity;
            _timer += Time.deltaTime;
            
            if (_timer >= dashTime)
            {
                _moveState = "walking";
                _timer = 0.0f;
            }
        }  
    }

    // TODO: Use projectile factory
    private void Fire()
    {
        Vector3 mousePos = _inputManager.FirePosition;
        if (_inputManager.Fire)
        {
            //animator.SetTrigger("is_fire");
            float t = mousePos.x - transform.position.x;
            float u = mousePos.y - transform.position.y;
            var theta = Mathf.Atan(u / t);
           
            var degtheta = theta * Mathf.Rad2Deg;
            if (t < 0)
            {
                degtheta -= 180;
            }
            //triggers different shooting animation
            if (t > 0)
            {
                if (u > 0)
                {
                    if (u / t > 1)
                    {
                        animator.SetTrigger("is_back_fire");
                    }
                    else
                    {
                        animator.SetTrigger("is_right_fire");
                    }
                }
                else
                {
                    if (u / t > -1)
                    {
                        animator.SetTrigger("is_right_fire");
                    }
                    else
                    {
                        animator.SetTrigger("is_front_fire");
                    }
                }
                
            }
            else
            {
                if (u > 0)
                {
                    if (u / t > -1)
                    {
                        animator.SetTrigger("is_left_fire");
                    }
                    else
                    {
                        animator.SetTrigger("is_back_fire");
                    }
                }
                else
                {
                    if (u / t > 1)
                    {
                        animator.SetTrigger("is_front_fire");
                    }
                    else
                    {
                        animator.SetTrigger("is_left_fire");
                    }
                }
            }

            var projectileRotation = Quaternion.Euler(0, 0, degtheta);
            Vector3 projectileDirection = new Vector2(t, u).normalized;

            _projectileFactory.Create(arrow, transform.position, projectileRotation, projectileDirection);
        }
        
    }
}
