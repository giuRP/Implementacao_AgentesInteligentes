using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Agent playerAgent;

    private void Awake()
    {
        if (Instance != null || Instance == this)
            return;

        if (Instance == null)
            Instance = this;

        playerAgent = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Agent>();
    }

    private void Start()
    {
        playerAgent.InitializeAgent();
        EnemiesManager.Instance.InitializeEnemies();
    }
}
