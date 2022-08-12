using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera firstVirtualCam;
    [SerializeField] private CinemachineVirtualCamera secondVirtualCam;
    [SerializeField] private CinemachineVirtualCamera thirdVirtualCam;

    private void OnEnable()
    {
       gameManager.onLevelStart += startTheGame;
       gameManager.onLevelCompleted += endGame;
    }
    private void OnDisable() 
    {
        gameManager.onLevelStart -= startTheGame;
        gameManager.onLevelCompleted -= endGame;
    }
    
    private void startTheGame()
    {
        firstVirtualCam.enabled = false;
        secondVirtualCam.enabled = true; 
        thirdVirtualCam.enabled = false;
    }

    private void endGame()
    {
        firstVirtualCam.enabled = false;
        secondVirtualCam.enabled = false;
        thirdVirtualCam.enabled = true;
    }
}
