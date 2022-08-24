using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectControl : MonoBehaviour
{
    [SerializeField] private Rigidbody _planeRigidBody;
    [SerializeField] private Vector3 _planeSpeed;
    [SerializeField] private Rigidbody _carRigidBody;
    [SerializeField] private Vector3 _carSpeed;

    private void FixedUpdate() 
    {
        _planeRigidBody.velocity = _planeSpeed;
        _carRigidBody.velocity = _carSpeed;
    }
}
