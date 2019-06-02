using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WorldManager : MonoBehaviour
{
    private Player.Factory _playerFactory;
    private List<Player> _players;

    [Inject]
    private void Init(Player.Factory playerFactory, List<Player> players)
    {
        _playerFactory = playerFactory;
        _players = players;
    }

    private void Start()
    {
        _playerFactory.Create();
    }

    //For now if the user presses the escape key, he/she can leave the game
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }
}
