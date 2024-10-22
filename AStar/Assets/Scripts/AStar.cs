using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    [SerializeField] Transform _start;

    [SerializeField] Transform _end;

    private const int _constStraigtCost = 10;

    private const int _constDiagoanalCost = 14;

    private SortedSet<Vector2Int> _priorityQueue;  
    void Start()
    {
        Vector2Int start = new Vector2Int((int)_start.position.x, (int)_start.position.y);
    }

  
    void Update()
    {
        
    }

    private void DoAStar(Vector2Int start, Vector2Int end)
    {
        _priorityQueue.Add(start);
    }

    private int GetHeuristic(Vector2Int start, Vector2Int end)
    {
        int xSize = Mathf.Abs(start.x - end.x);
        int ySize = Mathf.Abs(start.y - end.y);

        int straightCost = Mathf.Abs(xSize - ySize);
        int diagonalCost= Mathf.Max(xSize, ySize) - straightCost;
        return _constStraigtCost * straightCost + _constDiagoanalCost * diagonalCost;

    }
}
