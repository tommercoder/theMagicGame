using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questPointer : MonoBehaviour
{
    public static questPointer instance;
    private void Awake()
    {
        instance = this;
    }


    public Image img;
    public Transform target;
    public Text meter;
   
    public Vector3 offset;
    private void Update()
    {
        if (target != null)
        {
            float minX = img.GetPixelAdjustedRect().width / 2;

            float maxX = Screen.width - minX;


            float minY = img.GetPixelAdjustedRect().height / 2;

            float maxY = Screen.height - minY;


            Vector2 pos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(target.position + offset);


            if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
            {

                if (pos.x < Screen.width / 2)
                {

                    pos.x = maxX;
                }
                else
                {

                    pos.x = minX;
                }
            }


            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);


            img.transform.position = pos;

            meter.text = ((int)Vector3.Distance(target.position, MarieleQuest.instance.transform.position)).ToString() + "m";
        }
    }
}
