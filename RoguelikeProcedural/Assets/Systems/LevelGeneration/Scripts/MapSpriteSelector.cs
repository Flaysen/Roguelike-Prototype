using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour
{

    public GameObject spU, spD, spR, spL,
            spUD, spRL, spUR, spUL, spDR, spDL,
            spULD, spRUL, spDRU, spLDR, spUDRL;
    public bool up, down, left, right;
    public int type; // 0: normal, 1: enter
    public Color normalColor, enterColor;
    Color mainColor;
    SpriteRenderer rend;
    void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
        //mainColor = normalColor;
        PickSprite();
        //PickColor();
    }
    void PickSprite()
    { //picks correct sprite based on the four door bools
        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        Instantiate(spUDRL, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(spDRU, transform.position, Quaternion.identity);
                    }
                }
                else if (left)
                {
                    Instantiate(spULD, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(spUD, transform.position, Quaternion.identity);
                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        Instantiate(spRUL, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(spUR, transform.position, Quaternion.identity);
                    }
                }
                else if (left)
                {
                    Instantiate(spUL, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(spU, transform.position, Quaternion.identity);
                }
            }
            return;
        }
        if (down)
        {
            if (right)
            {
                if (left)
                {
                    Instantiate(spLDR, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(spDR, transform.position, Quaternion.identity);
                }
            }
            else if (left)
            {
                Instantiate(spDL, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(spD, transform.position, Quaternion.identity);
            }
            return;
        }
        if (right)
        {
            if (left)
            {
                Instantiate(spRL, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(spR, transform.position, Quaternion.identity);
            }
        }
        else
        {
            Instantiate(spL, transform.position, Quaternion.identity);
        }
    }

    void PickColor()
    { //changes color based on what type the room is
        if (type == 0)
        {
            mainColor = normalColor;
        }
        else if (type == 1)
        {
            mainColor = enterColor;
        }
        rend.color = mainColor;
    }
}