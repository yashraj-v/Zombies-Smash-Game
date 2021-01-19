using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] musicGameObjects = GameObject.FindGameObjectsWithTag("music");   //Give your music file the tag "music"

        if(musicGameObjects.Length>1)
        {
            Destroy(this.gameObject);    //So that the music doesnt overlap itself
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
