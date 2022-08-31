using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private RectTransform _image;
    [SerializeField] private TMPro.TMP_Text _gameOverText;
    [SerializeField] private TMPro.TMP_Text _levelCount;

    void OnEnable()
    {
        gameManager.onLevelStart += startScreen;
        gameManager.onEndLevel += endScreen;
        _image.DOAnchorPosX(-350, 1.5f).SetLoops(-1, LoopType.Yoyo);
        _levelCount.text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }

    void OnDisable()
    {
        gameManager.onLevelStart -= startScreen;
        gameManager.onEndLevel -= endScreen;
    }

    private void startScreen()
    {
        DOVirtual.DelayedCall(.25f, () => _startScreen.SetActive(false));
    }

    private void endScreen()
    {
        _endScreen.SetActive(true);
    }
}
