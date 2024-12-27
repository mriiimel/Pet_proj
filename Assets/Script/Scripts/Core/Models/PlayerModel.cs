using UnityEngine;

public class PlayerModel
{
    private readonly int _playerMaxHealth;
    private readonly int _playerDamage;
    private float _playerCritChance;
    private float _playerSpeed;
    private readonly float _playerRotationSpeed;
    private float _attackSpeed;
   

    
    private readonly ScriptableObjectService _scriptableObjectService;
    private Camera _cameraView;


    public int PlayerMaxHealth => _playerMaxHealth;

    public int PlayerDamage => _playerDamage;

    public float PlayerCritChance { get => _playerCritChance; set => _playerCritChance = value; }
    public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value; }

    public float PlayerRotationSpeed => _playerRotationSpeed;

    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }

    

    public ScriptableObjectService ScriptableObjectService => _scriptableObjectService;

    public Camera CameraView { get => _cameraView; set => _cameraView = value; }
    

    public PlayerModel(ScriptableObjectService scriptableObjectService,Camera cameraView)
    {
        
        _scriptableObjectService = scriptableObjectService;
        _playerMaxHealth = _scriptableObjectService.PlayerConfig.GetHeroValue().MaxHealth;
        _playerDamage = _scriptableObjectService.PlayerConfig.GetHeroValue().Damage;
        _playerCritChance = _scriptableObjectService.PlayerConfig.GetHeroValue().CritChance;
        _playerSpeed = _scriptableObjectService.PlayerConfig.GetHeroValue().Speed;
        _playerRotationSpeed = _scriptableObjectService.PlayerConfig.GetHeroValue().RotationSpeed;
        _attackSpeed = _scriptableObjectService.PlayerConfig.GetHeroValue().AttackSpeed;
        _cameraView = cameraView;

    }
    
}
