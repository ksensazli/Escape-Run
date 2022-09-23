using DG.Tweening;
using UnityEngine;

public class arrowShoot : MonoBehaviour
{
    [SerializeField] private GameObject _arrow, _target;
    [SerializeField] private Rigidbody _arrowRigidbody;
    [SerializeField] private ParticleSystem _confetties;
    [SerializeField] private float throwForceInZ = 50f, throwForceInXAndY = 1f;
    [SerializeField] private TMPro.TMP_Text _gameScoreText;
    private Vector2 m_startPos, m_endPos, m_direction;
    private float m_touchTimeStart, m_touchTimeFinish, m_timeInterval;
    private bool _isShooting;
    private int _point;
    private GameObject _playerScript;
    private int _gameScore;
    private int _generalScore;

    private void OnEnable()
    {
        gameManager.onLevelCompleted += showArrow;
        gameManager.onEndLevel += playConfetties;
        _playerScript = GameObject.Find("Player");
        _generalScore = PlayerPrefs.GetInt("GameScore");
        _gameScoreText.text = _generalScore.ToString();
    }

    private void OnDisable()
    {
        gameManager.onLevelCompleted -= showArrow;
        gameManager.onEndLevel -= playConfetties;
    }

    private void Update()
    {
        if (!_isShooting)
        {
            return;
        }

        if (_arrow.transform.localPosition.z >= 209.5f)
        {
            DOVirtual.DelayedCall(1f, () => gameManager.onEndLevel?.Invoke());
            _isShooting = false;
            _arrowRigidbody.velocity = Vector3.zero;

            controlXPos();
            calculateScore();
        }

        shootArrow();

    }

    private void showArrow()
    {
        DOVirtual.DelayedCall(1f, () => _arrow.SetActive(true));
        DOVirtual.DelayedCall(1f, () => _target.SetActive(true));
        _isShooting = true;
    }

    private void controlXPos()
    {
        if (Mathf.Abs(_arrow.transform.localPosition.x) >= 0.058f && Mathf.Abs(_arrow.transform.localPosition.x) <= 0.114f)
        {
            _point = 9;
        }
        else if (Mathf.Abs(_arrow.transform.localPosition.x) >= 0.115f && Mathf.Abs(_arrow.transform.localPosition.x) <= 0.168f)
        {
            _point = 8;
        }
        else if (Mathf.Abs(_arrow.transform.localPosition.x) >= 0.169f && Mathf.Abs(_arrow.transform.localPosition.x) <= 0.225f)
        {
            _point = 7;
        }
        else if (Mathf.Abs(_arrow.transform.localPosition.x) >= 0.226f && Mathf.Abs(_arrow.transform.localPosition.x) <= 0.281f)
        {
            _point = 6;
        }
        else if (Mathf.Abs(_arrow.transform.localPosition.x) >= 0.282f && Mathf.Abs(_arrow.transform.localPosition.x) <= 0.337f)
        {
            _point = 5;
        }
        else if (Mathf.Abs(_arrow.transform.localPosition.x) >= 0.338f && Mathf.Abs(_arrow.transform.localPosition.x) <= 0.393f)
        {
            _point = 4;
        }
        else if (Mathf.Abs(_arrow.transform.localPosition.x) >= 0.394f && Mathf.Abs(_arrow.transform.localPosition.x) <= 0.450f)
        {
            _point = 3;
        }
        else if (Mathf.Abs(_arrow.transform.localPosition.x) >= 0.451 && Mathf.Abs(_arrow.transform.localPosition.x) <= 0.507f)
        {
            _point = 2;
        }
        else if (Mathf.Abs(_arrow.transform.localPosition.x) >= 0.508f && Mathf.Abs(_arrow.transform.localPosition.x) <= 0.564f)
        {
            _point = 1;
        }
        else if (Mathf.Abs(_arrow.transform.localPosition.x) >= 0.565f)
        {
            _point = 0;
        }
        else
        {
            _point = 10;
        }
    }

    private void calculateScore()
    {
        _gameScore = _playerScript.GetComponent<playerControl>().playerCount * _point;
        _generalScore = _generalScore + _gameScore;
        _gameScoreText.text = _generalScore.ToString();
        PlayerPrefs.SetInt("GameScore", _generalScore);
    }

    private void shootArrow()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_touchTimeStart = Time.time;
            m_startPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_touchTimeFinish = Time.time;
            m_timeInterval = m_touchTimeFinish - m_touchTimeStart;
            m_endPos = Input.mousePosition;
            m_direction = m_startPos - m_endPos;
            m_direction.x = Mathf.Clamp(m_direction.x, -1f, 1f);
            _arrowRigidbody.AddForce(-m_direction.x * throwForceInXAndY, -m_direction.y * throwForceInXAndY, throwForceInZ / m_timeInterval);
        }
    }

    private void playConfetties()
    {
        _confetties.Play();
    }
}
