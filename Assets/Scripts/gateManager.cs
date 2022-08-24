using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateManager : MonoBehaviour
{
    public int increaseAmount;
    [SerializeField] private GameObject _gates;
    [SerializeField] private TMPro.TMP_Text _count;
    [SerializeField] private Material _positive;
    [SerializeField] private Material _negative;

    private void Start()
    {
        increaseAmount = Random.Range(-10, 10);

        if(_gates.transform.position.z == 30)
        {
            increaseAmount = Mathf.Abs(increaseAmount);
        }

        if (increaseAmount == 0)
        {
            increaseAmount = 1;
        }
        else if (increaseAmount < 0)
        {
            _gates.GetComponent<Renderer>().material.color = _negative.color;
        }
        else
        {
            _gates.GetComponent<Renderer>().material.color = _positive.color;
        }

        _count.text = increaseAmount.ToString();
    }
}
