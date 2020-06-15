 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    float length, startpos;
    Camera cam;
    public float parallaxEffect;
    public float bgWidth;
    public int moveLength;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        startpos = transform.position.x;
        // length = 20.8f;
        // length = 9.28f * 2;
        length = bgWidth * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        if (temp > startpos + length * moveLength) startpos += length;

        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}
