using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct TransformationInit
{
    public MyVector3 Translation;
    public MyVector3 Rotation;
    public MyVector3 Scale;

}



//My transformation component to handle Positon, Rotation and Movement
public class myTransformation : MonoBehaviour
{
    Vector3[] ModelSpaceVertices;
    //  List<Vector3[]> ChildModelSpaceVertices = new List<Vector3[]>();
    //  public MeshFilter[] ChildObjects;
    public BoundingObject BoundObject;
    public MyVector3 Translation, ChildTranslation;
    public MyVector3 Rotation, ChildRotation;
    public MyVector3 Scale, ChildScale;
    public MyVector3 ModelMinExtent, ModelMaxExtent;
    Quat QuatRotation;
    Matrix4B4 RotationMatrix;
    public void Initialise(TransformationInit Values)
    {
        Translation = Values.Translation;
        Rotation = Values.Rotation;
        Scale = Values.Scale;
    }

    public myTransformation()
    {
        this.Translation = new MyVector3(0, 0, 0);
        this.Scale = new MyVector3(1, 1, 1);
        this.Rotation = new MyVector3(0, 0, 0);
    }
    public myTransformation(MyVector3 Position, MyVector3 scale, MyVector3 rotation)
    {
        this.Translation = Position;
        this.Scale = scale;
        this.Rotation = rotation;
    }

    void Start()
    {
        BoundObject = new AABB(new MyVector3(-1, -1, -1), new MyVector3(1, 1, 1));
        ModelMinExtent = new MyVector3(10000, 10000, 10000);
        ModelMaxExtent = new MyVector3(-10000, -10000, -10000);

        RotationMatrix = Matrix4B4.Identiy;
        MeshFilter MF = GetComponent<MeshFilter>();
        ModelSpaceVertices = MF.mesh.vertices;

        //   ChildModelSpaceVertices = new List<Vector3[]>();
        //   if (ChildObjects.Length > 0)
        //   foreach (MeshFilter mf in ChildObjects)
        //       ChildModelSpaceVertices.Add(mf.mesh.vertices);
        ////   Scale = new MyVector3(1, 1, 1);

    }
    public Quat GetRotation()
    {
        MyVector3 RotationInRadians = new MyVector3();
        RotationInRadians.x = VectorMaths.Deg2Rad(Rotation.x);
        RotationInRadians.y = VectorMaths.Deg2Rad(Rotation.y);
        RotationInRadians.z = VectorMaths.Deg2Rad(Rotation.z);
        Quat Ret = Quat.EulerToQuat(RotationInRadians);
        return Ret;
    }

    // Update is called once per frame
    public void SetRotation(Quat LHS)
    {
        Rotation = Quat.QuatToEuler(LHS);
    }

    void Update()
    {
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
        Matrix4B4 M = scaleMatrix * RotationMatrix * translationMatrix;
        for (int i = 0; i < TransformedVertices.Length; i++)
        {
            TransformedVertices[i] = M * ModelSpaceVertices[i];
        }


        for (int i = 0; i < ModelSpaceVertices.Length; i++)
        {
            if (ModelSpaceVertices[i].x < ModelMinExtent.x)
            {
                ModelMinExtent.x = ModelSpaceVertices[i].x;
            }
            if (ModelSpaceVertices[i].y < ModelMinExtent.y)
            {
                ModelMinExtent.y = ModelSpaceVertices[i].y;
            }
            if (ModelSpaceVertices[i].z < ModelMinExtent.z)
            {
                ModelMinExtent.z = ModelSpaceVertices[i].z;
            }


            if (ModelSpaceVertices[i].x > ModelMaxExtent.x)
            {
                ModelMaxExtent.x = ModelSpaceVertices[i].x;
            }
            if (ModelSpaceVertices[i].y > ModelMaxExtent.y)
            {
                ModelMaxExtent.y = ModelSpaceVertices[i].y;
            }
            if (ModelSpaceVertices[i].z > ModelMaxExtent.z)
            {
                ModelMaxExtent.z = ModelSpaceVertices[i].z;
            }
        }

        MeshFilter MF = GetComponent<MeshFilter>();
        MF.mesh.vertices = TransformedVertices;
        MF.mesh.RecalculateNormals();
        MF.mesh.RecalculateBounds();

        //    if(ChildModelSpaceVertices.Count >0)
        //    for (int j = 0; j < ChildModelSpaceVertices.Count; j++)
        //    {
        //        Vector3[] TransformedChildVertices = new Vector3[ChildModelSpaceVertices[j].Length];
        //        for (int i = 0; i < TransformedChildVertices.Length; i++)
        //        {
        //            Vector4 vertex = ChildModelSpaceVertices[j][i];
        //            vertex.w = 1;
        //            Matrix4B4 CscaleMatrix = new Matrix4B4(
        //new MyVector3(ChildScale.x, 0, 0),
        //new MyVector3(0, ChildScale.y, 0),
        //new MyVector3(0, 0, ChildScale.z),
        //MyVector3.zero
        //);
        //            //Translation
        //            Matrix4B4 CtranslationMatrix = new Matrix4B4(
        //            new MyVector3(1, 0, 0),
        //            new MyVector3(0, 1, 0),
        //            new MyVector3(0, 0, 1),
        //            new MyVector3(ChildTranslation.x, ChildTranslation.y, ChildTranslation.z)
        //            );
        //            //Rotation
        //            RotationInRadians = new MyVector3();
        //            RotationInRadians.x = VectorMaths.Deg2Rad(ChildRotation.x);
        //            RotationInRadians.y = VectorMaths.Deg2Rad(ChildRotation.y);
        //            RotationInRadians.z = VectorMaths.Deg2Rad(ChildRotation.z);
        //            QuatRotation = Quat.EulerToQuat(RotationInRadians);
        //            Matrix4B4 CRotationMatrix = Matrix4B4.QuatToMatrix(QuatRotation);
        //            Matrix4B4 ChildMatrix = CscaleMatrix * CRotationMatrix * CtranslationMatrix;



        //            TransformedChildVertices[i] = M * ChildMatrix * vertex;
        //        }
        //        ChildObjects[j].mesh.vertices = TransformedChildVertices;
        //        ChildObjects[j].mesh.RecalculateNormals();
        //        ChildObjects[j].mesh.RecalculateBounds();
    }
}
