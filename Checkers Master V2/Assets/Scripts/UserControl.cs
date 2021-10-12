using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    [SerializeField] GameObject selectIndicator;
    [SerializeField] GameObject possibleMoveIndicator;

    private Vector3Int[] possibleCellsToMove;
    private List<GameObject> possibleMovePools = new List<GameObject>();

    const int maximumNumberOfMoves = 4;

    private void Start()
    {
        currentState = GameState.CheckerSelecting;
        possibleCellsToMove = new Vector3Int[maximumNumberOfMoves];
        selectIndicator.SetActive(false);

        for(int i = 0; i < maximumNumberOfMoves; i++)
        {
            var go = Instantiate(possibleMoveIndicator);
            go.SetActive(false);
            possibleMovePools.Add(go);
        }

    }
    private void Update()
    {
        switch (currentState)
        {
            case GameState.CheckerSelecting:
                if (Input.GetMouseButton(0))
                {
                    SelectChecker();
                }
           break;

            case GameState.CheckerMoving:
                if (Input.GetMouseButton(0))
                {
                    MoveChecker();
                }
                break;
        }


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
                DeselectChecker();
                selectedChecker = clickedChecker;
            }
            else
            {
                selectedChecker = null;
            }

            if(selectedChecker != null)
            {
                selectIndicator.SetActive(true);
                var newPosition = new Vector3(selectedChecker.transform.position.x, -0.1f, selectedChecker.transform.position.z);
                selectIndicator.transform.position = newPosition;

                Vector3Int[] directions = null;
                int count = selectedChecker.FindPossibleCellsToMove( out possibleCellsToMove, directions);

                for(int i = 0; i < count; i++)
                {
                    possibleMovePools[i].SetActive(true);
                    possibleMovePools[i].transform.position = Gameplay.Instance.Grid.GetCellCenterWorld(possibleCellsToMove[i]);
                    possibleMovePools[i].transform.position += new Vector3(0, -0.12f, 0);
                }
                currentState = GameState.CheckerMoving;
            }
            
            else
            {
                DeselectChecker();
            }
        }
    }

    private void DeselectChecker()
    {
        selectedChecker = null;
        selectIndicator.SetActive(false);

        foreach(GameObject indicator in possibleMovePools)
        {
            indicator.SetActive(false);
        }
    }
    
    private void MoveChecker()
    {
        if (Gameplay.Instance.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Vector3Int clickedCell))
        {
            if(possibleCellsToMove.Contains(clickedCell))
            {
                MoveCommand command = new MoveCommand(selectedChecker.CurrentCell, clickedCell);
                CommandManager.Instance.AddCommand(command);
            }
        }
    }


}
