using UnityEngine;

public class SledAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _ShowCasePosition;
    [SerializeField] private float _Speed;
    private void Start()
    {
        _ShowCasePosition = transform.parent.parent.Find("SledShowPos").transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _ShowCasePosition, _Speed);
    }
    
}
