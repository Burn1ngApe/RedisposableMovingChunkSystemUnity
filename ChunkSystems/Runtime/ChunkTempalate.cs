using UnityEngine;

public class ChunkTempalate : MonoBehaviour
{
    public float ChunkLength;

    private void OnDrawGizmos()
    {
        var pos = transform.position;

        Gizmos.color = Color.white;
        Gizmos.DrawLine(pos, new Vector3(pos.x + 15f, pos.y, pos.z));
        Gizmos.DrawLine(new Vector3(pos.x, pos.y, pos.z + 2f), new Vector3(pos.x + 2f, pos.y, pos.z));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(pos.x, pos.y, pos.z + ChunkLength));
    }
}
