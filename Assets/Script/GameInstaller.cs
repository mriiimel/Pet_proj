using Enemy_Factory;
using Enemy_Config;
using Object_Pool;
using UnityEngine;
using Zenject;


public class GameInstaller : MonoInstaller
{
    [SerializeField] private ConfigAllEnemys _enemysConfig;
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private Factory _enemyFactory;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private HeroConfig _heroConfig;
    [SerializeField] private MenuPauseController _menuPauseController;
    [SerializeField] private ConfigAllEnemys _configAllEnemys;
    [SerializeField] private HealthPotionConfig _healthPotionConfig;
    
   
    public override void InstallBindings()
    {
        Container.Bind<ObjectPool>().FromInstance(_objectPool).AsCached();
        Container.Bind<ConfigAllEnemys>().FromInstance(_configAllEnemys).AsCached();
        Container.Bind<EnemyCounter>().FromInstance(_enemyCounter).AsSingle();
        Container.Bind<Factory>().FromInstance(_enemyFactory).AsSingle();
        Container.Bind<FactoryBase>().FromInstance(_enemyFactory).AsCached().NonLazy();
        Container.Bind<PlayerControllerBase>().FromInstance(_playerController).AsSingle();
        Container.Bind<PlayerController>().FromInstance(_playerController).AsCached();
        Container.Bind<AnimatorController>().FromInstance(_animatorController).AsSingle();
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<HeroConfig>().FromInstance(_heroConfig).AsSingle();
        Container.Bind<MenuPauseController>().FromInstance(_menuPauseController).AsSingle();
        Container.Bind<HeailhBehaviour>().AsSingle();
        Container.Bind<HealthPotionConfig>().FromInstance(_healthPotionConfig).AsSingle();
    }
}
