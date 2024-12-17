using UnityEngine;
using Zenject;


namespace Enemy_Factory
{
    public class Factory: MonoBehaviour
    {
        [field: SerializeField] public Transform[] SpawnEnemyPosition { get; private set; }
        DiContainer container;

        public Transform spawn;
        private PlayerView player;
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            container = diContainer;
        }
        private void Start()
        {
            player = container.Resolve<PlayerView>();
            player.transform.position = spawn.transform.position;

        }

        
    }
}
