using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FollowAbove : MonoBehaviour
{
    public Transform player;  // 玩家对象的引用
    public Vector3 offset = new Vector3(0, 1, 0);  // 偏移量，使得血条在玩家正上方（UI坐标）
    private CanvasGroup canvasGroup;
    private Coroutine showCoroutine;
    private static FollowAbove instance;
    public static FollowAbove Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<FollowAbove>();
            return instance;
        }
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;  // 初始时使血条不可见
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("玩家对象未设置。请在Unity编辑器中设置玩家对象。"); //拖预制体进来
            return;
        }

        // 将血条的位置设置为玩家位置加偏移量
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position + offset);
        transform.position = screenPos;
    }

    public void Show()
    {

        // 停止之前的协程
        if (showCoroutine != null)
        {
            StopCoroutine(showCoroutine);
        }
        
        // 启动新的协程
        showCoroutine = StartCoroutine(ShowTemporarily());
    }

    private IEnumerator ShowTemporarily()
    {
        canvasGroup.alpha = 1;  // 显示血条
        yield return new WaitForSeconds(1);  // 保持显示1秒钟

        // 在后一秒内逐渐减少alpha值
        float elapsedTime = 0f;
        float fadeDuration = 1f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1 - (elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;  // 确保最后隐藏血条
    }
    }
