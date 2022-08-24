using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateManager : MonoBehaviour
{
    public int increaseAmount;
    [SerializeField] private TMPro.TMP_Text _count;

    private void Start() 
    {
        increaseAmount = Random.Range(-10 ,10);
        if (increaseAmount == 0)
        {
            increaseAmount = 1;
        }
        _count.text = increaseAmount.ToString();
    }
}
