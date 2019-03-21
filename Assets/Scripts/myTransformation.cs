using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
class Test : System.Object
{
    public float x = 0;
    public float y = 0;
    public float z = 0;
}

public struct TransformationInit
{
    public float Head, Feat;
    public MyVector3 Translation;
    public MyVector3 Rotation;
    public MyVector3 Scale;

}

//My transformation component to handle Positon, Rotation and Movement
public class myTransformation : MonoBehaviour
{
    Vector3[] ModelSpaceVertices;

    public BoundingObject BoundObject;
    public MyVector3 Translation;
    public MyVector3 Rotation;
    public MyVector3 Scale;

    Matrix4B4 M;
    Quat QuatRotation;
    Matrix4B4 RotationMatrix;
    public void Initialise (TransformationInit Values)
    {
        Translation = Values.Translation;
        Rotation = Values.Rotation;
        Scale = Values.Scale;
    }

    public myTransformation()
    {
        this.Translation = new MyVector3(0,0,0);
        this.Scale = new MyVector3(1, 1, 1);
        this.Rotation = new MyVector3(0,0,0);
    }
    public myTransformation(MyVector3 Position, MyVector3 scale, MyVector3 rotation)
    {
        this.Translation = Position;
        this.Scale = scale;
        this.Rotation=rotation;
    }
    //public myTransformation(MyVector3 Position, MyVector3 scale, Quat rotation)
    //{
    //    this.Translation = Position;
    //    this.Scale = scale;
    //    this.Rotation = rotation;
    //}


    void Start()
    {
      
 
       
        RotationMatrix = Matrix4B4.Identiy;
        MeshFilter MF = GetComponent<MeshFilter>();
        ModelSpaceVertices = MF.mesh.vertices;
     //   Scale = new MyVector3(1, 1, 1);

    }


    // Update is called once per frame
    void SetQuat(Vector4 lhs)
    {
        QuatRotation.x = lhs.x;

        //CONVERSION PROCESS
    }
    void Update () {
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];
        //Scale
        Matrix4B4 scaleMatrix = new Matrix4B4(
            new MyVector3(Scale.x, 0, 0),
            new MyVector3(0, Scale.y, 0),
            new MyVector3(0, 0, Scale.z),
            MyVector3.zero
            );
        //Translation
        Matrix4B4 translationMatrix = new Matrix4B4(
        new MyVector3(1, 0, 0),
        new MyVector3(0, 1, 0),
        new MyVector3(0, 0, 1),
        new MyVector3(Translation.x, Translation.y, Translation.z)
        );
        //Rotation
         MyVector3 RotationInRadians = new MyVector3();
        RotationInRadians.x = VectorMaths.Deg2Rad(Rotation.x);
        RotationInRadians.y = VectorMaths.Deg2Rad(Rotation.y);
        RotationInRadians.z = VectorMaths.Deg2Rad(Rotation.z);
        QuatRotation = Quat.EulerToQuat(RotationInRadians);
        RotationMatrix = Matrix4B4.QuatToMatrix(QuatRotation);
        Matrix4B4 M = scaleMatrix * RotationMatrix *translationMatrix;
        for (int i = 0; i < TransformedVertices.Length; i++)
        {
            TransformedVertices[i] = M * ModelSpaceVertices[i];
        }
       
        MeshFilter MF = GetComponent<MeshFilter>();
        MF.mesh.vertices = TransformedVertices;
        MF.mesh.RecalculateNormals();
        MF.mesh.RecalculateBounds();
    }
}
