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

            if(selectedChecker != null)
            {
                selectIndicator.SetActive(true);
                var newPosition = new Vector3(selectedChecker.transform.position.x, -0.1f, selectedChecker.transform.position.z);
                selectIndicator.transform.position = newPosition;

                int count = selectedChecker.FindPossibleCellsToMove( out possibleCellsToMove);

                for(int i = 0; i < count; i++)
                {
                    possibleMovePools[i].SetActive(true);
                    possibleMovePools[i].transform.position = Gameplay.Instance.Grid.GetCellCenterWorld(possibleCellsToMove[i]);
                }
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
    }


}
