using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GamePhase { Runner, WallPaint };

    public event Action OnGameCompleted = delegate { };

    public static GameManager Instance { get; private set; }


    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera wallCam;

    [SerializeField] Transform player;
    [SerializeField] Transform wallTransform;

    bool isLevelCompleted = false;
    GamePhase currentPhase;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        currentPhase = GamePhase.Runner;
    }

    public void CompleteLevel()
    {
        switch (currentPhase)
        {
            case GamePhase.Runner:
                OnGameCompleted();

                currentPhase = GamePhase.WallPaint;
                wallTransform.gameObject.SetActive(true);

                playerCam.enabled = false;

                StartCoroutine(DisableAllComponents(player));

                break;

            case GamePhase.WallPaint:
                break;

        }
    }

    IEnumerator DisableAllComponents(Transform tranformToDisable)
    {
        yield return new WaitForSeconds(2f);

        foreach (MonoBehaviour monoBehaviour in tranformToDisable.GetComponents<MonoBehaviour>())
        {
            monoBehaviour.enabled = false;
        }
    }
}
