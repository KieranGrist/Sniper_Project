﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BoxUpdater : MonoBehaviour {
    public MyVector3 MinExtent;
    public MyVector3 MaxExtent;
     public MyVector3 Centre;
     public MyVector3 Half;
    //  Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        myTransformation TempoaryTransformation = GetComponent<myTransformation>();


        MinExtent = TempoaryTransformation.Translation + (TempoaryTransformation.Scale * TempoaryTransformation.ModelMinExtent);
        MaxExtent = TempoaryTransformation.Translation + (TempoaryTransformation.Scale * TempoaryTransformation.ModelMaxExtent);
        TempoaryTransformation.BoundObject = new AABB(MinExtent,MaxExtent);
        AABB Box1 = TempoaryTransformation.BoundObject as AABB;
        Box1.Half = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        Box1.Centre = Box1.Half + Box1.MinExtent;
        MinExtent = Box1.MinExtent;
        MaxExtent = Box1.MaxExtent;
        Centre = Box1.Centre;
        Half = Box1.Half;
    }
}