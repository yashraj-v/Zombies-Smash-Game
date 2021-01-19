using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public AudioClip[] smashSounds;
    private AudioSource audioSource;

    public GameObject bloodEffect;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

  
    void Update()
    {
        //Constantly checking input here

        if(Input.GetMouseButtonDown(0))
        {
              
            //raycast here
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if(hit.collider != null)
            {
              
                if(hit.collider.tag == "Enemy")
                {
                    audioSource.PlayOneShot(smashSounds[Random.Range(0, smashSounds.Length)], 0.3f);  //0, 1, 2, 3
                    gameObject.GetComponent<GameManager>().killEnemy();
                    Camera.main.GetComponent<Animator>().SetTrigger("Shake");    //Shake after each hit
                    DisplayBloodEffect(Camera.main.ScreenToWorldPoint(Input.mousePosition));   //display bloodeffect animation
                }
            }
        } 
    }

    private void DisplayBloodEffect(Vector2 pos)   //to display blood effect at the correct position
    {
        bloodEffect.transform.position = pos;
        bloodEffect.GetComponent<Animator>().SetTrigger("Smashed");
    }
}


