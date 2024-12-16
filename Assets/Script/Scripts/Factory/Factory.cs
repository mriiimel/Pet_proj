using UnityEngine;
using Zenject;


namespace Enemy_Factory
{
    public class Factory: MonoBehaviour
    {
        [field: SerializeField] public Transform[] SpawnEnemyPosition { get; private set; }
        [Inject]DiContainer container;

        public Transform spawn;
        private PlayerView player;
        private void Start()
        {
            player = container.Resolve<PlayerView>();
            player.transform.position = spawn.transform.position;

        }

        
    }
}
