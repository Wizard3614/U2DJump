using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public enum Direction { UpDown, LeftRight }
    public Direction movementDirection = Direction.UpDown; // 默认方向为上下
    public float distance = 5.0f; // 要移动的距离
    public float duration = 2.0f; // 每次移动的持续时间

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        // 初始化起始位置
        startPosition = transform.position;

        // 根据选择的方向计算目标位置
        if (movementDirection == Direction.UpDown)
        {
            targetPosition = startPosition - new Vector3(0, distance, 0);
        }
        else if (movementDirection == Direction.LeftRight)
        {
            targetPosition = startPosition - new Vector3(distance, 0, 0);
        }

        // 开始协程
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            // 向目标位置移动
            yield return StartCoroutine(MoveToPosition(targetPosition));
            // 移动回起始位置
            yield return StartCoroutine(MoveToPosition(startPosition));
        }
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保物体完全移动到目标位置
        transform.position = target;
    }
}

