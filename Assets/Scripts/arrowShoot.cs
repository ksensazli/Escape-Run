using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class arrowShoot : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;
    [SerializeField] private Rigidbody _arrowRigidbody;
    [SerializeField] private GameObject _target;
    [SerializeField] private float throwForceInZ = 50f;
    [SerializeField] private float throwForceInXAndY = 1f;
    private Vector3 m_ballInitialPosition;
    private Vector2 m_startPos, m_endPos, m_direction;
    private float m_touchTimeStart, m_touchTimeFinish, m_timeInterval;
    private bool _isShooting;
    private void OnEnable()
    {
        gameManager.onLevelCompleted += shootArrow;
    }

    private void OnDisable()
    {
        gameManager.onLevelCompleted -= shootArrow;
    }

    private void Update()
    {
        if (!_isShooting)
        {
            return;
        }

        if (_arrow.transform.localPosition.z >= 209.95f)
        {
            gameManager.onEndLevel?.Invoke();
            _isShooting = false;
            _arrowRigidbody.velocity = Vector3.zero;
        }

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
            m_direction.x = Mathf.Clamp(m_direction.x, -0.65f, 0.65f);
            _arrowRigidbody.AddForce(-m_direction.x * throwForceInXAndY, -m_direction.y * throwForceInXAndY, throwForceInZ / m_timeInterval);
        }
    }

    private void shootArrow()
    {
        DOVirtual.DelayedCall(1f, () => _arrow.SetActive(true));
        DOVirtual.DelayedCall(1f, () => _target.SetActive(true));
        _isShooting = true;
    }
}
