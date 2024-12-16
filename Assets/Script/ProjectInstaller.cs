using Zenject;
using UnityEngine;
using Enemy_Config;


public class ProjectInstaller : MonoInstaller
{
    
    [SerializeField] private HeroConfig _heroConfig;
    [SerializeField] private ConfigAllEnemys _configAllEnemys;
    [SerializeField] private HealthPotionConfig _healthPotionConfig;
    [SerializeField] private PlayerView _player;

    private PlayerModel _playerModel;
    private PlayerController _playerController;
    private PlayerInput _playerInput;
    private CameraView _cameraController;
    private UIController _uiController;
    private AnimatorController _animatorController;


    public override void InstallBindings()
    {
        EnemyConfigBindings();
        HeroConfigBindings();
        HealthConfigBindings();
        PlayerBindings();

    }

    private void PlayerBindings()
    {
        _player = Container.InstantiatePrefabForComponent<PlayerView>(_player);
        Container.Bind<PlayerView>().FromInstance(_player).AsSingle();
        //Container.Bind<PlayerInput>().AsSingle();
        //Container.Bind<PlayerModel>().AsSingle().WithArguments(_player,Container);
        //Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().WithArguments(_player);
    }

    private void HealthConfigBindings()
    {
        Container.Bind<HealthPotionConfig>().FromInstance(_healthPotionConfig).AsSingle();
    }

    private void HeroConfigBindings()
    {
        Container.Bind<HeroConfig>().FromInstance(_heroConfig).AsSingle();
    }

    private void EnemyConfigBindings()
    {
        Container.Bind<ConfigAllEnemys>().FromInstance(_configAllEnemys).AsCached();
    }

    
}
