using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WorldManager : MonoBehaviour
{
    private Player.Factory _playerFactory;
    public List<Player> _players;

    private Enemy.Factory _enemyFactory;
    private List<Enemy> _enemies;

    private Crosshair.Factory _crosshairFactory;
    private List<Crosshair> _crosshairs;
    
    [Inject]
    private void Init(Player.Factory playerFactory, List<Player> players, Crosshair.Factory crosshairFactory, List<Crosshair> crosshairs)
    {
        _playerFactory = playerFactory;
        _players = players;

        _crosshairFactory = crosshairFactory;
        _crosshairs = crosshairs; 
    }

    private void Start()
    {
        _playerFactory.Create();

        _crosshairFactory.Create();
    }

    //For now if the user presses the escape key, he/she can leave the game
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }
}
