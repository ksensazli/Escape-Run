using System;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static Action onLevelStart;
    public static Action onEndReached;
    public static Action onLevelCompleted;
    [SerializeField] private Rigidbody _rigidBody;
    private bool _isStart;
    private bool _isEnd;

    private void startLevel()
    {
        onLevelStart?.Invoke();
        _isStart = true;
    }

    private void endLevel()
    {
        onLevelCompleted?.Invoke();
        _isEnd = true;
    }

    private void FixedUpdate() 
    {
        if (!_isStart)
        {
            if(Input.GetMouseButtonDown(0))
            {
                startLevel();
            }
            return;
        }

        if (!_isEnd)
        {
            if(_rigidBody.position == new Vector3(_rigidBody.position.x, _rigidBody.position.y, 119.7993f))
            {
                endLevel();
            }
            return;
        }
    }
}
