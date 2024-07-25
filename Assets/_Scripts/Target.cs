using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    public List<Projectile> attachedProjectiles;
    public int maxAttachableCount = 5; //value of maximum shown projectiles attached to target
    public AudioSource audioSource;
    
    private void Start()
    {
        attachedProjectiles = new List<Projectile>();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void TakeDamage(float damage)
    {
        Debug.Log($"Damage taken: {damage}");
        Disappear();
    }

    //method calls when button clicked and set random position for target and show it
    public void Appear()
    {
        audioSource.Play();
        ClearSurface();
        float rX = Random.Range(-7, 7);
        float rY = Random.Range(-5f, 5f);
        Vector2 newPos = new Vector2(rX, rY);
        transform.position = newPos;
        transform.gameObject.SetActive(true);
    }

    private void Disappear()
    {
        transform.gameObject.SetActive(false);
    }

    //method add new projectiles which collided with it (calls in OnCollisionEnter of projectile)  
    public void AddProjectileToList(Projectile projectile)
    {
        if(!attachedProjectiles.Contains(projectile))// because of object pool, need to check if projectiles already belongs to target
            attachedProjectiles.Add(projectile);
    }

    //method needed to clear surface of target so it isnt be filled with infinite number of arrows. It hides them to object pool can reuse it
    private void ClearSurface()
    {
        if (attachedProjectiles.Count == maxAttachableCount)
        {
            DeactivateProjectile<Projectile>();
        }
    }
    
    //if maxAttachableCount reached this method delete one of active projectiles to allow another projectile be attached or reuse existing
    bool DeactivateProjectile<T>() where T : Projectile
    {
        if (attachedProjectiles.OfType<T>().Any(p => !p.gameObject.activeSelf)) return false; // if no any active disabled objects, just reuse it
        var projectile = attachedProjectiles.OfType<T>().FirstOrDefault(p => p.gameObject.activeSelf); // if full delete one from list
        if (projectile != null)
        {
            projectile.gameObject.SetActive(false);
            attachedProjectiles.Remove(projectile);
            return true;
        }
        return false;
    }
}
