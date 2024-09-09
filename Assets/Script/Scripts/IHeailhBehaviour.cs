

public interface IHeailhBehaviour 
{
    public int AddHealth(int healthToAdd,int currentHealth,int maxHealth)
    {
        if (healthToAdd < 0) return 0;

        currentHealth += healthToAdd;
        if (currentHealth > maxHealth)
        {  
            currentHealth = maxHealth; 
        }
        
        return currentHealth;
    }

    public int RemoveHealth(int damage,int currentHealth, int maxHealth)
    {
        if (damage <= 0) return 0;
        
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        
        return currentHealth;
    }
}
