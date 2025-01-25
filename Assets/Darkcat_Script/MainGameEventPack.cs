using Gamemanager;
using System;

public class MainGameEventPack : GameEventPack
{
    public IObservable<WallShrink> TriggerWallShrink => getSubject<WallShrink>();
}
