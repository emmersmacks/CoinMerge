using System.Collections;
using CodeBase.Infrastructure.Services;
using UnityEngine;

public class Game
{
    public StateMachine StateMachine { get; private set; }

    public Game(Runpoint runpoint)
    {
        StateMachine = new StateMachine(new AllServices(), new SceneLoader(runpoint), runpoint);
    }
}