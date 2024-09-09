using Zenject;
using UnityEngine;
using Enemy_Config;


public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private HeroConfig _heroConfig;
    [SerializeField] private ConfigAllEnemys _configAllEnemys;
    [SerializeField] private HealthPotionConfig _healthPotionConfig;
    [SerializeField] private PlayerController _playerController;



    public override void InstallBindings()
    {
        Container.Bind<ConfigAllEnemys>().FromInstance(_configAllEnemys).AsCached();
        Container.Bind<EnemyCounter>().FromInstance(_enemyCounter).AsSingle();
        Container.Bind<HeroConfig>().FromInstance(_heroConfig).AsSingle();
        Container.Bind<IHeailhBehaviour>().To<PlayerControllerBase>().FromComponentInNewPrefab(_playerController).AsSingle();
        Container.Bind<HealthPotionConfig>().FromInstance(_healthPotionConfig).AsSingle();
        
    }
}
