using System;
using UnityEngine;

public class PlayerInformation : MonoBehaviour, IDamagable
{
    /**
     * The full capacity of the shield
     */
    public float ShieldCapacity = 100F;

    /**
     * The remaining health of the shield
     */
    public float ShieldHealth = 100F;

    /**
     * Controls how much incoming damage ill be absorbed by the shield
     */
    public float DamageResistance = 0.25F;

    public float MaxHealth { get; }
    public float Health { get; set; }

    public void HealDamage(float value)
    {
        Health = Mathf.Clamp(Health + value, 0f, MaxHealth);
    }

    public void TakeDamage(float value)
    {
        // Calculate the value that would be absorbed
        var absorbed = value * DamageResistance;

        // If its trying to absorb more than it has health, set absorbed to its health    
        if (absorbed > ShieldHealth) absorbed = ShieldHealth;

        // Remove the amount it absorbed
        DamageShieldHealth(absorbed);
        // Remove the initial damage, with the absorbed amount removed. 
        var newValue = Health - value;

        if (newValue <= 0)
        {
            Health = 0;
            Die();
            return;
        }

        Health = newValue;
    }

    /**
     * Logic for when the player is killed. Called when health reaches zero.
     */
    public void Die()
    {
        throw new NotImplementedException("Need logic for player death");
    }

    /**
     * Adds health to the shield
     */
    public void HealShieldHealth(float value)
    {
        ShieldHealth = Mathf.Clamp(ShieldHealth + value, 0f, ShieldCapacity);
    }

    /**
     * removes health
     */
    public void DamageShieldHealth(float value)
    {
        var newValue = ShieldHealth - value;

        if (newValue <= 0)
        {
            shieldsEmptied();
            newValue = 0;
        }

        ShieldHealth = newValue;
    }

    /**
     * Logic for when the player shields have been emptied.
     */
    public void shieldsEmptied()
    {
        throw new NotImplementedException("Need logic for when the shields are empty");
    }
}