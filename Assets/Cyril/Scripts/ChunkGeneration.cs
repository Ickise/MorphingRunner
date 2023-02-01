using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    [SerializeField] private List<ChunkData> _listChunk = new List<ChunkData>();
    [SerializeField] private List<ChunkData> _listChunkPause;
    [SerializeField] private GameObject _pointStart;
    [SerializeField] private Vector3 _spacing;
    [SerializeField] private float _spacingPlus;
    [SerializeField] private GameObject _chunkFinish;
    [SerializeField] private int _numberSpawnChunk;
    [SerializeField] private int _numberOfSpawnChunkObs = 10;
    [SerializeField] private bool _movePoint;
    [SerializeField] private bool _newSeed;
    [SerializeField] private bool _rotateWord;
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
    private void Update() 
    {
        if(_pointStart.transform.position.z < 150) 
        {
            Generate();        
        }       
    }
    private Quaternion Rotation(ChunkData _chunkData)
    {
        if(_rotateWord) return Quaternion.identity; 
        else return _chunkData._visual.transform.rotation;
        
    }
    private void GenerateChunkObstacle()
    {
        int RandomN = Random.Range(0, _listChunk.Count);
        _spacing = _spacing + _listChunk[RandomN]._size;
        Instantiate(_listChunk[RandomN]._visual, _pointStart.transform.position + _spacing, Rotation(_listChunk[RandomN]));
        _spacing = _spacing + _listChunk[RandomN]._size;
    }
    private GameObject GenerateChunkPause()
    {
        int RandomN = Random.Range(0, _listChunkPause.Count);
        _spacing = _spacing + _listChunkPause[RandomN]._size;
        GameObject chunk = Instantiate(_listChunkPause[RandomN]._visual, _pointStart.transform.position + _spacing, Rotation(_listChunkPause[RandomN]));
        _spacing = _spacing + _listChunkPause[RandomN]._size;
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
        _spacing = Vector3.zero;
        MovePoint();
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
