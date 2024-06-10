using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance;

    [SerializeField]
    private List<EnemyController> enemies;

    private void Awake()
    {
        if (Instance != null || Instance == this)
            return;

        if (Instance == null)
            Instance = this;
    }

    public void InitializeEnemies()
    {
        foreach (Transform enemy in transform)
        {
            EnemyController enemyController;

            if(enemy.TryGetComponent(out enemyController))
            {
                enemies.Add(enemyController);
                enemyController.InitializeAgent();
            }
        }
    }
}
