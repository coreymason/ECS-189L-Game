using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour
{
    private Vector2 _projectileDirection;
    private Rigidbody2D _rigidbody2d;

    [Inject]
    private void Init(Vector2 fireDirection)
    {
        _projectileDirection = fireDirection;
    }

    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _rigidbody2d.AddForce(_projectileDirection * 500f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, Vector3, Quaternion, Vector2, Projectile>
    {
    }
}

public class ProjectileFactory : IFactory<UnityEngine.Object, Vector3, Quaternion, Vector2, Projectile>
{
    readonly DiContainer _container;
    
    public ProjectileFactory(DiContainer container)
    {
        _container = container;
    }

    public Projectile Create(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Vector2 projectileDirection)
    {
        // TODO: If that becomes a script, inject instead
        var parent = GameObject.Find("ProjectileManager").transform;
        return _container.InstantiatePrefabForComponent<Projectile>(prefab, position, rotation, parent, new object[] { projectileDirection });
    }
}