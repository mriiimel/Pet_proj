public class EnemyModel
{
    private readonly ScriptableObjectService _scriptableObjectService;
    private bool damageIsApply = false;
    private bool _isDead = false;
    public EnemyModel(ScriptableObjectService scriptableObjectService)
    {
        _scriptableObjectService = scriptableObjectService;
    }

    public ScriptableObjectService ScriptableObjectService => _scriptableObjectService;

    public bool DamageIsApply { get => damageIsApply; set => damageIsApply = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }
}

