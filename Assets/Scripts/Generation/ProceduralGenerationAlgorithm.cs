
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithm
{
   public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPos = startPosition;
        for (int i = 0; i < walkLength; i++)
        {
            var newPos = previousPos + Direction2D.GetRandomCardinalDirection();
            path.Add(newPos);
            previousPos = newPos;
        }
        return path;
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPos;
        corridor.Add(currentPosition);
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
}
public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>(){
        new Vector2Int(0,1),    //UP
        new Vector2Int(1,0),    //RIGHT
        new Vector2Int(0,-1),   //DOWN
        new Vector2Int(-1,0)    //LEFT
    };
    public static List<Vector2Int> diagonalDirectionList = new List<Vector2Int>(){
        new Vector2Int(1,1),    //UP-RIGHT
        new Vector2Int(-1,1),    //UP-LEFT
        new Vector2Int(-1,-1),   //DOWN-LEFT
        new Vector2Int(1,-1)    //DOWN-RIGHT
    };
    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP

    };
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}
