using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControl : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private GameObject clonePlayer;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private TMPro.TMP_Text _countInfo;
    private Vector3 _forwardMoveAmount;
    private inputTouch _inputTouch;
    private Animator _animator;
    private bool _isEndGame;
    private bool _isStart;
    private float _xPos;
    private int _playerCount = 1;

    private List<GameObject> players = new List<GameObject>();

    private void OnEnable()
    {
        gameManager.onLevelStart += startGame;
        _animator = GetComponentInChildren<Animator>();
        _inputTouch = GetComponent<inputTouch>();
        _countInfo.text = _playerCount.ToString();
    }

    private void OnDisable()
    {
        gameManager.onLevelStart -= startGame;
    }

    private void FixedUpdate()
    {
        if (!_isStart)
        {
            return;
        }

        if (_isEndGame)
        {
            return;
        }

        movePlayer();

        if (_playerCount <= 0)
        {
            failedGame();
        }
    }

    private void startGame()
    {
        _animator.SetTrigger("Run");
        _isStart = true;
    }

    private void failedGame()
    {
        _isEndGame = true;
        _countInfo.enabled = false;
        _animator.SetTrigger("Dying");
        DOVirtual.DelayedCall(4f, () => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        for (int i = 0; i < _playerCount; i++)
        {
            GameObject dummy = players[i];
            dummy.GetComponentInChildren<Animator>().SetTrigger("Dying");
        }
    }

    private void finishGame()
    {
        _isEndGame = true;
        gameManager.onLevelCompleted?.Invoke();
        _countInfo.enabled = false;
        _animator.SetTrigger("Idle");
        DOVirtual.DelayedCall(7f, () => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        for (int i = 0; i < _playerCount; i++)
        {
            GameObject dummy = players[i];
            dummy.GetComponentInChildren<Animator>().SetTrigger("Idle");
        }
    }

    private void movePlayer()
    {
        _forwardMoveAmount = Vector3.forward * _forwardSpeed;
        Vector3 targetPosition = _rigidBody.transform.position + _forwardMoveAmount;

        if (Input.GetMouseButton(0))
        {
            targetPosition.x = 0;
            targetPosition.x = _inputTouch.DragAmountX * _slideSpeed;
        }

        //Thanks to the Mathf.Clamp function, we determine the max and min points that our character can go on the X axis.
        _xPos = Mathf.Clamp(targetPosition.x, -3.5f, 3.5f);

        //Lerp is a function that allows us to go from one point to another on a linear scale at a given time.
        Vector3 targetPositionLerp = new Vector3(Mathf.Lerp(_rigidBody.position.x, _xPos, Time.fixedDeltaTime * _lerpSpeed),
        Mathf.Lerp(_rigidBody.position.y, targetPosition.y, Time.fixedDeltaTime * _lerpSpeed),
        Mathf.Lerp(_rigidBody.position.z, targetPosition.z, Time.fixedDeltaTime * _lerpSpeed));

        _rigidBody.MovePosition(targetPositionLerp);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndLine")
        {
            finishGame();
        }

        if (other.tag == "Car")
        {
            failedGame();
        }

        if (other.tag == "Gate")
        {
            if (other.GetComponent<gateManager>().increaseAmount >= 0)
            {
                for (int i = 0; i < other.GetComponent<gateManager>().increaseAmount; i++)
                {
                    _playerCount++;
                    GameObject tempClone = Instantiate(clonePlayer, transform);
                    tempClone.transform.localPosition = new Vector3(Random.Range(-2, 2), 0, Random.Range(0,-3));
                    players.Add(tempClone);
                    tempClone.GetComponentInChildren<Animator>().SetTrigger("Run");
                }
            }

            if (other.GetComponent<gateManager>().increaseAmount < 0)
            {
                for (int i = 0; i < Mathf.Abs(other.GetComponent<gateManager>().increaseAmount); i++)
                {
                    _playerCount--;
                    GameObject dummy = players[0];
                    dummy.GetComponentInChildren<Animator>().SetTrigger("Dying");
                    players.RemoveAt(0);
                    dummy.transform.parent = null;
                    DOVirtual.DelayedCall(3f, () => Destroy(dummy));
                }
            }

            _countInfo.text = _playerCount.ToString();
        }

        // Triangle pattern code
        // ----------------------
        // for (int x = 0; x < other.GetComponent<gateManager>().increaseAmount; x++)
        // {
        //     for (int y = x; y < 2 * (other.GetComponent<gateManager>().increaseAmount - x) - 1; y++)
        //     {
        //         _playerCount++;
        //         GameObject tempClone = Instantiate(clonePlayer, transform);
        //         tempClone.transform.localPosition = new Vector3(x, 0 ,y);
        //         players.Add(tempClone);
        //         tempClone.GetComponentInChildren<Animator>().SetTrigger("Run");
        //     }
        // }
    }
}
