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

    private Vector3Int[] possibleCellsToMove;

    const int maximumNumberOfMoves = 4;

    private void Start()
    {
        currentState = GameState.CheckerSelecting;
        possibleCellsToMove = new Vector3Int[maximumNumberOfMoves];
        selectIndicator.SetActive(false);
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
