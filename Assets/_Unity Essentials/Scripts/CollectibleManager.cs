using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance { get; private set; }

    private int currentCount = 0;

    public System.Action<int> OnCollectibleCountChanged;

    [Header("完成特效")]
    public GameObject victoryVFX;
    public AudioClip victorySound;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // 如果要跨场景不销毁游戏对象，取消注释
            //DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    public void RegisterCollectible()
    {
        currentCount++;
        // 检查是否收集完成
        OnCollectibleCountChanged?.Invoke(currentCount);
    }
    public void UnregisterCollectible()
    {
        currentCount = Mathf.Max(currentCount - 1, 0);
        OnCollectibleCountChanged?.Invoke(currentCount);
        if (currentCount == 0)
        {
            AllCollectiblesCollected();
        }
    }
    public int GetCurrentCount() => currentCount;


    private void AllCollectiblesCollected()
    {
        // Debug.Log("所有收藏品收集完成！");

        // 获取玩家位置
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 spawnPosition = player != null ? player.transform.position : Vector3.up;

        // 在玩家位置生成特效
        if (victoryVFX != null)
        {
            Instantiate(victoryVFX, spawnPosition, Quaternion.LookRotation(Vector3.up));
        }

        // ... 其他代码 ...

        if (victorySound != null)
        {
            AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position);
        }


        //     // 可以在这里添加其他完成事件
        //     // UIManager.Instance.ShowVictoryMessage();
        //     // GameManager.Instance.LevelComplete();
    }

    // 可选：重置进度
    //     public void ResetCollection()
    //     {
    //         currentCount = 0;
    //     }
}