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
            //wylicza powołę zdjęcia dlatego żeby zdjęcie pokazywało się na ekranie całkowicie i nie odcięło się go 75%
            float minx = img.GetPixelAdjustedRect().width / 2;
            float maxx = Screen.width - minx;
            float minY = img.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;

            //przerobienie punktu 3d to punktu 2d z widoku kamery
            Vector2 pos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(target.position + offset);
            //jeśli tylko kamera patrzy w przód ,zróci "+1" jeśli target jest z przodu i "-1" w innym przypadku i 0 w przypadku jeśli one są jeden obok drugiego
            if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
            {
                //target is behind player
                if (pos.x < Screen.width / 2)
                {
                    //left size of screen wstawiamy go w prawo
                    pos.x = maxx;
                }
                else
                {
                    //
                    pos.x = minx;
                }
            }
            //granice dla zdjęcia na ekranie żeby go się nie odcięło
            pos.x = Mathf.Clamp(pos.x, minx, maxx);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            img.transform.position = pos;
            meter.text = ((int)Vector3.Distance(target.position, MarieleQuest.instance.transform.position)).ToString() + "m";
        }
    }
}
