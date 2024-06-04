using UnityEngine;
using Cinemachine;
using System.Collections;

public class keyscript : MonoBehaviour
{
    public GameObject targetObject; // 空物体
    public GameObject player; // 角色
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    public GameObject lockObject; // 锁
    public GameObject key; // 钥匙
    private Vector3 originalCameraPosition; // 初始摄像机位置
    private bool isTransitioning = false; // 是否正在进行传送

    void Start()
    {
            originalCameraPosition = virtualCamera.transform.position; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !isTransitioning)
        {
            Destroy(lockObject);
            StartCoroutine(HandleCollision());
        }
    }

    IEnumerator HandleCollision()
    {
        isTransitioning = true;
        Vector3 targetPosition = targetObject.transform.position;

        // 移动摄像头回到原位置
        virtualCamera.transform.position = originalCameraPosition;

        yield return new WaitForSeconds(1);

        // 传送角色到目标位置
        player.transform.position = targetPosition;

        isTransitioning = false;
    }
}
