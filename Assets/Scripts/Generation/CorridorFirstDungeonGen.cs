using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using NavMeshPlus.Components;

public class CorridorFirstDungeonGen : SimpleRndomWalkMapGen
{
    [SerializeField] private GameObject Spawner;
    [SerializeField] GameObject player;
    [SerializeField] float distance = 10;
    [SerializeField] private int corridorLength = 14, corridorCount = 5;
    [SerializeField][Range(0.1f, 1)] float roomPercent = 0.8f;
    public NavMeshSurface surface;

    private void Start()
    {
        CorridorFirstDungeonGennerator();
    }

    protected override void RunProceduralGen()
    {
        CorridorFirstDungeonGennerator();
        //StartCoroutine(scanWait());
    }
    IEnumerator scanWait()
    {
        yield return new WaitForSeconds(1);
    }
    private void CorridorFirstDungeonGennerator()
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridor(floorPos, potentialRoomPos);

        HashSet<Vector2Int> roomPos = CreateRooms(potentialRoomPos);
        List<Vector2Int> deadEnd = FindAllDeadEnd(floorPos);
        CreateRoomAsDeadEnd(deadEnd, roomPos);
        floorPos.UnionWith(roomPos);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorBrushBy3(corridors[i]);
            floorPos.UnionWith(corridors[i]);
        }
        tilemapVisualizer.PaintFloorTile(floorPos);

        WallGen.CreateWall(floorPos, tilemapVisualizer);
        Spawn(randomWalkCache);
        surface.BuildNavMeshAsync();
    }
    void Spawn(List<Vector2Int> roomPos)
    {
        for (int i = 0; i < roomPos.Count; i++)
        {
            if (roomPos[i] != startPos && i % 3 == 0 && Vector3.Distance(player.transform.position, new Vector3(roomPos[i].x, roomPos[i].y, 0)) > distance)
            {
                Vector3Int pos3 = new Vector3Int(roomPos[i].x, roomPos[i].y, 0);

                Instantiate(Spawner, pos3, Quaternion.identity);
            }
        }
    }

    private List<Vector2Int> IncreaseCorridorBrushBy3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    private void CreateRoomAsDeadEnd(List<Vector2Int> deadEnd, HashSet<Vector2Int> roomFloors)
    {
        foreach (var pos in deadEnd)
        {
            if (!roomFloors.Contains(pos))
            {
                var room = RunRandomWalk(randomWalkParameter, pos);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnd(HashSet<Vector2Int> floorPos)
    {
        List<Vector2Int> deadEnd = new List<Vector2Int>();
        foreach (var pos in floorPos)
        {
            int neighborCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if (floorPos.Contains(pos + direction))
                {
                    neighborCount++;
                }
                if (neighborCount == 1)
                    deadEnd.Add(pos);
            }
        }
        return deadEnd;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> PotentialRoomPos)
    {
        HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(PotentialRoomPos.Count * roomPercent);

        List<Vector2Int> roomToCreate = PotentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameter, roomPosition);
            roomPos.UnionWith(roomFloor);
        }
        return roomPos;
    }

    private List<List<Vector2Int>> CreateCorridor(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos)
    {
        var currentPosition = startPos;
        potentialRoomPos.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
        for (int i = 0; i < corridorLength; i++)
        {
            var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPos.Add(currentPosition);
            floorPos.UnionWith(corridor);
        }
        return corridors;
    }
}
