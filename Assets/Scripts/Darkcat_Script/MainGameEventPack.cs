using Gamemanager;
using System;
using UniRx;

public class MainGameEventPack : GameEventPack
{
    public IObservable<WallShrink> TriggerWallShrink => getSubject<WallShrink>();
    public IObservable<PlayerMoveCommand> OnPlayerMove => getSubject<PlayerMoveCommand>();
    public IObservable<PlayerHurt> OnPlayerHurt => getSubject<PlayerHurt>();
    public IObservable<SetPlayerBGM> OnSetPlayerBGM => getSubject<SetPlayerBGM>();
}
