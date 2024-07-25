using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float damage;
    public bool hasHit;
    public Rigidbody2D rb;
    private bool _isModified = false;

    public bool IsModified => _isModified;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Target"))
        {
            Target target = other.gameObject.GetComponent<Target>();
            hasHit = true;
            transform.SetParent(other.transform);
            target.AddProjectileToList(this);
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            target.TakeDamage(damage);
            
        }
    }
    
    public void IncreaseDamageByModifier(float modifier)
    {
        damage *= 1 + modifier;
        _isModified = true;
    }
    public void DecreaseDamageByModifier(float modifier)
    {
        damage *= 1 / (1+ modifier);
        _isModified = false;
    }
}
