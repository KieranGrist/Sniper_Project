using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix4B4
{
  public static void DebugMyMatrix(Matrix4B4 matrix)
    {
        Vector4 Row0 = matrix.GetRow(0);
        Vector4 Row1 = matrix.GetRow(1);
        Vector4 Row2 = matrix.GetRow(2);
        Vector4 Row3 = matrix.GetRow(3);
        Debug.Log("Row 0");
        Debug.Log(Row0);
        Debug.Log("Row 1");
        Debug.Log(Row1);
        Debug.Log("Row 2");
        Debug.Log(Row2);
        Debug.Log("Row 3");
        Debug.Log(Row3);
    }
    public Matrix4B4(Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
    {
        values = new float[4, 4];
        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = column1.w;

        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = column2.w;

        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = column3.w;

        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = column4.w;
    }
    public Matrix4B4(MyVector3 column1, MyVector3 column2, MyVector3 column3, MyVector3 column4)
    {
        values = new float[4, 4];
        //     R, C
        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = 0;

        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = 0;

        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = 0;

        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = 1;
    }
    public float[,] values = new float[4, 4];
    public static Vector4 operator *(Matrix4B4 lhs, Vector4 vector)
    {
        vector.w = 1;
        /*
         [0,0,0,0]
         [0,0,0,0]
         [0,0,0,0]
         [0,0,0,0]

         [column1.x;,column1.y;,column1.z;,0]           * [0,0,0,0]
         [0,0,0,0]
         [0,0,0,0]
         [0,0,0,0]

         */
        Vector4 RET;
        RET = vector;
        RET.x = lhs.values[0, 0] * vector.x + lhs.values[0, 1] * vector.y + lhs.values[0, 2] * vector.z + lhs.values[0, 3] * vector.w;
        RET.y = lhs.values[1, 0] * vector.x + lhs.values[1, 1] * vector.y + lhs.values[1, 2] * vector.z + lhs.values[1, 3] * vector.w;
        RET.z = lhs.values[2, 0] * vector.x + lhs.values[2, 1] * vector.y + lhs.values[2, 2] * vector.z + lhs.values[2, 3] * vector.w;
        RET.w = lhs.values[3, 0] * vector.x + lhs.values[3, 1] * vector.y + lhs.values[3, 2] * vector.z + lhs.values[3, 3] * vector.w;
        return RET;

    }

    public static Matrix4B4 operator *(Matrix4B4 lhs, Matrix4B4 rhs)
    {
        Matrix4B4 RET = new Matrix4B4(new Vector4(), new Vector4(), new Vector4(), new Vector4());
        RET.values[0, 0] =
        lhs.values[0, 0] * rhs.values[0, 0] +
        lhs.values[1, 0] * rhs.values[0, 1] +
        lhs.values[2, 0] * rhs.values[0, 2] +
        lhs.values[3, 0] * rhs.values[0, 3];

        RET.values[0, 1] =
           lhs.values[0, 1] * rhs.values[0, 0] +
           lhs.values[1, 1] * rhs.values[0, 1] +
           lhs.values[2, 1] * rhs.values[0, 2] +
           lhs.values[3, 1] * rhs.values[0, 3];

        RET.values[0, 2] =
  lhs.values[0, 2] * rhs.values[0, 0] +
  lhs.values[1, 2] * rhs.values[0, 1] +
  lhs.values[2, 2] * rhs.values[0, 2] +
  lhs.values[3, 2] * rhs.values[0, 3];

        RET.values[0, 3] =
        lhs.values[0, 3] * rhs.values[0, 0] +
   lhs.values[1, 3] * rhs.values[0, 1] +
   lhs.values[2, 3] * rhs.values[0, 2] +
   lhs.values[3, 3] * rhs.values[0, 3];

        RET.values[1, 0] =
        lhs.values[0, 0] * rhs.values[1, 0] +
        lhs.values[1, 0] * rhs.values[1, 1] +
        lhs.values[2, 0] * rhs.values[1, 2] +
        lhs.values[3, 0] * rhs.values[1, 3];

        RET.values[1, 1] =
 lhs.values[0, 1] * rhs.values[1, 0] +
 lhs.values[1, 1] * rhs.values[1, 1] +
 lhs.values[2, 1] * rhs.values[1, 2] +
 lhs.values[3, 1] * rhs.values[1, 3];

        RET.values[1, 2] =
lhs.values[0, 2] * rhs.values[1, 0] +
lhs.values[1, 2] * rhs.values[1, 1] +
lhs.values[2, 2] * rhs.values[1, 2] +
lhs.values[3, 2] * rhs.values[1, 3];


        RET.values[1, 3] =
        lhs.values[0, 3] * rhs.values[1, 0] +
        lhs.values[1, 3] * rhs.values[1, 1] +
        lhs.values[2, 3] * rhs.values[1, 2] +
        lhs.values[3, 3] * rhs.values[1, 3];

 
        RET.values[2, 0] =
        lhs.values[0, 0] * rhs.values[2, 0] +
        lhs.values[1, 0] * rhs.values[2, 1] +
        lhs.values[2, 0] * rhs.values[2, 2] +
        lhs.values[3, 0] * rhs.values[2, 3];

        RET.values[2, 1] =
     lhs.values[0, 1] * rhs.values[2, 0] +
     lhs.values[1, 1] * rhs.values[2, 1] +
     lhs.values[2, 1] * rhs.values[2, 2] +
     lhs.values[3, 1] * rhs.values[2, 3];


        RET.values[2, 2] =
        lhs.values[0, 2] * rhs.values[2, 0] +
        lhs.values[1, 2] * rhs.values[2, 1] +
        lhs.values[2, 2] * rhs.values[2, 2] +
        lhs.values[3, 2] * rhs.values[2, 3];

        RET.values[2, 3] =
    lhs.values[0, 3] * rhs.values[2, 0] +
    lhs.values[1, 3] * rhs.values[2, 1] +
    lhs.values[2, 3] * rhs.values[2, 2] +
    lhs.values[3, 3] * rhs.values[2, 3];

        RET.values[3, 0] =
        lhs.values[0, 0] * rhs.values[3, 0] +
        lhs.values[1, 0] * rhs.values[3, 1] +
        lhs.values[2, 0] * rhs.values[3, 2] +
        lhs.values[3, 0] * rhs.values[3, 3];

   

 

     
        RET.values[3, 1] =
        lhs.values[0, 1] * rhs.values[3, 0] +
        lhs.values[1, 1] * rhs.values[3, 1] +
        lhs.values[2, 1] * rhs.values[3, 2] +
        lhs.values[3, 1] * rhs.values[3, 3];
  



        RET.values[3, 2] =
        lhs.values[0, 2] * rhs.values[3, 0] +
        lhs.values[1, 2] * rhs.values[3, 1] +
        lhs.values[2, 2] * rhs.values[3, 2] +
        lhs.values[3, 2] * rhs.values[3, 3];

   

    

        RET.values[3, 3] =
        lhs.values[0, 3] * rhs.values[3, 0] +
        lhs.values[1, 3] * rhs.values[3, 1] +
        lhs.values[2, 3] * rhs.values[3, 2] +
        lhs.values[3, 3] * rhs.values[3, 3];

        return RET;
    }
    public static Matrix4B4 Identiy
    {
        get
        {
            return new Matrix4B4(
                   new Vector4(1, 0, 0, 0),
                   new Vector4(0, 1, 0, 0),
                   new Vector4(0, 0, 1, 0),
                   new Vector4(0, 0, 0, 1)
                   );
        }
    }
    public Matrix4B4 TranslationInverse()
    {
        Matrix4B4 rv = Identiy;
        rv.values[0, 3] = -values[0, 3];
        rv.values[1, 3] = -values[1, 3];
        rv.values[2, 3] = -values[2, 3];
        return rv;
    }
    public Vector4 GetRow(int row)
    {
        Vector4 RET;

        RET = new Vector4(values[row, 0], values[row, 1], values[row, 2], values[row, 3]);

        return RET;
    }
    public Vector4 GetColum(int column)
    {
        Vector4 RET;
        RET = new Vector4(values[0, column], values[1, column], values[2, column], values[3, column]);
        return RET;
    }
    public Matrix4B4 RotationInverse()
    {
        return new Matrix4B4(GetRow(0), GetRow(1), GetRow(2), GetRow(3));
    }
    public Matrix4B4 ScaleInverse()
    {
    
        Matrix4B4 rv = Identiy;

        rv.values[0, 0] = 1.0f / values[0, 0];
        
        rv.values[1, 1] = 1.0f / values[1, 1];

        rv.values[2, 2] = 1.0f / values[2, 2];
    
        return rv;

    }
    public static Matrix4B4 QuatToMatrix(Quat quat)
    {
       // quat.SetVector(VectorMaths.VectorNormalized(quat.GetVector()));
        //Sample Retrieved from https://www.gamasutra.com/view/feature/131686/rotating_objects_using_quaternions.php?page=2
        //Modefied to fit my maths
        Matrix4B4 ret = Identiy;
        float wx, wy, wz, xx, yy, yz, xy, xz, zz, x2, y2, z2;


        // calculate coefficients
        x2 = quat.x + quat.x;
        y2 = quat.y + quat.y;
        z2 = quat.z + quat.z;
        xx = quat.x * x2;
        xy = quat.x * y2;
        xz = quat.x * z2;
        yy = quat.y * y2;
        yz = quat.y * z2;
        zz = quat.z * z2;
        wx = quat.w * x2;
        wy = quat.w * y2;
        wz = quat.w * z2;


        ret.values[0, 0] = 1.0f - (yy + zz);
        ret.values[1, 0] = xy - wz;
        ret.values[2, 0] = xz + wy;
        ret.values[3, 0] = 0.0f;

        ret.values[0, 1] = xy + wz;
        ret.values[1, 1] = 1.0f - (xx + zz);
        ret.values[2, 1] = yz - wx;
        ret.values[3, 1] = 0.0f;


        ret.values[0, 2] = xz - wy;
        ret.values[1, 2] = yz + wx;
        ret.values[2, 2] = 1.0f - (xx + yy);
        ret.values[3, 2] = 0.0f;


        ret.values[0, 3] = 0;
        ret.values[1, 3] = 0;
        ret.values[2, 3] = 0;
        ret.values[3, 3] = 1;




        //ret.values[0, 0] = 1.0f - (yy + zz);
        //ret.values[0, 1] = xy - wz;
        //ret.values[0, 2] = xz + wy;
        //ret.values[0, 3] = 0.0f;

        //ret.values[1, 0] = xy + wz;
        //ret.values[1, 1] = 1.0f - (xx + zz);
        //ret.values[1, 2] = yz - wx;
        //ret.values[1, 3] = 0.0f;


        //ret.values[2, 0] = xz - wy;
        //ret.values[2, 1] = yz + wx;
        //ret.values[2, 2] = 1.0f - (xx + yy);
        //ret.values[2, 3] = 0.0f;


        //ret.values[3, 0] = 0;
        //ret.values[3, 1] = 0;
        //ret.values[3, 2] = 0;
        //ret.values[3, 3] = 1; 
        return ret;
    }
}
