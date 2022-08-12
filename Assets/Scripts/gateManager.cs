using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateManager : MonoBehaviour
{
    public int increaseAmount;
    [SerializeField] private TMPro.TMP_Text _count;

    private void Start() 
    {
        _count.text = increaseAmount.ToString();
    }
}
