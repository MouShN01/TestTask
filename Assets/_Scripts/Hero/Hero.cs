using UnityEngine;

[RequireComponent(typeof(HeroEuler))]
public class Hero : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private HeroEuler heroEuler;
    [SerializeField] private Bow bow;
    private StateMachine _stateMachine;
    private Animator _heroAnimator;
    private readonly float _detectRange = 5.0f;
    
    public StateMachine HeroStateMachine => _stateMachine;
    public HeroEuler HeroEuler => heroEuler;

    private void Awake()
    {
        _heroAnimator = GetComponent<Animator>();
        _stateMachine = new StateMachine(this);
    }

    private void Start()
    {
        _stateMachine.Initialize(_stateMachine.idleState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }
    
    //method to invoke animations
    public void SetAnimationTrigger(string trigger)
    {
        _heroAnimator.SetTrigger(trigger);
    }
    
    //method to start rotation to target and calculate projectile flight path
    public void ScopeTarget()
    {
        bow.ProjectilePathVisualizer.PointPosition(AngleToHitTarget(), bow.LaunchForce);
        heroEuler.GraduallyTurnTo(heroEuler.TargetRotation(target.transform, AngleToHitTarget()));
    }
    
    //method to choose right side and show projectile flight path
    public void PrepareForAttack()
    {
        Vector3 direction = target.transform.position - transform.position;
        if (direction.x < 0)
        {
            heroEuler.Flip();
        }
        bow.ProjectilePathVisualizer.ActivatePathVisualizer();
    }
    
    //method to check if pulling a bowstring animation ended to make a shot
    public void Attack()
    {
        AnimatorStateInfo stateInfo = _heroAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Shot") && stateInfo.normalizedTime >= 1.0f)
        {
            bow.Fire();
        }
    }
    
    //method to hide pathway while not scoping and to reset attacking script
    public void Reload()
    {
        bow.ProjectilePathVisualizer.DisablePathVisualizer();
        bow.ResetFire();
    }

    //method detect if hero turned in other direction from the original position and turns him to it
    public void BackToInitialPosition()
    {
        Quaternion initialPose = Quaternion.Euler(0, 0, 0);
        if (transform.rotation.x == 0)
        {
            heroEuler.GraduallyTurnTo(initialPose);
        }
        else
        {
            heroEuler.Flip();
            heroEuler.GraduallyTurnTo(initialPose);
        }
        
    }
    
    //method responsible for detection of target in selected readonly range
    public bool IsTargetActiveAndInRange()
    {
        if (!target.activeInHierarchy) return false;
        return Vector3.Distance(transform.position, target.transform.position) <= _detectRange;
    }
    
    //method which calculate right angle to hit target 
    private float AngleToHitTarget()
    {
        Vector2 direction = target.transform.position - bow.LaunchPoint.position;
        float distance = direction.magnitude;
        float gravity = Mathf.Abs(Physics2D.gravity.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // this angle that makes hero look straight at target 
        float additionalAngle = (Mathf.Asin((gravity * distance) / Mathf.Pow(bow.LaunchForce,2)) / 2) * Mathf.Rad2Deg; // this is additional angle to previous which take in account gravity and launch force
        if (direction.x < 0)
        {
            angle *= -1;
        }
        return angle + additionalAngle; //result angle
    }
}
