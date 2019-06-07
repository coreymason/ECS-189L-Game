using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private InputManager _inputManager;
    private Player _player;

    [SerializeField] private GameObject arrow;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 5.0f;
    [SerializeField] private float dashTime = 0.1f;
    
    private string _moveState = "walking";
    private Vector3 _movementDirection;
    private float _timer;
    public Animator animator;
    
    [Inject]
    private void Init(InputManager inputManager, Player player)
    {
        _inputManager = inputManager;
        _player = player;
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
        float actualSpeed;
        
        if (_moveState == "walking")
        {
            actualSpeed = moveSpeed;
            animator.SetFloat("speed_front",actualSpeed*_inputManager.Vertical);
            animator.SetFloat("speed_right",actualSpeed*_inputManager.Horizontal);
            _movementDirection = new Vector3(_inputManager.Horizontal, _inputManager.Vertical, 0.0f);
            gameObject.transform.Translate(Time.deltaTime * actualSpeed * _movementDirection);
            
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
            gameObject.transform.Translate(Time.deltaTime * actualSpeed * _movementDirection);
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
            animator.SetTrigger("is_fire");
            float t = mousePos.x - transform.position.x;
            float u = mousePos.y - transform.position.y;
            var theta = Mathf.Atan(u / t);
            var degtheta = theta * Mathf.Rad2Deg;
            if (t < 0)
            {
                degtheta -= 180;
            }
            
            GameObject currentArrow = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, degtheta));
        }
        
    }
}
