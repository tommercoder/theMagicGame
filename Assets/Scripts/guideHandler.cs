using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guideHandler : MonoBehaviour
{
    public void openGuide()
    {
        FindObjectOfType<audioManager>().Play("menuClick");
    }
}
