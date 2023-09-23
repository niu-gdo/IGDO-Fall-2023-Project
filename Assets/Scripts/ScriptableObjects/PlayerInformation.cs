using System;
using UnityEngine;

namespace ScriptableObjects
{
    public class PlayerInformation : MonoBehaviour, Damagable
    {
        public PlayerInformation(double maxHealth)
        {
            this.maxHealth = maxHealth;
        }

        public double maxHealth { get; }

        public double health { get; set; }

        /** The full capacity of the shield */
        public double shieldCapacity;

        /** The remaining health of the shield */
        public double shieldHealth;

        /** Controls how much incoming damage ill be absorbed by the shield */
        private double damageResistance;

        public void removeHealth(double value)
        {
            // Calculate the value that would be absorbed
            double absorbed = value * damageResistance;
            
            // If its trying to absorb more than it has health, set absorbed to its health    
            if (absorbed > shieldHealth)
            { 
                absorbed = shieldHealth;
            }
            
            // Remove the amount it absorbed
            removeShieldHealth(absorbed);
            // Remove the initial damage, with the absorbed amount removed. 
            double newValue = health - value;
        
            if (newValue <= 0)
            {
                health = 0;
                kill();
                return;
            }

            health = newValue;
        }
        
        /** Adds health to the shield */
        public void addShieldHealth(double value)
        {
            double newValue = shieldHealth + value;
        
            if (newValue > shieldCapacity)
            {
                newValue = shieldCapacity;
            }

            shieldHealth = newValue;
        }
        
        /** removes health */
        public void removeShieldHealth(double value)
        {
            double newValue = shieldHealth - value;
        
            if (newValue <= 0)
            {
                shieldsEmptied();
                newValue = 0;
            }

            shieldHealth = newValue;
        }

        /** Logic for when the player is killed. Called when health reaches zero. */
        public void kill()
        {
            throw new NotImplementedException("Need logic for player death");
        }
        
        /** Logic for when the player shields have been emptied. */
        public void shieldsEmptied()
        {
            throw new NotImplementedException("Need logic for when the shields are empty");
        }
    }
}
