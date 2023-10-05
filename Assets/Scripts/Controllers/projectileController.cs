using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    // deactivate after delay
    [SerializeField] private float timeoutDelay = 3f;

    private IObjectPool<ProjectileController> objectPool;

    // public property to give the projectile a reference to its ObjectPool
    public IObjectPool<ProjectileController> ObjectPool { set => objectPool = value; }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    private void OnCollisionEnter2D(Collision2D collision) //Destroy the object on collision.
    {
        objectPool.Release(this);
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // reset the moving Rigidbody
        Rigidbody2D rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = new Vector2(0f, 0f);

        // release the projectile back to the pool
        objectPool.Release(this);
    }
}