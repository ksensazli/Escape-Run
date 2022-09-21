using System;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static Action onLevelStart;
    public static Action onLevelCompleted;
    public static Action onEndLevel;
    [SerializeField] private GameObject _arrow;
    private bool _isStart;
    private bool _isComplete;
    private bool _isEnd;

    private void startLevel()
    {
        onLevelStart?.Invoke();
        _isStart = true;
    }

    private void completeLevel()
    {
        onLevelCompleted?.Invoke();
        _isComplete = true;
    }

    private void endLevel()
    {
        onEndLevel?.Invoke();
        _isEnd = true;
    }

    private void FixedUpdate() 
    {
        if (!_isStart && Input.GetMouseButtonDown(0))
        {
            startLevel();
            return;
        }

        if (!_isEnd)
        {
            return;
        }

        if(!_isComplete)
        {
            return;
        }
    }

    private void hitTarget()
    {
        if (_arrow.transform.localPosition.z == 209.5f)
        {
            endLevel();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "EndLine")
        {
            completeLevel();
        }
    }
}
