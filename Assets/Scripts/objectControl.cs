using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectControl : MonoBehaviour
{
    [SerializeField] private Rigidbody _planeRigidBody;
    [SerializeField] private Vector3 _planeSpeed;
    //[SerializeField] private Rigidbody _carRigidBody;

    private void Update() 
    {
        _planeRigidBody.velocity = _planeSpeed;
        //_carRigidBody.velocity = new Vector3(0,0,-5f);
    }
}
