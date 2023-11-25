using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum MapChipAttribute
{
    None = 0,
    Start = 1 << 0,
    Goal = 1 << 1,
    Up = 1 << 2,
    Down = 1 << 3,
    Left = 1 << 4,
    Right = 1 << 5,
    Lock = 1 << 6,
}

[RequireComponent(typeof(SpriteRenderer))]
public class MapChip : MonoBehaviour
{
    private SpriteRenderer _renderer = null;

    // マップチップの属性
    public MapChipAttribute MapChipAttribute { get => _mapChipAttribute; }
    private MapChipAttribute _mapChipAttribute = MapChipAttribute.None;

    // スワイプの始点と終点
    Vector3 _swipeStartPosition = Vector3.zero, _swipeEndPosition = Vector3.zero;
    // 動かすマップチップと移動先のマップチップ
    MapChip _targetMapChip = null, _destinationMapChip = null;

    // Start is called before the first frame update
    void Start()
    {
        // リクエストしている為、null チェックは不要
        _renderer = GetComponent<SpriteRenderer>();
        // 背景である為、一番下に配置されるようにする
        _renderer.sortingOrder = -99;
        SetUpMapChipAtrribute();
    }

    // Update is called once per frame
    void Update()
    {
        MoveMapChip();
    }

    private void SetUpMapChipAtrribute()
    {

    }

    /// <summary>
    /// マップチップの移動
    /// </summary>
    private void MoveMapChip()
    {
        int half = 2;
        bool isMoveMapChip = false;
        if (Input.GetMouseButtonDown(0))
        {
            var dummySwipeStartPosition = Input.mousePosition;
            _swipeStartPosition = Camera.main.ScreenToWorldPoint(dummySwipeStartPosition);
            _targetMapChip = MapManager.Instance.GetMapChipData(_swipeStartPosition.x, _swipeStartPosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            var dummySwipeEndPosition = Input.mousePosition;
            _swipeEndPosition = Camera.main.ScreenToWorldPoint(dummySwipeEndPosition);
            // 絶対値に変換
            float xDirection = Mathf.Abs(GetSwipeDirection(_swipeStartPosition.x, _swipeEndPosition.x));
            float yDirection = Mathf.Abs(GetSwipeDirection(_swipeStartPosition.y, _swipeEndPosition.y));
            // スワイプ距離があまりにも短い場合は移動しない
            if (xDirection <= MapManager.Instance.MapChip.transform.localScale.x / half
                && yDirection <= MapManager.Instance.MapChip.transform.localScale.y / half)
                return;
            _destinationMapChip = MapManager.Instance.GetMapChipData(_swipeEndPosition.x, _swipeEndPosition.y);
            isMoveMapChip = true;
        }
        if (isMoveMapChip == true)
        {
            // いずれかのマップチップの情報が null ならば移動しない
            if (_targetMapChip == null || _destinationMapChip == null)
                return;
            MapManager.Instance.ChangeMapChip(ref _targetMapChip, ref _destinationMapChip);
        }
    }

    /// <summary>
    /// スワイプした距離の長さを取得
    /// </summary>
    /// <param name="startValue">スワイプの始点</param>
    /// <param name="endValue">スワイプの終点</param>
    /// <returns>始点と終点の距離</returns>
    private float GetSwipeDirection(in float startValue, in float endValue)
    {
        return endValue - startValue;
    }

    public void SetMapChipSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    // マップチップの属性を設定
    // マップチップの移動
    // 選択している時に、マップチップを光らせる
}
