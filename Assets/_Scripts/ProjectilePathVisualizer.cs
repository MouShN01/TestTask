using UnityEngine;

public class ProjectilePathVisualizer : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject point;
    [SerializeField] private int numberOfPoints;
    [SerializeField] private float distanceBetweenFloats;
    private Vector2 _originalScale;
    private GameObject[] _points;
    
    void Start()
    {
        _originalScale = point.transform.localScale; //save original size of point to make other smaller
        _points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            _points[i] = Instantiate(point, startPoint.position, Quaternion.identity, transform);
        }
    }

    public void PointPosition(float angle, float launchForce) 
    {
        //invert angle is hero has x = 180 rotation
        if (Mathf.Abs(angle) > 90)
        {
            angle *= -1;
        }
        float angleToRad = angle * Mathf.Deg2Rad;
        Vector2 launchDirection = new Vector2(Mathf.Cos(angleToRad), Mathf.Sin(angleToRad)) * launchForce;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float time = i * distanceBetweenFloats;
            Vector2 position = (Vector2)startPoint.position + launchDirection * time +
                               0.5f * Physics2D.gravity * Mathf.Pow(time, 2);
            _points[i].transform.position = position;
            SetSize(_points[i], i);
        }
    }

    //method decrease pints first - the biggest, last - the smallest
    private void SetSize(GameObject go, int goNumber)
    {
        float reductionFraction = 0.1f;

        float scaleFactor = 1 - reductionFraction * goNumber;
        Vector2 newScale = _originalScale * scaleFactor;

        go.transform.localScale = newScale;
    }
    
    public void ActivatePathVisualizer()
    {
        transform.gameObject.SetActive(true);
    }

    public void DisablePathVisualizer()
    {
        transform.gameObject.SetActive(false);
    }
}
