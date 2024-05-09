using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageAmount = 1f;

    void OnTriggerStay2D(Collider2D other)
    {
        // 检查碰撞的物体是否是玩家
        Playcontroller playcontroller = other.GetComponent<Playcontroller>();
        if (playcontroller != null)
        {
            PlayerStats.Instance.TakeDamage(damageAmount);
        }
    }
}

