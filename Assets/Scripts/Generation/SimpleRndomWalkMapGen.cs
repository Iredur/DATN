using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRndomWalkMapGen : AbstractDungeonGen
{
    protected List<Vector2Int> randomWalkCache = new List<Vector2Int>();
    [SerializeField] protected SimpleRandomWalkSO randomWalkParameter;
    protected override void RunProceduralGen()
    {
        HashSet<Vector2Int> floorPos = RunRandomWalk(randomWalkParameter, startPos);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTile(floorPos);
        WallGen.CreateWall(floorPos, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO RandomWalkParameter, Vector2Int position)
    {
        var currentPosition = position;
        randomWalkCache.Add(position);
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < RandomWalkParameter.iteration; i++)
        {
            var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPosition, RandomWalkParameter.walkLength);
            floorPos.UnionWith(path);
            if (RandomWalkParameter.startRandomlyEachIteration)
            {
                currentPosition = floorPos.ElementAt(Random.Range(0, floorPos.Count));
            }
        }

        return floorPos;
    }
}
