using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPathfinding : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    //public static HumanPathfinding Instance { get; private set; }

    public GridManager gridManager;
    public Dictionary<Vector2, Tile> tiles;

    private List<Tile> openList;
    private List<Tile> closedList;
    private HumanController human;

    //public HumanPathfinding(int width, int height)
    //{
    //    Instance = this;
    //}

    // Start is called before the first frame update

    void Awake()
    {
        //human = gameObject.GetComponent<HumanController>();
        //gridManager = GameObject.FindWithTag("Grid").GetComponent<GridManager>();
        //tiles = gridManager._tiles;
        
        //Debug.Log(tiles + " tiles");
    }

    // Update is called once per frame
    void Update()
    {
        human = gameObject.GetComponent<HumanController>();
        gridManager = GameObject.FindWithTag("Grid").GetComponent<GridManager>();
        tiles = gridManager._tiles;

        Debug.Log(tiles + " tiles");
    }
    public List<Vector3> FindPath(Vector2 startWorldPosition, Vector2 endWorldPosition)
    {
        Debug.Log("start: " + startWorldPosition);
        Debug.Log("end: " + endWorldPosition);

        Tile startTile = tiles[startWorldPosition];
        Tile endTile = tiles[endWorldPosition];

        List<Tile> path = FindPath((int)startTile.position.x, (int)startTile.position.y, (int)endTile.position.x, (int)endTile.position.y);
        if (path == null)
        {
            Debug.Log("really bad null");
            return null;
        }
        else
        {
            float cellSize = 1;
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (Tile tile in path)
            {
                vectorPath.Add(new Vector3(tile.position.x, tile.position.y) * cellSize + Vector3.one * cellSize * .5f);
            }
            return vectorPath;
        }
    }
    public List<Tile> FindPath(int startX, int startY, int endX, int endY)
    {
        Debug.Log(startX + " " + startY + " " + endX + " " + endY);
        Tile startTile = tiles[new Vector2(startX, startY)];
        Tile endTile = tiles[new Vector2(endX, endY)];

        if (startTile == null || endTile == null)
        {
            
            // Invalid Path
            return null;
        }

        openList = new List<Tile> { startTile };
        closedList = new List<Tile>();

        for (int x = 0; x < gridManager.GetWidth(); x++)
        {
            for (int y = 0; y < gridManager.GetHeight(); y++)
            {
                Tile tile =  tiles[new Vector2(x, y)];
                tile.gCost = 99999999;
                tile.CalculateFCost();
                tile.cameFromTile = null;
            }
        }

        startTile.gCost = 0;
        startTile.hCost = CalculateDistanceCost(startTile, endTile);
        startTile.CalculateFCost();
        Debug.Log("hCost" + startTile.hCost);


        while (openList.Count > 0)
        {
            Tile currentTile = GetLowestFCostTile(openList);
            if (currentTile == endTile)
            {
                // Reached final node
                Debug.Log("final");
                return CalculatePath(endTile);
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            foreach (Tile neighbourTile in GetNeighbourList(currentTile))
            {
                if (closedList.Contains(neighbourTile)) continue;
                if (!neighbourTile.isWalkable)
                {
                    closedList.Add(neighbourTile);
                    continue;
                }

                int tentativeGCost = currentTile.gCost + CalculateDistanceCost(currentTile, neighbourTile);
                if (tentativeGCost < neighbourTile.gCost)
                {
                    neighbourTile.cameFromTile = currentTile;
                    neighbourTile.gCost = tentativeGCost;
                    neighbourTile.hCost = CalculateDistanceCost(neighbourTile, endTile);
                    neighbourTile.CalculateFCost();

                    if (!openList.Contains(neighbourTile))
                    {
                        openList.Add(neighbourTile);
                    }
                }
            }
        }

        //  Out of nodes on the openList
        return null;
    }

    private int CalculateDistanceCost(Tile a, Tile b)
    {
        int xDistance = (int) Mathf.Abs(a.position.x - b.position.x);
        int yDistance = (int) Mathf.Abs(a.position.y - b.position.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private Tile GetLowestFCostTile(List<Tile> tileList)
    {
        Tile lowestFCostTile = tileList[0];
        for (int i = 1; i < tileList.Count; i++)
        {
            if (tileList[i].fCost < lowestFCostTile.fCost)
            {
                lowestFCostTile = tileList[i];
            }
        }
        return lowestFCostTile;
    }
    private List<Tile> CalculatePath(Tile endTile)
    {
        List<Tile> path = new List<Tile>();
        path.Add(endTile);
        Tile currentTile = endTile;
        while (currentTile.cameFromTile != null)
        {
            path.Add(currentTile.cameFromTile);
            currentTile = currentTile.cameFromTile;
        }
        path.Reverse();
        return path;
    }

    private List<Tile> GetNeighbourList(Tile currentTile)
    {
        List<Tile> neighbourList = new List<Tile>();

        if (currentTile.position.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetTile(currentTile.position.x - 1f, currentTile.position.y));
            // Left Down
            if (currentTile.position.y - 1 >= 0) neighbourList.Add(GetTile(currentTile.position.x - 1, currentTile.position.y - 1));
            // Left Up
            if (currentTile.position.y + 1 < gridManager.GetHeight()) neighbourList.Add(GetTile(currentTile.position.x - 1, currentTile.position.y + 1));
        }
        if (currentTile.position.x + 1 < gridManager.GetWidth())
        {
            // Right
            neighbourList.Add(GetTile(currentTile.position.x + 1, currentTile.position.y));
            // Right Down
            if (currentTile.position.y - 1 >= 0) neighbourList.Add(GetTile(currentTile.position.x + 1, currentTile.position.y - 1));
            // Right Up
            if (currentTile.position.y + 1 < gridManager.GetHeight()) neighbourList.Add(GetTile(currentTile.position.x + 1, currentTile.position.y + 1));
        }
        // Down
        if (currentTile.position.y - 1 >= 0) neighbourList.Add(GetTile(currentTile.position.x, currentTile.position.y - 1));
        // Up
        if (currentTile.position.y + 1 < gridManager.GetHeight()) neighbourList.Add(GetTile(currentTile.position.x, currentTile.position.y + 1));

        return neighbourList;
    }

    public Tile GetTile(float x, float y)
    {
        return tiles[new Vector2(x, y)];
    }
}
