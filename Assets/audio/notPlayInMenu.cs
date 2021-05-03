using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notPlayInMenu : MonoBehaviour
{
    AudioSource a;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<AudioSource>()!=null)
        a = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a != null)
        {
            if(pauseMenu.instance.menuIsOpened)
            {
                a.enabled = false;
            }
            else
            {
                a.enabled = true;
            }
        }

    }
}
