using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum MapChipAttribute
{
    None  = 0,
    Start = 1 << 0,
    Goal  = 1 << 1,
    Up    = 1 << 2,
    Down  = 1 << 3,
    Left  = 1 << 4,
    Right = 1 << 5,
    Lock  = 1 << 6,
}

[RequireComponent(typeof(SpriteRenderer))]
public class MapChipController : MonoBehaviour
{
    private SpriteRenderer _renderer = null;

    // このマップチップの属性
    public MapChipAttribute MapChipAttribute => _mapChipAttribute;
    private MapChipAttribute _mapChipAttribute = MapChipAttribute.None;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetUpMapChipAtrribute()
    {
        Sprite sprite = _renderer.sprite;
    }

    public void SetMapChipSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    // マップチップの属性を設定
    // マップチップの移動
    // 選択している時に、マップチップを光らせる
}
