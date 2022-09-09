using DG.Tweening;
using UnityEngine;

public class arrowShoot : MonoBehaviour
{
    [SerializeField] private GameObject _arrow, _target;
    [SerializeField] private Rigidbody _arrowRigidbody;
    [SerializeField] private ParticleSystem _confetties;
    [SerializeField] private float throwForceInZ = 50f, throwForceInXAndY = 1f;
    private Vector2 m_startPos, m_endPos, m_direction;
    private float m_touchTimeStart, m_touchTimeFinish, m_timeInterval;
    private bool _isShooting;

    private void OnEnable()
    {
        gameManager.onLevelCompleted += showArrow;
        gameManager.onEndLevel += playConfetties;
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
        }

        shootArrow();

    }

    private void showArrow()
    {
        DOVirtual.DelayedCall(1f, () => _arrow.SetActive(true));
        DOVirtual.DelayedCall(1f, () => _target.SetActive(true));
        _isShooting = true;
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
