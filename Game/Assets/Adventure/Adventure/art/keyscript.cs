using UnityEngine;
using Cinemachine;
using System.Collections;

public class keyscript : MonoBehaviour
{
    public GameObject targetObject; // ������
    public GameObject player; // ��ɫ
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    public GameObject lockObject; // ��
    public GameObject key; // Կ��
    private Vector3 originalCameraPosition; // ��ʼ�����λ��
    private bool isTransitioning = false; // �Ƿ����ڽ��д���

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

        // �ƶ�����ͷ�ص�ԭλ��
        virtualCamera.transform.position = originalCameraPosition;

        yield return new WaitForSeconds(1);

        // ���ͽ�ɫ��Ŀ��λ��
        player.transform.position = targetPosition;

        isTransitioning = false;
    }
}
