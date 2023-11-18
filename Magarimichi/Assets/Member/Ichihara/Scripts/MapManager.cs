using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    #region Refarences
    // 配置するマップチップ
    [SerializeField]
    private MapChipController _mapChipController = null;
    [SerializeField]
    private GameObject _key = null;
    [SerializeField]
    private GameObject _lock = null;
    // マップチップに設定するスプライトの配列
    public List<Sprite> MapChipSprites => _mapChipSprites;
    [SerializeField]
    private List<Sprite> _mapChipSprites = new List<Sprite>();
    #endregion
    #region マップの大きさ
    // マップの縦横比
    [SerializeField]
    private Vector2 _mapWidthAndHeight = new Vector2(4, 4);
    #endregion

    // マップチップ配列
    // 配列のイメージは、[縦のマップチップ, 横のマップチップ]でお願いします。
    private GameObject[,] _map = { };

    new private void Awake()
    {
        // null チェック
        if (_mapChipController == null)
        {
            GameObject obj = Resources.Load("Prefabs/MapChip") as GameObject;
            _mapChipController = obj.GetComponent<MapChipController>();
        }
        if (_key == null)
        {
            GameObject obj = Resources.Load("Prefabs/Key") as GameObject;
            _key = obj;
        }
        if (_lock == null)
        {
            GameObject obj = Resources.Load("Prefabs/Lock") as GameObject;
            _lock = obj;
        }
        // 配列の大きさを設定
        _map = new GameObject[(int)_mapWidthAndHeight.y, (int)_mapWidthAndHeight.x];
        GenerateMap();
        GenerateObject();
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
        for (int heightMapChipCount = 0; heightMapChipCount < _mapWidthAndHeight.y; heightMapChipCount++)
        {
            // 一列配置する度に、次に配置するマップチップを 1 個分ずつ下にずらす
            if (heightMapChipCount != 0)
            {
                var dummyObj = _map[heightMapChipCount - 1, 0];
                mapChipPosition = new Vector3(
                    dummyObj.transform.position.x
                  , dummyObj.transform.position.y - _mapChipController.transform.localScale.y);
            }
            for (int widthMapChipCount = 0; widthMapChipCount < _mapWidthAndHeight.x; widthMapChipCount++)
            {
                // 配置する度にマップチップを 1 個分ずつ右にずらす
                if (widthMapChipCount != 0)
                    mapChipPosition += Vector3.right * _mapChipController.transform.localScale.x;
                GameObject mapChipObj = Instantiate(_mapChipController.gameObject, mapChipPosition, Quaternion.identity);
                mapChipObj.transform.SetParent(transform);
                _map[heightMapChipCount, widthMapChipCount] = mapChipObj;
            }
        }
        //_mapChipController.SetMapChipSprite();
        SpriteRenderer renderer = _map[(int)hiddenMapChip.y, (int)hiddenMapChip.x].GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    /// <summary>
    /// マップ内に存在するオブジェクトを生成
    /// </summary>
    private void GenerateObject()
    {
        Vector2 keySpawnPosition = new Vector2(2, 1);
        Vector2 lockSpawnPosition = new Vector2(_mapWidthAndHeight.y - 1, _mapWidthAndHeight.x - 2);
        _key = Instantiate(_key, _map[(int)keySpawnPosition.y, (int)keySpawnPosition.x].transform.position, Quaternion.identity);
        _lock = Instantiate(_lock, _map[(int)lockSpawnPosition.y, (int)lockSpawnPosition.x].transform.position, Quaternion.identity);
    }

    /// <summary>
    /// 配列の中のマップチップの情報を取得
    /// </summary>
    /// <param name="height">マップの列</param>
    /// <param name="width">マップの行</param>
    /// <returns>マップチップの情報</returns>
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
