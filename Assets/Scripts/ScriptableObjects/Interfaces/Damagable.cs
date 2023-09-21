interface Damagable {
    double maxHealth { get; }
    double health { get; set; }

    /** Adds health to the player, stopping values above max health from occuring. */
    public virtual void addHealth(double value)
    {
        double newValue = health + value;
        
        if (newValue > maxHealth)
        {
            newValue = maxHealth;
        }

        health = newValue;
    }

    void kill();
}
