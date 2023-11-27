using UnityEngine;

/// <summary>
/// 拡張メソッドを定義するクラス
/// </summary>
public static class Extensions
{
    /// <summary>
    /// 二次元配列の Index を取得する
    /// </summary>
    /// <typeparam name="T">ジェネリック化</typeparam>
    /// <param name="array"> element が格納されている配列</param>
    /// <param name="element">Index を取得したい要素</param>
    /// <returns>element が存在する配列の Index (二次元配列の Index を返す為、Vector2Int を 2 つの Index のまとまりとして返す)</returns>
    public static Vector2Int GetIndex<T>(this T[,] array, T element) where T : MapChip
    {
        int line = 0, column = 0;
        for (int i = 0; i < array.GetLength(1); i++)
        {
            for (int j = 0; j < array.GetLength(0); j++)
            {
                if (array[j, i] == element)
                {
                    line = j;
                    column = i;
                }
            }
        }
        return new Vector2Int(line, column);
    }
}