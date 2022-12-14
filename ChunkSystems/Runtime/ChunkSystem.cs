using System.Collections.Generic;
using UnityEngine;

public class ChunkSystem : MonoBehaviour
{
    public bool SpawnChunks = false;
    public List<ChunkRowInfo> ChunkRows = new();

    private ChunksSpawner _chunksSpawner;
    private ChunksMover _chunksMover;


    private void Awake()
    {
        _chunksMover = new(ChunkRows);

        if (SpawnChunks)
        {
            _chunksSpawner = new(ChunkRows);
            _chunksSpawner.SpawnChunks();
        }
    }


    private void Update() => _chunksMover.MoveFormations();
}
