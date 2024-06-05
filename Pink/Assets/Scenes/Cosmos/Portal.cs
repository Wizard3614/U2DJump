using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject destination; // 目标位置的GameObject

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 如果进入触发器的是Player标签的对象
        {
            collision.transform.position = destination.transform.position; // 将Player传送到目标位置
        }
    }
}
