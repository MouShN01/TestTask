using UnityEngine;

public class Arrow : Projectile
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = 15f;
    }
    
    private void Update()
    {
        if (hasHit) return;
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
}
