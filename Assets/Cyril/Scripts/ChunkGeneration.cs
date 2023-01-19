using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    [SerializeField] private List<ChunkData> _listChunk = new List<ChunkData>();
    [SerializeField] private List<ChunkData> _listChunkPause;
    [SerializeField] private GameObject _pointStart;
    [SerializeField] private Vector3 _spacing;
    [SerializeField] private GameObject _chunkFinish;
    [SerializeField] private int _numberSpawnChunk;
    [SerializeField] private int _numberOfSpawnChunkObs = 10;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        //MovePoint();
    }
    private void GenerateChunkObstacle()
    {
        int RandomN = Random.Range(0, _listChunk.Count);
        Instantiate(_listChunk[RandomN]._visual, _pointStart.transform.position + (_spacing * _numberSpawnChunk), Quaternion.identity);
        _numberSpawnChunk++;
    }
    private GameObject GenerateChunkPause()
    {
        int RandomN = Random.Range(0, _listChunkPause.Count);
        GameObject chunk = Instantiate(_listChunkPause[RandomN]._visual, _pointStart.transform.position + (_spacing * _numberSpawnChunk), Quaternion.identity);
        _numberSpawnChunk++;
        return chunk;
    }
    public void Generate()
    {
        for (int i = 0; i < _numberOfSpawnChunkObs; i++)
        {
            GenerateChunkObstacle();
            _chunkFinish = GenerateChunkPause();

        }
        MovePoint();
        //_numberSpawnChunk = 0;
    }
    private void MovePoint()
    {
        _pointStart.transform.position = _chunkFinish.transform.position + _spacing; 
    }
}
