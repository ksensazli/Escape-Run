using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectControl : MonoBehaviour
{
    [SerializeField] private Rigidbody _plane;
    [SerializeField] private Vector3 _planeSpeed;
    [SerializeField] private Rigidbody _car;
    [SerializeField] private Vector3 _carSpeed;

    private void FixedUpdate()
    {
        _car.velocity = _carSpeed;
        _plane.velocity = _planeSpeed;
    }
}
