using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    #region Refarences
    // 配置するマップチップ
    public MapChip MapChip => _mapChip;
    [SerializeField]
    private MapChip _mapChip = null;
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
    private Vector2Int _mapWidthAndHeight = new Vector2Int(4, 4);
    #endregion

    // マップチップ配列
    // 配列のイメージは、[横のマップチップ, 縦のマップチップ]です。
    private MapChip[,] _map = { };

    new private void Awake()
    {
        // null チェック
        if (_mapChip == null)
        {
            GameObject obj = Resources.Load("Prefabs/MapChip") as GameObject;
            _mapChip = obj.GetComponent<MapChip>();
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
        _map = new MapChip[_mapWidthAndHeight.y, _mapWidthAndHeight.x];
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
        Vector2Int hiddenMapChip = new Vector2Int(_mapWidthAndHeight.y - 1, 0);
        // _map[0,0] の座標
        Vector3 mapChipPosition = new Vector3(
            -(_mapChip.transform.localScale.x * _mapWidthAndHeight.x / half) + _mapChip.transform.localScale.x / half
           , (_mapChip.transform.localScale.y * _mapWidthAndHeight.y / half) - _mapChip.transform.localScale.y / half);
        for (int heightMapChipCount = 0; heightMapChipCount < _mapWidthAndHeight.y; heightMapChipCount++)
        {
            // 一列配置する度に、次に配置するマップチップを 1 個分ずつ下にずらす
            if (heightMapChipCount != 0)
            {
                var dummyObj = _map[heightMapChipCount - 1, 0];
                mapChipPosition = new Vector3(
                    dummyObj.transform.position.x
                  , dummyObj.transform.position.y - _mapChip.transform.localScale.y);
            }
            for (int widthMapChipCount = 0; widthMapChipCount < _mapWidthAndHeight.x; widthMapChipCount++)
            {
                // 配置する度にマップチップを 1 個分ずつ右にずらす
                if (widthMapChipCount != 0)
                    mapChipPosition += Vector3.right * _mapChip.transform.localScale.x;
                GameObject mapChipObj = Instantiate(_mapChip.gameObject, mapChipPosition, Quaternion.identity);
                mapChipObj.transform.SetParent(transform);
                _map[heightMapChipCount, widthMapChipCount] = mapChipObj.GetComponent<MapChip>();
            }
        }
        // TODO
        //_mapChipController.SetMapChipSprite();
        // マップチップを隠す
        SpriteRenderer renderer = _map[hiddenMapChip.x, hiddenMapChip.y].GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    /// <summary>
    /// マップ内に存在するオブジェクトを生成
    /// </summary>
    private void GenerateObject()
    {
        // 鍵が生成されるマップチップの座標
        Vector2Int keySpawnPosition = new Vector2Int(
            Random.Range(1, _mapWidthAndHeight.x - 1), Random.Range(1, _mapWidthAndHeight.y - 1));
        // 錠前が生成されるマップチップの座標
        Vector2Int lockSpawnPosition = new Vector2Int(_mapWidthAndHeight.x - 1, _mapWidthAndHeight.y - 2);
        // 生成
        _key = Instantiate(_key, _map[keySpawnPosition.y, keySpawnPosition.x].transform.position, Quaternion.identity);
        _lock = Instantiate(_lock, _map[lockSpawnPosition.y, lockSpawnPosition.x].transform.position, Quaternion.identity);
    }

    /// <summary>
    /// マップチップを入れ替える
    /// </summary>
    /// <param name="targetMapChip">入れ替えたいマップチップ</param>
    /// <param name="destinationMapChip">入れ替える先のマップチップ</param>
    public void ChangeMapChip(ref MapChip targetMapChip, ref MapChip destinationMapChip)
    {
        Vector3 targetPosition = targetMapChip.transform.position, destinationPosition = destinationMapChip.transform.position;
        MapChip dummyTargetMapChip = targetMapChip, dummyDestinationMapChip = destinationMapChip, dummyMapChip = null;
        //var index = _map.GetIndex(dummyTargetMapChip);
        //Debug.Log(index);
        targetMapChip.transform.position = destinationPosition;
        destinationMapChip.transform.position = targetPosition;
    }

    #region Getter
    /// <summary>
    /// 配列の中のマップチップの情報を添え字から取得
    /// </summary>
    /// <param name="width">マップの行</param>
    /// <param name="height">マップの列</param>
    /// <returns>マップチップの情報</returns>
    public MapChip GetMapChipData(int width, int height)
    {
        // 配列外参照を防ぐ
        if (width >= _mapWidthAndHeight.x || height >= _mapWidthAndHeight.y)
        {
            Debug.LogError($"{_map[width, height].name}の情報が取得できません。");
            return null;
        }
#if UNITY_EDITOR
        Debug.Log($"今回取得したマップチップは、{_map[width, height].name} です。");
#endif
        return _map[width, height];
    }

    /// <summary>
    /// 配列の中のマップチップの情報を X 座標、Y 座標から取得
    /// </summary>
    /// <param name="xPosition">指定した X 座標</param>
    /// <param name="yPosition">指定した Y 座標</param>
    /// <returns>マップチップの情報</returns>
    public MapChip GetMapChipData(float xPosition, float yPosition)
    {
        int half = 2;
        MapChip mapChip = null;
        foreach (var dummyMapChip in _map)
        {
            float xScale = dummyMapChip.transform.localScale.x;
            float yScale = dummyMapChip.transform.localScale.y;
            Vector3 dummyMapChipPosition = dummyMapChip.transform.position;
            // 引数の値を比較し、条件に合致するマップチップを特定、代入する
            if (xPosition >= dummyMapChipPosition.x - xScale / half && xPosition <= dummyMapChipPosition.x + xScale / half)
            {
                Debug.Log($"xPos = {xPosition >= dummyMapChipPosition.x - xScale / half && xPosition <= dummyMapChipPosition.x + xScale / half}, " +
                    $"yPos = {yPosition >= dummyMapChipPosition.y - yScale / half && yPosition <= dummyMapChipPosition.y + yScale / half}");
                if (yPosition >= dummyMapChipPosition.y - yScale / half && yPosition <= dummyMapChipPosition.y + yScale / half)
                {
                    mapChip = dummyMapChip;
                    break;
                }
            }
        }
        if (mapChip == null)
        {
            //Debug.LogError($"{new Vector2(xPosition, yPosition)}にあるマップチップの情報を取得できませんでした。");
            return null;
        }
#if UNITY_EDITOR
        //Debug.Log($"今回取得したマップチップは、{mapChip.name} です。");
#endif
        return mapChip;
    }
    #endregion
}
