using System.Collections.Generic;
using UnityEngine;

public class ChunksMover
{
    private List<ChunkRowInfo> _chunksRows = new List<ChunkRowInfo>();
    private bool _isMoving = true;

    public ChunksMover(List<ChunkRowInfo> chunksRows) => _chunksRows = chunksRows;


    public void MoveFormations()
    {
        if (_isMoving)
        {
            foreach (var row in _chunksRows)
            {
                var activeChunks = row.GetInstantiatedChunksByActivity(true);

                for (int i = 0; i < activeChunks.Count; i++)
                {
                    var chunk = activeChunks[i].Instance;

                    chunk.transform.position += chunk.transform.forward * row.Speed * Time.deltaTime;
                    RepositionChunk(row, activeChunks[i], row.zEndPoint);
                }
            }
        }
    }


    private void RepositionChunk(ChunkRowInfo row, InstantiatedChunk instChunk, float endPoint)
    {
        if (instChunk.Instance.transform.position.z < -endPoint)
        {
            instChunk.Instance.SetActive(false);

            var closestChunkPos = row.GetClosestChunkToStart().Instance.transform.position.z;

            var randomChunk = row.GetRandomInstantiatedChunk();
            randomChunk.Instance.transform.position = new Vector3(row.StartPoint.position.x, row.StartPoint.position.y,
            closestChunkPos + randomChunk.Length);
        }
    }
}


