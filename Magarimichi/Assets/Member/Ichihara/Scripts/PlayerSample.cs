using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSample : MonoBehaviour
{
    // プレイヤーが現在いるマップチップ
    private MapChip _mapChip = null;
    // 入力キー
    private readonly KeyCode _movedUpKey = KeyCode.W;
    private readonly KeyCode _movedDownKey = KeyCode.S;
    private readonly KeyCode _movedLeftKey = KeyCode.A;
    private readonly KeyCode _movedRightKey = KeyCode.D;

    private Rigidbody2D _rb2d = null;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody 初期化
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.simulated = true;
        _rb2d.gravityScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var mapChipData = MapManager.Instance.GetMapChipData(transform.position.x, transform.position.y);
        if (mapChipData != null)
            SetMapChipData(mapChipData);
        else
            _mapChip = null;
        MovePlayer(mapChipData);
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    /// <param name="mapChip">プレイヤーが存在している座標のマップチップ</param>
    private void MovePlayer(MapChip mapChip)
    {
        if (mapChip == null)
            return;
        var mapData = MapManager.Instance.Map;
        Vector2Int mapChipIndex = mapData.GetIndex(mapChip);
        try
        {
            // 入力したキーに応じて、プレイヤーを隣接しているマップチップに移動
            if (Input.GetKeyDown(_movedUpKey) == true)
            {
                MapChip targetMapChip = mapData[mapChipIndex.x, mapChipIndex.y];
                MapChip destitaionMapChip = mapData[mapChipIndex.x - 1, mapChipIndex.y];
                // プレイヤーが現在いるマップチップと移動先のマップチップを比較する
                if (targetMapChip.CanMovePlayer[Common.MoveUp] == true && destitaionMapChip.CanMovePlayer[Common.MoveDown] == true)
                    transform.position += Vector3.up * mapChip.transform.localScale.y;
            }
            else if (Input.GetKeyDown(_movedDownKey) == true)
            {
                MapChip targetMapChip = mapData[mapChipIndex.x, mapChipIndex.y];
                MapChip destitaionMapChip = mapData[mapChipIndex.x + 1, mapChipIndex.y];
                // プレイヤーが現在いるマップチップと移動先のマップチップを比較する
                if (targetMapChip.CanMovePlayer[Common.MoveDown] == true && destitaionMapChip.CanMovePlayer[Common.MoveUp] == true)
                    transform.position += Vector3.down * mapChip.transform.localScale.y;
            }
            else if (Input.GetKeyDown(_movedLeftKey) == true)
            {
                MapChip targetMapChip = mapData[mapChipIndex.x, mapChipIndex.y];
                MapChip destitaionMapChip = mapData[mapChipIndex.x, mapChipIndex.y - 1];
                // プレイヤーが現在いるマップチップと移動先のマップチップを比較する
                if (targetMapChip.CanMovePlayer[Common.MoveLeft] == true && destitaionMapChip.CanMovePlayer[Common.MoveRight] == true)
                    transform.position += Vector3.left * mapChip.transform.localScale.x;
            }
            else if (Input.GetKeyDown(_movedRightKey) == true)
            {
                MapChip targetMapChip = mapData[mapChipIndex.x, mapChipIndex.y];
                MapChip destitaionMapChip = mapData[mapChipIndex.x, mapChipIndex.y + 1];
                // プレイヤーが現在いるマップチップと移動先のマップチップを比較する
                if (targetMapChip.CanMovePlayer[Common.MoveRight] == true && destitaionMapChip.CanMovePlayer[Common.MoveLeft] == true)
                    transform.position += Vector3.right * mapChip.transform.localScale.x;
            }
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.LogWarning($"{name} はこの先に移動できません。");
            throw;
        }
    }

    #region Setter
    /// <summary>
    /// マップチップの情報を設定する
    /// </summary>
    /// <param name="mapChip">プレイヤーが現在位置しているマップチップ</param>
    private void SetMapChipData(MapChip mapChip)
    {
        _mapChip = mapChip;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 鍵を取得
        if (other.name == MapManager.Instance.GetKeyData().name)
        {
            other.gameObject.SetActive(false);
            _mapChip.RemoveKeyAttribute();
        }
        // 錠前を開錠
        if (other.name == MapManager.Instance.GetLockData().name)
            other.gameObject.SetActive(false);
    }
}
