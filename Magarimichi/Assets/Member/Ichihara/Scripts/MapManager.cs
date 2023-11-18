using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    // 配置するマップチップ
    [SerializeField]
    private MapChipController _mapChipController = null;
    // マップの縦横比
    [SerializeField]
    private Vector2 _mapWidthAndHeight = new Vector2(4, 4);
    // マップチップに設定するスプライトの配列
    public List<Sprite> MapChipSprites => _mapChipSprites;
    [SerializeField]
    private List<Sprite> _mapChipSprites = new List<Sprite>();

    // マップチップ配列
    private GameObject[,] _map = { };

    new private void Awake()
    {
        // null チェック
        if (_mapChipController == null)
        {
            GameObject obj = Resources.Load("Prefabs/MapChip") as GameObject;
            _mapChipController = obj.GetComponent<MapChipController>();
        }
        // 配列の大きさを設定
        _map = new GameObject[(int)_mapWidthAndHeight.y, (int)_mapWidthAndHeight.x];
        GenerateMap();
    }

    /// <summary>
    /// マップ生成
    /// </summary>
    private void GenerateMap()
    {
        int half = 2;
        // 欠けさせるマップチップの座標
        Vector2 hiddenMapChip = new Vector2(0, _mapWidthAndHeight.y - 1);
        // _map[0,0] の座標
        Vector3 mapChipPosition = new Vector3(
            -(_mapChipController.transform.localScale.x * _mapWidthAndHeight.x / half) + _mapChipController.transform.localScale.x / half
           , (_mapChipController.transform.localScale.y * _mapWidthAndHeight.y / half) - _mapChipController.transform.localScale.y / half);
        for (int heightCOunt = 0; heightCOunt < _mapWidthAndHeight.y; heightCOunt++)
        {
            // 一列配置する度に、次に配置するマップチップを 1 個分ずつ下にずらす
            if (heightCOunt != 0)
            {
                var dummyObj = _map[heightCOunt - 1, 0];
                mapChipPosition = new Vector3(
                    dummyObj.transform.position.x
                  , dummyObj.transform.position.y - _mapChipController.transform.localScale.y);
            }
            for (int widthCount = 0; widthCount < _mapWidthAndHeight.x; widthCount++)
            {
                // 配置する度にマップチップを 1 個分ずつ右にずらす
                if (widthCount != 0)
                    mapChipPosition += Vector3.right * _mapChipController.transform.localScale.x;
                GameObject mapChipObj = Instantiate(_mapChipController.gameObject, mapChipPosition, Quaternion.identity);
                mapChipObj.transform.SetParent(transform);
                _map[heightCOunt, widthCount] = mapChipObj;
            }
        }
        //_mapChipController.SetMapChipSprite();
        SpriteRenderer renderer = _map[(int)hiddenMapChip.y, (int)hiddenMapChip.x].GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    public GameObject GetMapChipData(int height, int width)
    {
        if (height >= _mapWidthAndHeight.y || width >= _mapWidthAndHeight.x)
        {
            Debug.LogError($"{_map[height, width]}の情報が取得できません。");
            return null;
        }
        return _map[height, width];
    }
}
