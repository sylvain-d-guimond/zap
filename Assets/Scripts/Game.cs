using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public PortalGenerator Portals;

    public GameObject Enemy1Prefab;
    public GameObject Enemy2Prefab;
    public GameObject Enemy3Prefab;

    public GameStates StartState;
    public GameStates State {
        get => _state;
        set
        {
            if (value != _state)
            {
                SetState(value);
                _state = value;
            }
        }
    }
    public bool SuperCharged
    {
        get => _superCharged;
        set
        {
            if (_superCharged != value)
            {
                if (!value)
                {
                    foreach (var ball in _superChargedBalls)
                    {
                        ball.SetSupercharged(false);
                    }
                    _superChargedBalls.Clear();
                }
                _superCharged = value;
            }
        }
    }

    public Player CurrentPlayer;
    public GameObject CurrentPortal;

    private GameStates _state;
    private bool _superCharged;
    private List<LightningBallDamage> _superChargedBalls = new List<LightningBallDamage>();
    private IMixedRealityEyeGazeProvider _eyeGaze;

    private List<Coroutine> _stateActions = new List<Coroutine>();

    private void OnEnable()
    {
        Instance = this;
        State = StartState;
    }

    private void Start()
    {
        _eyeGaze = CoreServices.InputSystem?.EyeGazeProvider;
    }

    public void SetState(GameStates state)
    {
        switch (state)
        {
            case GameStates.Prestart:
                Clear();
                _stateActions.Add(StartCoroutine(CoDelay(5, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(10, () => SetState(GameStates.Level1))));
                break;
            case GameStates.Level1:
                Clear();
                _stateActions.Add(StartCoroutine(CoDelay(0, () => SpawnEnemy(Enemy1Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(5, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(10, () => SpawnEnemy(Enemy1Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(15, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(20, () => SpawnEnemy(Enemy1Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(25, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(30, () => SpawnEnemy(Enemy1Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(35, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(40, () => SpawnEnemy(Enemy1Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(45, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(50, () => SpawnEnemy(Enemy1Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(55, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(60, () => SpawnEnemy(Enemy2Prefab, 
                    () => _stateActions.Add(StartCoroutine(CoDelay(15, ()=> SetState(GameStates.Level2))))))));
                break;
            case GameStates.Level2:
                Clear();
                _stateActions.Add(StartCoroutine(CoDelay(0, () => SpawnEnemy(Enemy1Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(3, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(6, () => SpawnEnemy(Enemy2Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(9, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(12, () => SpawnEnemy(Enemy1Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(15, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(17, () => SpawnEnemy(Enemy2Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(19, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(20, () => SpawnEnemy(Enemy1Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(22, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(24, () => SpawnEnemy(Enemy2Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(26, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(28, () => SpawnEnemy(Enemy3Prefab,
                    () => _stateActions.Add(StartCoroutine(CoDelay(15, () => SetState(GameStates.Level3))))))));
                break;
            case GameStates.Level3:
                Clear();
                _stateActions.Add(StartCoroutine(CoDelay(0, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(5, () => SpawnEnemy(Enemy3Prefab,
                    () => _stateActions.Add(StartCoroutine(CoDelay(60, () => SetState(GameStates.Level4))))))));
                break;
            case GameStates.Level4:
                Clear();
                _stateActions.Add(StartCoroutine(CoDelay(0, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(5, () => SpawnEnemy(Enemy3Prefab,
                    () => _stateActions.Add(StartCoroutine(CoDelay(60, () => SetState(GameStates.Level5))))))));
                break;
            case GameStates.Level5:
                Clear();
                _stateActions.Add(StartCoroutine(CoDelay(0, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(1, () => SpawnEnemy(Enemy3Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(2, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(3, () => SpawnEnemy(Enemy3Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(4, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(5, () => SpawnEnemy(Enemy3Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(6, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(7, () => SpawnEnemy(Enemy3Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(8, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(9, () => SpawnEnemy(Enemy3Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(10, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(11, () => SpawnEnemy(Enemy3Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(12, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(13, () => SpawnEnemy(Enemy3Prefab))));
                _stateActions.Add(StartCoroutine(CoDelay(14, () => Portals.PlacePortal())));
                _stateActions.Add(StartCoroutine(CoDelay(15, () => SpawnEnemy(Enemy3Prefab))));
                Debug.Log("Woop woop");
                break;
        }
    }

    private void Clear()
    {
        foreach (var sa in _stateActions)
        {
            if (sa != null)
            {
                StopCoroutine(sa);
            }
        }

        _stateActions.Clear();
    }

    private IEnumerator CoDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);

        action();
    }

    public void SpawnEnemy(GameObject prefab, Action onDeath = null)
    {
        StartCoroutine(CoSpawnEnemy(prefab, onDeath));
    }

    private IEnumerator CoSpawnEnemy(GameObject prefab, Action onDeath = null)
    {
        if (CurrentPortal != null)
        {
            var enemy = Instantiate(prefab, Room.Instance.transform);
            enemy.transform.position = CurrentPortal.transform.position;

            yield return null;
            if (onDeath!= null)
            enemy.GetComponent<Enemy>().OnDeath.AddListener(() => onDeath());
        }
    }

    private void FixedUpdate()
    {
        if (_superCharged)
        {
            var mask = LayerMask.NameToLayer("Weapon");

            if (_eyeGaze != null)
            {
                var ray = new Ray(CameraCache.Main.transform.position, _eyeGaze.GazeDirection.normalized);
                var hits = Physics.RaycastAll(ray, 100f/*, mask*/);
                //Debug.DrawRay(CameraCache.Main.transform.position, _eyeGaze.GazeDirection, Color.blue, 5f);
                foreach (var hit in hits)
                {
                    if (hit.transform.gameObject.layer == mask)
                    {
                        var lightning = hit.transform.gameObject.GetComponentInChildren<LightningBallDamage>();

                        if (lightning != null)
                        {
                            lightning.SetSupercharged(true);
                            _superChargedBalls.Add(lightning);
                        }
                    }
                }
            }
        }
    }
}

public enum GameStates
{
    Intro,
    Prestart,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
}
