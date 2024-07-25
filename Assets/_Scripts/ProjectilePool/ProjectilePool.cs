using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    //class creates two object pools for different types of projectiles
    [SerializeField] private Projectile arrowPrefab;
    [SerializeField] private Projectile boltPrefab;
    [SerializeField] private int maxProjectilesPool = 5;
    [SerializeField] private bool isAutoExpand = true;
    
    public ObjPool<Projectile> ArrowPool;
    public ObjPool<Projectile> BoltPool;
    private void Start()
    {
        ArrowPool = new ObjPool<Projectile>(arrowPrefab, maxProjectilesPool, this.transform);
        ArrowPool.IsAutoExpand = isAutoExpand;
        
        BoltPool = new ObjPool<Projectile>(boltPrefab, maxProjectilesPool, this.transform);
        BoltPool.IsAutoExpand = isAutoExpand;
    }
}
