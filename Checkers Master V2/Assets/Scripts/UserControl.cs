using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour
{
    enum GameState
    {
        CheckerSelecting,
        CheckerMoving,
        CheckerAttack
    }

    private GameState currentState;
    private Checker selectedChecker;

    private Vector3Int[] possibleCellsToMove;

    const int maximumNumberOfMoves = 4;

    private void Start()
    {
        currentState = GameState.CheckerSelecting;
        possibleCellsToMove = new Vector3Int[maximumNumberOfMoves];

    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            SelectChecker();
        }
    }
    private void SelectChecker()
    {
        if(Gameplay.Instance.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Vector3Int clickedCell))
        {
            var clickedChecker = Gameplay.Instance.GetChecker(clickedCell);

            if(clickedChecker != null && clickedChecker.Side == Gameplay.Instance.CurrentTeam)
            {
                selectedChecker = clickedChecker;
            }
            else
            {
                selectedChecker = null;
            }

            Debug.Log(selectedChecker);
        }
    }


}
