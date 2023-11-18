using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    // 配置するマップチップ
    [SerializeField]
    private GameObject _mapChip = null;
    // マップの縦横比
    [SerializeField]
    private Vector2 _mapWidthAndHeight = new Vector2(4, 4);

    // マップチップ配列
    public GameObject[,] Map => _map;
    private GameObject[,] _map = { };

    new private void Awake()
    {
        // null チェック
        if (_mapChip == null)
            _mapChip = Resources.Load("Prefabs/MapChip") as GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 配列の大きさを設定
        _map = new GameObject[(int)_mapWidthAndHeight.x, (int)_mapWidthAndHeight.y];
        GenerateMap();
    }

    /// <summary>
    /// マップ生成
    /// </summary>
    private void GenerateMap()
    {
        int half = 2;
        // _map[0,0] の座標
        Vector3 mapChipPosition = new Vector3(
            -(_mapChip.transform.localScale.x * _mapWidthAndHeight.x / half) + _mapChip.transform.localScale.x / half
           , (_mapChip.transform.localScale.y * _mapWidthAndHeight.y / half) - _mapChip.transform.localScale.y / half);
        for (int i = 0; i < _mapWidthAndHeight.y; i++)
        {
            // 一列配置する度に、次に配置するマップチップを 1 個分ずつ下にずらす
            if (i != 0)
            {
                var dummyObj = _map[0, i - 1];
                mapChipPosition = new Vector3(
                    dummyObj.transform.position.x
                  , dummyObj.transform.position.y - _mapChip.transform.localScale.y);
            }
            for (int j = 0; j < _mapWidthAndHeight.x; j++)
            {
                // 配置する度にマップチップを 1 個分ずつ右にずらす
                if (j != 0)
                    mapChipPosition += Vector3.right * _mapChip.transform.localScale.x;
                GameObject mapChipObj = Instantiate(_mapChip, mapChipPosition, Quaternion.identity);
                mapChipObj.transform.SetParent(transform);
                _map[j, i] = mapChipObj;
            }
        }
    }
}
