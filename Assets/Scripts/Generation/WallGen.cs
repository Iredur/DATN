using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGen
{
    public static void CreateWall(HashSet<Vector2Int> floorPos, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPos = FindWallsInDirection(floorPos, Direction2D.cardinalDirectionList);
        var corner = FindWallsInDirection(floorPos, Direction2D.diagonalDirectionList);
        CreateBasicWall(tilemapVisualizer, basicWallPos, floorPos);
        //CreateCornerWall(tilemapVisualizer, corner, floorPos);
    }

    private static void CreateCornerWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var position in cornerWallPos)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPos.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            //tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var position in basicWallPos)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                var neighbourPosition = position + direction;
                if (floorPos.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirection(HashSet<Vector2Int> floorPos, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var pos in floorPos)
        {
            foreach (var direction in directionList)
            {
                var neighborPos = pos + direction;
                if (!floorPos.Contains(neighborPos))
                {
                    wallPos.Add(neighborPos);
                }
            }
        }
        return wallPos;
    }
}
