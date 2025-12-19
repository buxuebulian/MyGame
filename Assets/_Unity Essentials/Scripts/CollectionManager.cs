using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager Instance { get; private set; }

    [Header("收藏品设置")]
    public int totalCollectibles = 40; // 根据场景中的收藏品数量设置
    private int collectedCount = 0;

    [Header("完成特效")]
    public GameObject victoryVFX;
    public AudioClip victorySound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            // 如果要跨场景不销毁游戏对象，取消注释
            // DontDestroyOnLoad(this.gameObject);
        }
    }

    public void CollectItem()
    {
        collectedCount++;
        // Debug.Log($"收集进度: {collectedCount}/{totalCollectibles}");

        // 检查是否收集完成
        if (collectedCount >= totalCollectibles)
        {
            AllCollectiblesCollected();
        }
    }


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


        // 可以在这里添加其他完成事件
        // UIManager.Instance.ShowVictoryMessage();
        // GameManager.Instance.LevelComplete();
    }

    // 可选：重置进度
    public void ResetCollection()
    {
        collectedCount = 0;
    }
}