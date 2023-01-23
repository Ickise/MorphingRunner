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
    [SerializeField] private bool _movePoint;
    [SerializeField] private bool _newSeed;
    [SerializeField] private int _mySeed;
    [SerializeField] private int _seed;
    private void Awake() 
    {
        GenertaionSeed();
    }
    void Start()
    {
        Generate();
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
        _movePoint = true;
        for (int i = 0; i < _numberOfSpawnChunkObs; i++)
        {       
            GenerateChunkObstacle();
            _chunkFinish = GenerateChunkPause();
        }
        MovePoint();
        _numberSpawnChunk = 0;
        _movePoint = false;
    }
    private void MovePoint()
    {
        _pointStart.transform.position = _chunkFinish.transform.position + _spacing; 
    }
    private void GenertaionSeed()
    {
        if(_newSeed == true)
        {
            _seed = Random.Range(int.MinValue,int.MaxValue);
        }
        else
        {
            _seed = _mySeed;
        }
        Random.InitState(_seed);
    }
}
