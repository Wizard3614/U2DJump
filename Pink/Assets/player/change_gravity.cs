using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_gravity : MonoBehaviour
{
    public GameObject obj;
    public Rigidbody2D  rb;
    public bool isplayer = false;

    public float jump ;

  
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isplayer &&Input.GetKeyUp(KeyCode.E))
        {
          
            print ("1");
           
            playercontroller();
            

        }
    }

   private void OnTriggerEnter2D(Collider2D other) 
    
    {
        if(other.CompareTag("Player"))
        {
        isplayer = true;
        }
    }

 private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
           isplayer = false;
        }
    }

    public void playercontroller()
    {
        //  rb.gravityScale = -1;
        //  obj.transform.localScale = new Vector3(4,-4,4);
        rb.AddForce(new Vector2(0f,jump),ForceMode2D.Impulse);

       
    }
}
