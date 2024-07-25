using UnityEngine;

public class HeroEuler : MonoBehaviour
{
    [SerializeField] private Transform boneToTurn; //class will rotate this bone
    [SerializeField] private Transform launchPoint; //class will take this point in account in calculations
    [SerializeField] private float rotationSpeed = 5;
    private bool _isRotating;

    public bool IsRotating => _isRotating;

    //method check if launch point look in right direction
    private void IsRotationEnded(Quaternion scopeRotation)
    {
        _isRotating = !(Quaternion.Angle(boneToTurn.rotation, scopeRotation) < 0.1);
    }
    
    public void GraduallyTurnTo(Quaternion targetRotation)
    {
        _isRotating = true;
        boneToTurn.rotation = Quaternion.RotateTowards(boneToTurn.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        IsRotationEnded(targetRotation);
    }
    
    //method transfer angle into Quaternion and taking in account x rotation
    public Quaternion TargetRotation(Transform targetToLookAt, float angle)
    {
        Vector3 direction = targetToLookAt.position - launchPoint.position;
        
        if (direction.x < 0)
        {
            return Quaternion.Euler(180, 0, angle);
        }
        else
        {
            return Quaternion.Euler(0, 0, angle);
        }
    }
    
    public void Flip()
    {
        transform.rotation = transform.rotation.x == 0 ? Quaternion.Euler(180, 0, 180) : Quaternion.Euler(0, 0, 0);
    }
}
