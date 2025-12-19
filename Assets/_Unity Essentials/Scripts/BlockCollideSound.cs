using UnityEngine;

public class BlockCollideSound : MonoBehaviour
{
    public AudioClip collideSound;        // 在Inspector中分配方块掉落音效
    private AudioSource audioSource;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 获取 Rigidbody 组件
        rb = GetComponent<Rigidbody>();

        // 配置 AudioSource 组件
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f;  //3D音效
    }

    void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞是否强烈
        if (collision.relativeVelocity.magnitude > 1.0f)
        {
            PlayCollideSound(collision.relativeVelocity.magnitude);
        }

    }

    void PlayCollideSound(float impactForce)
    {
        if (collideSound != null && audioSource != null)
        {
            // 根据撞击力度限制音量
            float volume = Mathf.Clamp(impactForce / 10.0f, 0.1f, 1.0f);
            audioSource.volume = volume;

            // 轻微随机化音调增加真实感
            audioSource.pitch = Random.Range(0.8f, 0.95f);

            audioSource.PlayOneShot(collideSound);
        }
    }
}
