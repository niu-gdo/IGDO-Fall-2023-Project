using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float despawnTimer; //When to despawn the projectiles after being fired.
    
    void Awake () //Set the despawn timer.
    {
        despawnTimer = Time.time + 3;
    }

    void Update() //Destroy the projectiles after the despawn timer runs out.
    {
        if(Time.time > despawnTimer)
        {
            Destroy(gameObject);
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision) //Destroy the object on collision.
    {
        Destroy(gameObject);
    }
}
