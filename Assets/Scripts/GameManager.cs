using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using Gamemanager;

public class GameManager : ToSingletonMonoBehavior<GameManager>
{
    public MainGameEventPack MainGameEvent { get; private set; } = new MainGameEventPack();
    [field: SerializeField] public MainGameMediator MainGameMediator { get; private set; }
    [SerializeField] public GameObject PlayerOBJ;
    [SerializeField] public GameObject breathingNomalMusicPlayer;
    [SerializeField] public GameObject runMusicPlayer;
    protected override void init()
    {
        base.init();
        MainGameMediator = new MainGameMediator();
        MainGameMediator.MainGameMediatorInit();
    }
}
