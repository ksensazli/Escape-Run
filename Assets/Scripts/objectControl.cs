using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectControl : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> _plane;
    [SerializeField] private Vector3 _planeSpeed;
    [SerializeField] private List<Rigidbody> _car;
    [SerializeField] private Vector3 _carSpeed;

    private void FixedUpdate() 
    {
        for(int i = 0; i < 10; i++)
        {
            _car[i].velocity = _carSpeed;
            _plane[i].velocity = _planeSpeed;
        }
    }
}
