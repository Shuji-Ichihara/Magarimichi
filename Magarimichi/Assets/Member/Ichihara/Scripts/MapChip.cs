using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum MapChipAttribute
{
    None = 0,
    Use = 1 << 0,
    Start = 1 << 1,
    Goal = 1 << 2,
    Key = 1 << 3,
    Lock = 1 << 4,
}

[RequireComponent(typeof(SpriteRenderer))]
public class MapChip : MonoBehaviour
{
    private SpriteRenderer _renderer = null;

    // マップチップの属性
    public MapChipAttribute MapChipAttribute { get => _MapChipAttribute; }
    private MapChipAttribute _MapChipAttribute = MapChipAttribute.Use;

    // プレイヤーが現在存在するマップチップから移動可能の方向
    public Dictionary<string, bool> CanMovePlayer => _canMovePlayer;
    private Dictionary<string, bool> _canMovePlayer = new Dictionary<string, bool>()
    {
        {"MoveUp",    false },
        {"MoveDown",  false },
        {"MoveLeft",  false },
        {"MoveRight", false },
    };

    // Start is called before the first frame update
    void Start()
    {
        // リクエストしている為、null チェックは不要
        _renderer = GetComponent<SpriteRenderer>();
        // 背景である為、一番下に配置されるようにする
        _renderer.sortingOrder = -99;
        SetUpMapChipAtrribute();
        // 空白のマップチップである場合、MapManager に情報を格納
        if (_MapChipAttribute == MapChipAttribute.None)
            MapManager.Instance.SetNoneMapChip(this);
    }

    #region SetUp
    /// <summary>
    /// マップチップの属性を設定
    /// </summary>
    private void SetUpMapChipAtrribute()
    {
        var map = MapManager.Instance.Map;
        // [0, 0] はスタート固定
        if (this == map[0, 0])
        {
            _MapChipAttribute = _MapChipAttribute | MapChipAttribute.Start;
            return;

        }
        // 右下 はゴール固定
        else if (this == map[MapManager.Instance.MapChipWidthAndHeight.y - 1, MapManager.Instance.MapChipWidthAndHeight.x - 1])
        {
            _MapChipAttribute = _MapChipAttribute | MapChipAttribute.Goal;
            return;
        }
        // 左下は欠けている
        else if (this == map[MapManager.Instance.MapChipWidthAndHeight.y - 1, 0])
        {
            _MapChipAttribute = _MapChipAttribute & ~MapChipAttribute.Use;
            return;
        }
        // 鍵のある座標は移動できない。
        else if (transform.position == MapManager.Instance.GetKeyData().transform.position)
        {
            _MapChipAttribute = _MapChipAttribute | MapChipAttribute.Key;
            return;
        }
        // 錠前は、ゴールの一つ上に存在
        else if (this == map[MapManager.Instance.MapChipWidthAndHeight.y - 2, MapManager.Instance.MapChipWidthAndHeight.x - 1])
        {
            _MapChipAttribute = _MapChipAttribute | MapChipAttribute.Lock;
            return;
        }
    }
    #endregion
    #region Setter
    public void SetMapChipSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    public void SetMapChipMaterial(Material material)
    {
        _renderer.material = material;
    }
    #endregion

    // 選択している時に、マップチップを光らせる
}
