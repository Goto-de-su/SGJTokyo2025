using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<PlayerController> players;
    public List<PlayerController> Players => players;

    [SerializeField] private int winScore;
    public int WinScore => winScore;

    /// <summary>
    /// シングルトンの実装
    /// </summary>
    private static GameManager _instance;

    // 遅延初期化とインスタンスの取得・生成ロジックを含む
    [System.Obsolete]
    public static GameManager Instance
    {
        get
        {
            // インスタンスがまだ設定されていない場合
            if (_instance == null)
            {
                // シーン内から既存のインスタンスを探す
                _instance = FindObjectOfType<GameManager>();

                // それでも見つからない場合は、新しいGameObjectを作成してコンポーネントを追加する
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(GameManager).Name);
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    // 必要に応じて、以下のOnDestroyメソッドを追加し、
    // シングルトンインスタンスが破棄されたときにnullに設定する
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}