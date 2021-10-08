using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
   public enum Team 
    { 
        White,
        Black
    }

    public Vector3Int CurrentCell;
    public Team Side;

    void Start()
    {
        CurrentCell = Gameplay.Instance.GetCoodiantesOfCell(transform.position);
        Gameplay.Instance.SetPlayingChecker(this);
        Debug.Log(Gameplay.Instance.IsCheckerPlaying(CurrentCell));
    }


}
