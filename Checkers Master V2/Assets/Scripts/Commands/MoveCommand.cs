using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : CommandManager.ICommand
{
    private Vector3Int start;
    private Vector3Int end;

    public MoveCommand(Vector3Int start, Vector3Int end)
    {
        this.start = start;
        this.end = end;
    }

    public void Move()
    {
        var checker = Gameplay.Instance.GetChecker(start);

        if(checker != null)
        {
            Gameplay.Instance.MoveChecker(checker, end);
            Gameplay.Instance.SwitchTeam();
        }

    }
    
}
