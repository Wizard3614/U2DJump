using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class StartSceneLevel : MonoBehaviour
{
    public GameObject subPanel;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public AudioClip clickSound;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialPosition = subPanel.transform.localPosition;
        targetPosition = new Vector3(0f, 0f, 0f);
    }

    public void MoveSubPanelUp()
    {
        PlayClickSound();
        StartCoroutine(MoveSubPanel(targetPosition));
    }

    public void MoveSubPanelBack()
    {   
        PlayClickSound();
        StartCoroutine(MoveSubPanel(initialPosition));
    }

    private IEnumerator MoveSubPanel(Vector3 destination)
    {
        subPanel.GetComponent<RectTransform>().DOAnchorPos(destination, 0.5f).SetEase(Ease.OutQuad);
        yield return null;
        subPanel.transform.position = destination;
    }
    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
    
}
