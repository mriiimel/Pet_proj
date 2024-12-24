using Zenject;


public class FactoryController: ITickable,IInitializable
{
    private readonly FactoryModel _factoryModel;
    private DiContainer _container;
    private Factory _factory;
    private PlayerFactory _playerFactory;
    private EnemyFactory _enemyFactory;
    private EnemyModel _enemyModel;
    private EnemyController _enemyController;
    private PlayerView _playerView;
    
    public FactoryController(DiContainer container, Factory factory,PlayerFactory playerFactory,EnemyFactory enemyFactory)
    {
        _container  = container;
        _factory = factory;
        _playerFactory = playerFactory;
        _enemyFactory = enemyFactory;
    }

    

    public void Initialize()
    {
        _playerFactory.CreatePlayer(_factory.HeroSpawn);
        _enemyFactory.CreateEnemy(EnemyTypes.BigSlime, _factory.EnemySpawn[0]);
        _enemyFactory.CreateEnemy(EnemyTypes.BigSlime, _factory.EnemySpawn[1]);
    }

    public void Tick()
    {
        
    }
}
