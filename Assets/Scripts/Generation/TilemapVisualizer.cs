using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap;
    [SerializeField] private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull;
    public void PaintFloorTile(IEnumerable<Vector2Int> floorPos)
    {
        PaintTile(floorPos, floorTilemap, floorTile);
    }

    private void PaintTile(IEnumerable<Vector2Int> pos, Tilemap tilemap, TileBase tile)
    {
        foreach (var p in pos)
        {
            PaintSingleTile(tilemap, tile, p);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePos, tile);
    }
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }


    internal void PaintSingleBasicWall(Vector2Int pos, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypeHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallTypeHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypeHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallTypeHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypeHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        if (tile != null)
            PaintSingleTile(wallTilemap, tile, pos);
    }
    // internal void PaintSingleBasicWall(Vector2Int pos, string binaryType)
    // {
    //     PaintSingleTile(wallTilemap, wallTop, pos);
    // }

    // internal void PaintSingleCornerWall(Vector2Int position, string neighboursBinaryType)
    // {
    //     throw new NotImplementedException();
    // }
}
