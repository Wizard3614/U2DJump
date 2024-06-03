using UnityEngine;

public class JumpBooster : MonoBehaviour
{
    public int newMaxJumpCount = 3; // 新的最大跳跃次数

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Playcontroller player = collision.GetComponent<Playcontroller>();
        if (player != null)
        {
            player.IncreaseMaxJumpCount(newMaxJumpCount);
            Destroy(gameObject); // 使物体消失
        }
    }
}
