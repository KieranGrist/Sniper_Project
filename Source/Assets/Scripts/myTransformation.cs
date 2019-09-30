using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
[System.Serializable]
public struct TransformationInit
{
    public MyVector3 Translation;  //Position of Object
    public MyVector3 Rotation;  //Rotation of object
    public MyVector3 Scale;  //Scale of object
}

//My transformation component to handle Positon, Rotation and Movement
public class myTransformation : MonoBehaviour
{
    Vector3[] ModelSpaceVertices;   //Model
    public BoundingObject BoundObject;  //Collision object
    public MyVector3 Translation;  //Position of Object
    public MyVector3 Rotation;  //Rotation of object
    public MyVector3 Scale;  //Scale of object
    public MyVector3 ModelMinExtent;  //Min extent of model used for collision
    public MyVector3 ModelMaxExtent;  //Max extent of model used for collision
    Quat QuatRotation;  //Quationion rotation 
    Matrix4B4 RotationMatrix;  //Rotation matrix global due to Get and Set rotation calls
    public void Initialise(TransformationInit Values)
    {
        //Set values
        Translation = Values.Translation;  
        Rotation = Values.Rotation;  
        Scale = Values.Scale;  
    }

    public myTransformation()
    {
        //Create default values
        this.Translation = new MyVector3(0, 0, 0);  
        this.Scale = new MyVector3(1, 1, 1);  
        this.Rotation = new MyVector3(0, 0, 0);  
    }
    public myTransformation(MyVector3 Position, MyVector3 scale, MyVector3 rotation)
    {
        //Set values
        this.Translation = Position;  
        this.Scale = scale;  
        this.Rotation = rotation;   
    }

    void Start()
    {
        //Set Model Min and max extents 
        ModelMinExtent = new MyVector3(10000, 10000, 10000);   //Sets Model Min to High values, this is done to make sure values are accurate in loop
        ModelMaxExtent = new MyVector3(-10000, -10000, -10000);  //Sets Model Max to low values, this is done to make sure values are accurate in loop
        //Get the model information
        MeshFilter MF = GetComponent<MeshFilter>();  
        ModelSpaceVertices = MF.mesh.vertices;  
    }
    public Quat GetRotation()
    {
        //Convert Rotation to radians
        MyVector3 RotationInRadians = new MyVector3();  //Creates Rotation in radians
        RotationInRadians.x = VectorMaths.Deg2Rad(Rotation.x);  //Converts Rotation x to Radians
        RotationInRadians.y = VectorMaths.Deg2Rad(Rotation.y);  //Converts Rotation y to Radians
        RotationInRadians.z = VectorMaths.Deg2Rad(Rotation.z);    //Converts Rotation z to Radians

        //Convert Rotation to Quat
        Quat RET = Quat.EulerToQuat(RotationInRadians);  

        //Return rotation
        return RET; //Create RET  
    }

    // Update is called once per frame
    public void SetRotation(Quat LHS)
    {
        Rotation = Quat.QuatToEuler(LHS);   //Sets rotation by converting a quat to an euler
    }

    void Update()
    {
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];   //Creates Transformed Vertices And sets array length to stop memory leak issues
        //Scale MAtrix created by setting scale diagonaly down a matrix 
        Matrix4B4 scaleMatrix = new Matrix4B4(
            new MyVector3(Scale.x, 0, 0),
            new MyVector3(0, Scale.y, 0),
            new MyVector3(0, 0, Scale.z),
            MyVector3.zero
            );  
        //Translation Matrix, created by using an identity matrix for first 3 rows then set last row to be translation
        Matrix4B4 translationMatrix = new Matrix4B4(
        new MyVector3(1, 0, 0),
        new MyVector3(0, 1, 0),
        new MyVector3(0, 0, 1),
        new MyVector3(Translation.x, Translation.y, Translation.z)
        );  

        //Rotatio
        MyVector3 RotationInRadians = new MyVector3();  //Creates Rotation in radians

        //Convert Rotation to Radians
        RotationInRadians.x = VectorMaths.Deg2Rad(Rotation.x);  //Converts Rotation x to Radians
        RotationInRadians.y = VectorMaths.Deg2Rad(Rotation.y);  //Converts Rotation y to Radians
        RotationInRadians.z = VectorMaths.Deg2Rad(Rotation.z);    //Converts Rotation z to Radians

        QuatRotation = Quat.EulerToQuat(RotationInRadians);   //Convert Euler Rotation To Quat Rotation
        RotationMatrix = Matrix4B4.QuatToMatrix(QuatRotation);  //Convert Quat rotation to matrix
        Matrix4B4 M = translationMatrix * RotationMatrix * scaleMatrix;  //Transformation matrix set my multiplying matrcies in TRS ordeer

        //Loop through Model points and transform them from local to world
        for (int i = 0;   i < TransformedVertices.Length;   i++)
        {
            TransformedVertices[i] = M * ModelSpaceVertices[i];   //Transform vertices by multiplying matrix by Model spacee verticies
        }

        //Goes through all model space vertices to create model bounds
        for (int i = 0;   i < ModelSpaceVertices.Length;   i++)
        {
            //If Model space vertice is less then model min extent then set model min extent to be model space verticies repeats for x y and z

            if (ModelSpaceVertices[i].x < ModelMinExtent.x)
            {
                ModelMinExtent.x = ModelSpaceVertices[i].x;  //Set Model Min Extent x to model space vertices x
            }
            if (ModelSpaceVertices[i].y < ModelMinExtent.y)
            {
                ModelMinExtent.y = ModelSpaceVertices[i].y;   //Set Model Min Extent y to model space vertices y
            }
            if (ModelSpaceVertices[i].z < ModelMinExtent.z)
            {
                ModelMinExtent.z = ModelSpaceVertices[i].z;   //Set Model Min Extent z to model space vertices z
            }

            //If Model space vertice is more then model max extent then set model max extent to be model space verticies repeats for x y and z
            if (ModelSpaceVertices[i].x > ModelMaxExtent.x)
            {
                ModelMaxExtent.x = ModelSpaceVertices[i].x;  //Set Model Max Extent x to model space vertices x
            }
            if (ModelSpaceVertices[i].y > ModelMaxExtent.y)
            {
                ModelMaxExtent.y = ModelSpaceVertices[i].y;   //Set Model Max Extent y to model space vertices y
            }
            if (ModelSpaceVertices[i].z > ModelMaxExtent.z)
            {
                ModelMaxExtent.z = ModelSpaceVertices[i].z;   //Set Model Max Extent z to model space vertices z
            }
        }

        //Unity Mesh Filter
        MeshFilter MF = GetComponent<MeshFilter>();  //Gets mesh filter
        MF.mesh.vertices = TransformedVertices;  //Sets vertices to be the transformed vertices 
        MF.mesh.RecalculateNormals();  //Calls Recalculate Normal
        MF.mesh.RecalculateBounds();   //Calls Recalculate bounds
    }
}
