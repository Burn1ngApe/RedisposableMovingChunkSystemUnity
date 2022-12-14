using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;


[Serializable]
public class ChunkRowInfo
{
    public string Name;
    public float Speed;
    public Transform StartPoint;
    public int AmountOfChunksInRow;
    [HideInInspector] public float zEndPoint { get; set; }
    [NonReorderable] public List<ChunkInfo> ChunksInThisRow;
    [HideInInspector] public List<InstantiatedChunk> InstantiatedChunks;


    public InstantiatedChunk GetRandomInstantiatedChunk()
    {
        List<InstantiatedChunk> inactiveChunks = InstantiatedChunks.Where(i => i.Instance.activeSelf == false).ToList();
        var randomNumber = Random.Range(0, inactiveChunks.Count);

        inactiveChunks[randomNumber].Instance.SetActive(true);

        return inactiveChunks[randomNumber]; 
    }


    public InstantiatedChunk GetClosestChunkToStart()
    {
        var closest = GetInstantiatedChunksByActivity(true)
            .OrderByDescending(x => x.Instance.transform.position.z)
            .First();

        return closest;
    }


    public GameObject GetRandomInstantiatedChunk(string excludedName)
    {
        List<InstantiatedChunk> inactiveChunks = InstantiatedChunks.Where(i => i.Instance.activeSelf == false).ToList();

        // Exclude chunk by name
        foreach (var chunk in inactiveChunks)
        {
            if (chunk.Instance.gameObject.name == excludedName)
            {
                inactiveChunks.Remove(chunk);
                break;
            }
        }


        var randomNumber = Random.Range(0, inactiveChunks.Count);
        var randomChunk = inactiveChunks[randomNumber].Instance;
        randomChunk.SetActive(true);

        return randomChunk;
    }


    public GameObject GetInstantiatedChunkByName(string name, bool isActive)
    {
        GameObject chunkByName = null;

        foreach (var chunk in InstantiatedChunks)
        {
            if (chunk.Instance.name == name)
            {
                chunkByName = chunk.Instance;
                chunkByName.SetActive(isActive);
            }
        }

        return chunkByName;
    }


    public List<InstantiatedChunk> GetInstantiatedChunksByActivity(bool isActive)
    {
        List<InstantiatedChunk> ñhunks = InstantiatedChunks.Where(i => i.Instance.activeSelf == isActive).ToList();
        return ñhunks;
    }
}

[Serializable]
public struct ChunkInfo
{
    public string Name;
    public GameObject GO;
    public float ChunkAmount;
}

[Serializable]
public struct InstantiatedChunk
{
    public GameObject Instance;
    public float Length;
}