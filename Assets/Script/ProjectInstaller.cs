using Zenject;
using UnityEngine;




public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ScriptableObjectService _scriptableObjectService;
    
    
    



    public override void InstallBindings()
    {
        ConfigsBindings();
        PlayerStateBindings();
        
    }

    private void PlayerStateBindings()
    {
        Container.Bind<PlayerStates>().AsSingle().WithArguments(_scriptableObjectService);
    }

    private void ConfigsBindings()
    {
        Container.Bind<ScriptableObjectService>().FromInstance(_scriptableObjectService).AsSingle();
    }
   
}
