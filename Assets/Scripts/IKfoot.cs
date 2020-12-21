using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKfoot : MonoBehaviour
{
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {
      
    }
}
