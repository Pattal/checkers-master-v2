using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance;

    public int Height;
    public int Width;

    public Grid Grid { get; private set; }
    private Plane plane;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        Grid = GetComponent<Grid>();
        plane = new Plane(Vector3.up, Vector3.zero);
    }

    // Update is called once per frame
    private void Update()
    {
        MouseInput();
    }

    public void Raycast(Ray ray, out Vector3Int field)
    {
        field = Vector3Int.zero;

        if(Input.GetMouseButtonUp(0))
        {
            if (plane.Raycast(ray, out float vector))
            {
                field = Gameplay.Instance.Grid.WorldToCell(ray.GetPoint(vector));
            }
        }

        
    }

    private void MouseInput()
    {
        Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Vector3Int clickedCell);
        Debug.Log(clickedCell);
    }
}
