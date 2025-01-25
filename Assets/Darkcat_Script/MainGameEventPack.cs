using Gamemanager;
using System;
using UniRx;

public class MainGameEventPack : GameEventPack
{
    public IObservable<PlayerMoveCommand> OnPlayerMove => getSubject<PlayerMoveCommand>();
}
