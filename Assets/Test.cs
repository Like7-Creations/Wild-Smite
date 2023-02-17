using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int totaltilesX;
    public int totaltilesZ;

    public int TotalTilesX { get { return totaltilesX; } }
    public int TotalTilesZ { get { return totaltilesZ; } }

    public int tileWidth;
    public int tileHeight;


    private void OnDrawGizmos()
    {
        for (int x = 0; x <= totaltilesX; x++)
        {
            Gizmos.DrawLine(
                transform.position + new Vector3(x * tileWidth, 0, 0),
                transform.position + new Vector3(x * tileWidth, 0, tileHeight * totaltilesZ));
        }
       /* for (int z = 0; z <= totaltilesZ; z++)
        {
            Gizmos.DrawLine(
                transform.position + new Vector3(0, 0, z * tileHeight),
                transform.position + new Vector3(tileWidth * totaltilesX, 0, z * tileHeight));
        }*/
    }
}
