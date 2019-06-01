using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private InputManager _inputManager;
    private Player _player;

    [Range(0, 1f)] [SerializeField] private float velocitySmoothing = 0.05f;
    [SerializeField] private float baseSpeed = 20f;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _velocity = Vector3.zero;
    
    
    [Inject]
    private void Init(Player player, InputManager inputManager)
    {
        _inputManager = inputManager;
        _player = player;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_player.CanControl)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector2 moveVelocity = new Vector2(_inputManager.Horizontal, _inputManager.Vertical);
        
        // Reconcile keyboard and controller diagonals
        if (_inputManager.Source == InputManager.InputSource.MKB && moveVelocity.x != 0 && moveVelocity.y != 0)
        {
            moveVelocity *= 0.7f;
        }
        
        moveVelocity *= baseSpeed * Time.fixedDeltaTime;
        _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, moveVelocity, ref _velocity, velocitySmoothing);
    }
}
