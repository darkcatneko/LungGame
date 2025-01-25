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
    protected override void init()
    {
        base.init();
        MainGameMediator = new MainGameMediator();
        MainGameMediator.MainGameMediatorInit();
    }
}
