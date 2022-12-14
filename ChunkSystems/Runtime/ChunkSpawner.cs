using System.Collections.Generic;
using UnityEngine;

public class ChunksSpawner 
{
    public List<ChunkRowInfo> ChunksRows = new List<ChunkRowInfo>();

    public ChunksSpawner(List<ChunkRowInfo> chunksRows) => ChunksRows = chunksRows;


    public void SpawnChunks()
    {
        SpawnChunksByRow();
        DisableChunks();
        RepositionAtStart();
    }


    private void SpawnChunksByRow()
    {
        foreach (var row in ChunksRows)
        {
            foreach (var chunk in row.ChunksInThisRow)
            {
                for (int i = 0; i < chunk.ChunkAmount; i++)
                {
                    var newChunk = GameObject.Instantiate(chunk.GO);
                    var tempalate = newChunk.GetComponent<ChunkTempalate>();

                    InstantiatedChunk instChunk = new();
                    instChunk.Instance = newChunk; 
                    instChunk.Length = tempalate.ChunkLength;

                    row.InstantiatedChunks.Add(instChunk);
                    GameObject.Destroy(tempalate);
                }
            }
        }
    }


    private void DisableChunks()
    {
        foreach (var row in ChunksRows)
        {
            foreach (var chunk in row.InstantiatedChunks)
            {
                chunk.Instance.SetActive(false);
            }
        }
    }


    private void RepositionAtStart()
    {
        foreach (var row in ChunksRows)
        {
            float MaxValueForReposition = 0;
            float nextPos = row.StartPoint.position.z;

            for (int i = 0; i < row.AmountOfChunksInRow; i++)
            {
                var chunk = row.GetRandomInstantiatedChunk();
                MaxValueForReposition += chunk.Length;

                chunk.Instance.transform.position = new Vector3(row.StartPoint.position.x, row.StartPoint.position.y, nextPos);
                nextPos = chunk.Instance.transform.position.z - chunk.Length;
            }

            row.zEndPoint = MaxValueForReposition + row.StartPoint.position.z;
        }
    }
}

