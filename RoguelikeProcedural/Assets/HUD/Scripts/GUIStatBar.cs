using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIStatBar : MonoBehaviour
{
    public PlayerStats stats;

    private float maxSize;
    private Vector3 adjustedSize;

    [SerializeField] private Gradient gradient;
    private Material material;



    void Start()
    {
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();

        material = GetComponent<Image>().material;

        maxSize = GetComponent<Transform>().localScale.x;
    }

    void Update()
    {
        transform.localScale = AdjustSize(transform.localScale);

        material.color = AdjustColor();
    }

    private Color AdjustColor()
    {
        return gradient.Evaluate(stats.health / stats.MaxHealth.Value);
    }

    private Vector3 AdjustSize(Vector3 vector3)
    {
        vector3.x = maxSize * (stats.health / stats.MaxHealth.Value);
        return vector3;
    }
}
