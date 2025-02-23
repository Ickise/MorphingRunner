using System.Collections.Generic;
using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    // Ce script permet de générer des chunks de manière procédurale. Il est possible de définir des chunks de pause et des chunks d'obstacles.
    // Nous avions séparé les datas et la gestion pour plus de clarté.
    
    // La nomenclature n'est pas correcte, les variables commencent par _ pour indiquer qu'elles sont privées, mais cela n'est pas nécessaire.
    // De plus, les noms de variables ne sont pas explicites.
    [SerializeField] private List<ChunkData> _listChunk = new List<ChunkData>(); // Nous ne vérifions jamais si ces listes sont vides, cela pourrait poser problème.
    [SerializeField] private List<ChunkData> _listChunkPause; // De même pour cette liste.
    [SerializeField] private GameObject _pointStart;
    [SerializeField] private Vector3 _spacing;
    [SerializeField] private float _spacingPlus;
    [SerializeField] private GameObject _chunkFinish;
    [SerializeField] private int _numberSpawnChunk;
    [SerializeField] private int _numberOfSpawnChunkObs = 1;
    [SerializeField] private bool _movePoint;
    [SerializeField] private bool _newSeed;
    [SerializeField] private bool _rotateWord;
    [SerializeField] private bool _once = true;
    [SerializeField] private int _mySeed;
    [SerializeField] private int _seed;
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _time;
    private float _timeMin = 30;
    private void Awake()
    {
        GenerationSeed();
    }
    void Start()
    {
        Generate();
    }
    private void Update()
    {
        // Cette méthode est peu claire puisque si la position en Z est inférieur à 150, on génère un nouveau chunk sans prendre en compte la taille du chunk.
        // Si les chunks sont de taille variable, cela peut poser un problème.
        // De plus, l'utilisation répétée de transform.position est coûteuse en performances.
        if (_pointStart.transform.position.z < 150)
        {
            Generate();
        }
        Speed();
    }
    private Quaternion Rotation(ChunkData _chunkData)
    {
        if (_rotateWord) return Quaternion.identity;
        else return _chunkData._visual.transform.rotation;

    }
    
    // Cette méthode fait quasiment la même chose que GenerateChunkPause, il serait plus judicieux de les fusionner.
    // Nous aurions pu créer une méthode GenerateChunk qui prend en paramètre une liste de chunks et un booléen pour savoir si nous devions faire une pause ou non.
    private void GenerateChunkObstacle()
    {
        int RandomN = Random.Range(0, _listChunk.Count);
        _spacing = _spacing + _listChunk[RandomN]._size;
        GameObject Chunk = Instantiate(_listChunk[RandomN]._visual, _pointStart.transform.position + _spacing, Rotation(_listChunk[RandomN]));
        MoveDecor moveDecor = Chunk.GetComponent<MoveDecor>();
        moveDecor.SetSpeedAdd(_speed);
        _spacing = _spacing + _listChunk[RandomN]._size;
    }
    private GameObject GenerateChunkPause()
    {
        int RandomN = Random.Range(0, _listChunkPause.Count);
        _spacing = _spacing + _listChunkPause[RandomN]._size;
        GameObject chunk = Instantiate(_listChunkPause[RandomN]._visual, _pointStart.transform.position + _spacing, Rotation(_listChunkPause[RandomN]));
        MoveDecor moveDecor = chunk.GetComponent<MoveDecor>();
        moveDecor.SetSpeedAdd(_speed);
        _spacing = _spacing + _listChunkPause[RandomN]._size;
        return chunk;
    }
    public void Generate()
    {
        _movePoint = true;
        for (int i = 0; i < _numberOfSpawnChunkObs; i++)
        {
            if (_once == true)
            {
                GenerateChunkPause();
                _once = false;
            }
            GenerateChunkObstacle();
            _chunkFinish = GenerateChunkPause();
        }
        _spacing = Vector3.zero;
        MovePoint();
        _movePoint = false;
    }
    
    // Encore une fois l'utilisation répétée de transfom.position est coûteuse en performances.
    // Nous aurions pu simplement la stocker dans une variable Vector3. 
    private void MovePoint()
    {
        _pointStart.transform.position = _chunkFinish.transform.position + _spacing;
        MoveDecor moveDecor = _pointStart.GetComponent<MoveDecor>();
        moveDecor.SetSpeed(_speed);
    }
    
    private void GenerationSeed()
    {
        // Nous aurions pu utiliser un ternaire pour simplifier le code : _seed = _newSeed ? Random.Range(int.MinValue, int.MaxValue) : _mySeed;
        if (_newSeed == true)
        {
            _seed = Random.Range(int.MinValue, int.MaxValue);
        }
        else
        {
            _seed = _mySeed;
        }

        Random.InitState(_seed);
    }
    
    // Cette méthode est peu claire, mais elle permet en réalité de set up la vitesse du jeu en fonction du temps.
    private void Speed()
    {
        _time = _time + Time.deltaTime;

        if (_speed < 20 && _time >= _timeMin)
        {
            _speed += 1;
            _time = 0;
        }
    }
}
