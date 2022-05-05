using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public GameObject EnemyPrefab;

    public Player CurrentPlayer;
    public GameObject CurrentPortal;

    private void OnEnable()
    {
        Instance = this;
    }

    public void SpawnEnemy()
    {
        var enemy = Instantiate(EnemyPrefab, Room.Instance.transform);
        enemy.transform.position = CurrentPortal.transform.position;
    }
}
