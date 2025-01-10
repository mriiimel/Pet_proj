

public class PlayerStates 
{
    public float Health { get; set; }

    public PlayerStates(ScriptableObjectService scriptableObjectService)
    {
        Health = scriptableObjectService.PlayerConfig.GetHeroValue().MaxHealth;
       
    }
}
