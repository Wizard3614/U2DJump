using UnityEngine;

public class JumpBooster : MonoBehaviour
{
    public int newMaxJumpCount = 3; // �µ������Ծ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Playcontroller player = collision.GetComponent<Playcontroller>();
        if (player != null)
        {
            player.IncreaseMaxJumpCount(newMaxJumpCount);
            Destroy(gameObject); // ʹ������ʧ
        }
    }
}
