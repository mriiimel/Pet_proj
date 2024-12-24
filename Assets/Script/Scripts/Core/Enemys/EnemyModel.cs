public class EnemyModel
{
    private readonly ScriptableObjectService _scriptableObjectService;
    

    public EnemyModel(ScriptableObjectService scriptableObjectService)
    {
        _scriptableObjectService = scriptableObjectService;
    }

    public ScriptableObjectService ScriptableObjectService => _scriptableObjectService;
}

