using UnityEngine;
using Zenject;

public class PlayerFactory
{
    private DiContainer _container;
    private PlayerView _playerView;
    private Camera _camera;

    public PlayerFactory(DiContainer container)
    {
        _container = container;
    }

    public void CreatePlayer(Transform playerSpawn)
    {
        _playerView = _container.Resolve<PlayerView>();
        _camera = _container.Resolve<Camera>();

        _playerView.transform.position = playerSpawn.position;
        _camera.transform.position = playerSpawn.position;
    }
}

