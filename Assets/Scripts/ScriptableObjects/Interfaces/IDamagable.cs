public interface IDamagable 
{
    /** Adds health to the player, stopping values above max health from occuring. */
    public void HealDamage(float value);

    public void TakeDamage(float value);

    void Die();
}
