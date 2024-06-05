using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip music;
    public GameObject effect;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        ProManager manager = other.GetComponent<ProManager>();
        if(manager)
        {
            bool pickUp=manager.PickupItem(gameObject);
            if(pickUp)
            {
                FollowAbove.Instance.Show();
                PlayerStats.Instance.Heal(0.5f);
                RemoveItem();
            }
        }
    }

    private void RemoveItem()
    {
        Destroy(gameObject);
    }
}
