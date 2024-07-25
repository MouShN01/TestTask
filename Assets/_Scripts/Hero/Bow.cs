using UnityEngine;

[RequireComponent(typeof(ProjectilePool))]
public class Bow : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float launchForce = 20.0f;
    [SerializeField] private ProjectilePathVisualizer projectilePathVisualizer;
    [SerializeField] private ProjectilePool projectilePool;
    private bool _isModifier;
    private float _fireModification = 0.2f;
    private bool _hasFired = false;
    
    private enum ProjectileType { Arrow, Bolt }
    private ProjectileType _selectedProjectile = ProjectileType.Arrow;
    
    public ProjectilePathVisualizer ProjectilePathVisualizer => projectilePathVisualizer;
    
    public Transform LaunchPoint => launchPoint;
    
    public float LaunchForce => launchForce;

    private void Start()
    {
        projectilePool = GetComponent<ProjectilePool>();
    }

    public void Fire()
    {
        if (_hasFired)
        {
            return;
        }

        _hasFired = true;

        var projectile = GetProjectileByType(_selectedProjectile);
        //this switch case needed because of object pool. If I just modified projectile when bow had modifier projectile would still have modifier and it would increase even more
        switch (_isModifier) // look if bow has modifier
        {
            case true when !projectile.IsModified:
                projectile.IncreaseDamageByModifier(_fireModification); // if projectile not modified it become this
                break;
            case false when projectile.IsModified:
                projectile.DecreaseDamageByModifier(_fireModification); // if projectile already modified and bow dont, it reduce its damage 
                break;
        }
        projectile.rb.isKinematic = false;
        projectile.transform.rotation = launchPoint.rotation;
        projectile.transform.position = launchPoint.position;
        projectile.rb.velocity = launchPoint.right * launchForce;
    }

    //method needed to prevent hero from shooting multiple times while in attack state
    public void ResetFire()
    {
        _hasFired = false;
    }

    public void SelectProjectileType(string type)
    {
        if (type.ToLower() == "arrow")
        {
            _selectedProjectile = ProjectileType.Arrow;
        }

        if (type.ToLower() == "bolt")
        {
            _selectedProjectile = ProjectileType.Bolt;
        }
    }

    private Projectile GetProjectileByType(ProjectileType type)
    {
        switch (type)
        {
            case ProjectileType.Arrow:
                return projectilePool.ArrowPool.GetFreeElement();
            case ProjectileType.Bolt:
                return projectilePool.BoltPool.GetFreeElement();
            default:
                return projectilePool.ArrowPool.GetFreeElement();
        }
    }

    public void ChangeModifierStatus(bool status)
    {
        _isModifier = status;
    }
}