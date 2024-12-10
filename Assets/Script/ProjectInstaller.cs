using Zenject;
using UnityEngine;
using Enemy_Config;
using Camera_Controller;


public class ProjectInstaller : MonoInstaller
{
    
    [SerializeField] private HeroConfig _heroConfig;
    [SerializeField] private ConfigAllEnemys _configAllEnemys;
    [SerializeField] private HealthPotionConfig _healthPotionConfig;
    
    

   


    public override void InstallBindings()
    {
        EnemyConfigBindings();
        HeroConfigBindings();
        HealthConfigBindings();
        

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
