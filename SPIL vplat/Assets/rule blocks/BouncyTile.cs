using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BouncyTile", menuName = "Tiles/Bouncy Tile")]
public class BouncyTile : Tile
{
    public float bounceForce = 10f;
    public Color gizmoColor = Color.magenta;

    // Optional: draw a little gizmo for debug
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.color = Color.white; // Keep sprite color normal
    }

#if UNITY_EDITOR
    // Editor-only: draw a magenta cube in scene view
    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
#endif
}

