using UnityEngine;
using TMPro;

public class TextFloat : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;  // 使用 TextMeshPro
    public float floatRange = 8f; // 上下浮动的范围
    public float floatSpeed = 5f; // 浮动速度

    private Vector3 startPos;

    void Start()
    {
        startPos = textMeshPro.transform.position;
    }

    void Update()
    {
        // 使用正弦波计算新的Y位置，实现平滑变速运动
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatRange;
        textMeshPro.transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}