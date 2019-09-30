using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
[System.Serializable]

public class Matrix4B4
{
    public float[,] values = new float[4, 4]; //2D Array Representing a matrix
    public Matrix4B4(Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
    {
        values = new float[4, 4]; //Creating a 4 by 4 matrix with a 2D array
        values[0, 0] = column1.x; //Setting new matrix 0, 0 to be Column 1 x
        values[1, 0] = column1.y; //Setting new matrix 1, 0 to be Column 1 y
        values[2, 0] = column1.z; //Setting new matrix 2, 0 to be Column 1 z
        values[3, 0] = column1.w; //Setting new matrix 3, 0 to be Column 1 w

        values[0, 1] = column2.x; //Setting new matrix 0, 1 to be Column 2 x
        values[1, 1] = column2.y; //Setting new matrix 1, 1 to be Column 2 y
        values[2, 1] = column2.z; //Setting new matrix 2, 1 to be Column 2 z
        values[3, 1] = column2.w; //Setting new matrix 3, 1 to be Column 2 w

        values[0, 2] = column3.x; //Setting new matrix 0, 2 to be Column 3 x
        values[1, 2] = column3.y; //Setting new matrix 1, 2 to be Column 3 y
        values[2, 2] = column3.z; //Setting new matrix 2, 2 to be Column 3 z
        values[3, 2] = column3.w; //Setting new matrix 3, 2 to be Column 3 w

        values[0, 3] = column4.x; //Setting new matrix 0, 3 to be Column 4 x 
        values[1, 3] = column4.y; //Setting new matrix 1, 3 to be Column 4 y
        values[2, 3] = column4.z; //Setting new matrix 2, 3 to be Column 4 w
        values[3, 3] = column4.w; //Setting new matrix 3, 3 to be Column 4 z
    }
    public Matrix4B4(MyVector3 column1, MyVector3 column2, MyVector3 column3, MyVector3 column4)
    {
        values = new float[4, 4]; //Creating a 4 by 4 matrix with a 2D array
        values[0, 0] = column1.x; //Setting new matrix 0, 0 to be Column 1 x
        values[1, 0] = column1.y; //Setting new matrix 1, 0 to be Column 1 y
        values[2, 0] = column1.z; //Setting new matrix 2, 0 to be Column 1 z
        values[3, 0] =0; //Setting new matrix 3, 0 to be 0

        values[0, 1] = column2.x; //Setting new matrix 0, 1 to be Column 2 x
        values[1, 1] = column2.y; //Setting new matrix 1, 1 to be Column 2 y
        values[2, 1] = column2.z; //Setting new matrix 2, 1 to be Column 2 z
        values[3, 1] = 0; //Setting new matrix 3, 1 to be 0

        values[0, 2] = column3.x; //Setting new matrix 0, 2 to be Column 3 x
        values[1, 2] = column3.y; //Setting new matrix 1, 2 to be Column 3 y
        values[2, 2] = column3.z; //Setting new matrix 2, 2 to be Column 3 z
        values[3, 2] = 0; //Setting new matrix 3, 2 to be 0

        values[0, 3] = column4.x; //Setting new matrix 0, 3 to be Column 4 x 
        values[1, 3] = column4.y; //Setting new matrix 1, 3 to be Column 4 y
        values[2, 3] = column4.z; //Setting new matrix 2, 3 to be Column 4 w
        values[3, 3] = 1; //Setting new matrix 3, 3 to be 1
    }




    public static Vector4 operator *(Matrix4B4 lhs, Vector4 vector)
    {
        vector.w = 1; //Sets w to 1 as conversion from vector 3 to 4 creates a w to be 0
   
        Vector4 RET; //Create RET //Create RET //Cret RET
        RET = vector; //Temorairly assign RET 
        //4 BY 4 Matrix Multiplied by a 4 by 1 matrix 
        RET.x = lhs.values[0, 0] * vector.x + lhs.values[0, 1] * vector.y + lhs.values[0, 2] * vector.z + lhs.values[0, 3] * vector.w;   //Rows multiplied by Columns Row is first Number Columns is second number
        RET.y = lhs.values[1, 0] * vector.x + lhs.values[1, 1] * vector.y + lhs.values[1, 2] * vector.z + lhs.values[1, 3] * vector.w; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.z = lhs.values[2, 0] * vector.x + lhs.values[2, 1] * vector.y + lhs.values[2, 2] * vector.z + lhs.values[2, 3] * vector.w;  //Rows multiplied by Columns Row is first Number Columns is second number
        RET.w = lhs.values[3, 0] * vector.x + lhs.values[3, 1] * vector.y + lhs.values[3, 2] * vector.z + lhs.values[3, 3] * vector.w;  //Rows multiplied by Columns Row is first Number Columns is second number
        return RET; //Create RET //Create RET //Return RET

    }

    public static Matrix4B4 operator *(Matrix4B4 lhs, Matrix4B4 rhs)
    {
        Matrix4B4 RET = new Matrix4B4(new Vector4(), new Vector4(), new Vector4(), new Vector4()); //Create a new matrix 4 by 4 
        RET.values[0, 0] = lhs.values[0, 0] * rhs.values[0, 0] + lhs.values[0, 1] * rhs.values[1, 0] + lhs.values[0, 2] * rhs.values[2, 0] + lhs.values[0, 3] * rhs.values[3, 0]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[0, 1] = lhs.values[0, 0] * rhs.values[0, 1] + lhs.values[0, 1] * rhs.values[1, 1] + lhs.values[0, 2] * rhs.values[2, 1] + lhs.values[0, 3] * rhs.values[3, 1]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[0, 2] = lhs.values[0, 0] * rhs.values[0, 2] + lhs.values[0, 1] * rhs.values[1, 2] + lhs.values[0, 2] * rhs.values[2, 2] + lhs.values[0, 3] * rhs.values[3, 2]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[0, 3] = lhs.values[0, 0] * rhs.values[0, 3] + lhs.values[0, 1] * rhs.values[1, 3] + lhs.values[0, 2] * rhs.values[2, 3] + lhs.values[0, 3] * rhs.values[3, 3]; ///Rows multiplied by Columns Row is first Number Columns is second number

        RET.values[1, 0] = lhs.values[1, 0] * rhs.values[0, 0] + lhs.values[1, 1] * rhs.values[1, 0] + lhs.values[1, 2] * rhs.values[2, 0] + lhs.values[1, 3] * rhs.values[3, 0];  //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[1, 1] = lhs.values[1, 0] * rhs.values[0, 1] + lhs.values[1, 1] * rhs.values[1, 1] + lhs.values[1, 2] * rhs.values[2, 1] + lhs.values[1, 3] * rhs.values[3, 1]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[1, 2] = lhs.values[1, 0] * rhs.values[0, 2] + lhs.values[1, 1] * rhs.values[1, 2] + lhs.values[1, 2] * rhs.values[2, 2] + lhs.values[1, 3] * rhs.values[3, 2]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[1, 3] = lhs.values[1, 0] * rhs.values[0, 3] + lhs.values[1, 1] * rhs.values[1, 3] + lhs.values[1, 2] * rhs.values[2, 3] + lhs.values[1, 3] * rhs.values[3, 3]; ///Rows multiplied by Columns Row is first Number Columns is second number


        RET.values[2, 0] = lhs.values[2, 0] * rhs.values[0, 0] + lhs.values[2, 1] * rhs.values[1, 0] + lhs.values[2, 2] * rhs.values[2, 0] + lhs.values[2, 3] * rhs.values[3, 0]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[2, 1] = lhs.values[2, 0] * rhs.values[0, 1] + lhs.values[2, 1] * rhs.values[1, 1] + lhs.values[2, 2] * rhs.values[2, 1] + lhs.values[2, 3] * rhs.values[3, 1]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[2, 2] = lhs.values[2, 0] * rhs.values[0, 2] + lhs.values[2, 1] * rhs.values[1, 2] + lhs.values[2, 2] * rhs.values[2, 2] + lhs.values[2, 3] * rhs.values[3, 2]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[2, 3] = lhs.values[2, 0] * rhs.values[0, 3] + lhs.values[2, 1] * rhs.values[1, 3] + lhs.values[2, 2] * rhs.values[2, 3] + lhs.values[2, 3] * rhs.values[3, 3]; //Rows Into Columns Row is first Number Columns is second number

        RET.values[3, 0] = lhs.values[3, 0] * rhs.values[0, 0] + lhs.values[3, 1] * rhs.values[1, 0] + lhs.values[3, 2] * rhs.values[2, 0] + lhs.values[3, 3] * rhs.values[3, 0]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[3, 1] = lhs.values[3, 0] * rhs.values[0, 1] + lhs.values[3, 1] * rhs.values[1, 1] + lhs.values[3, 2] * rhs.values[2, 1] + lhs.values[3, 3] * rhs.values[3, 1]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[3, 2] = lhs.values[3, 0] * rhs.values[0, 2] + lhs.values[3, 1] * rhs.values[1, 2] + lhs.values[3, 2] * rhs.values[2, 2] + lhs.values[3, 3] * rhs.values[3, 2]; //Rows multiplied by Columns Row is first Number Columns is second number
        RET.values[3, 3] = lhs.values[3, 0] * rhs.values[0, 3] + lhs.values[3, 1] * rhs.values[1, 3] + lhs.values[3, 2] * rhs.values[2, 3] + lhs.values[3, 3] * rhs.values[3, 3]; //Rows multiplied by Columns Row is first Number Columns is second number

        return RET; //Create RET //Create RET //Return RET 
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
                   ); //Identity Matrix
        }
    }
    public Matrix4B4 Transpose
    {
        get
        {
            for (int i = 0;   i < 4;   i++)
                for (int j = i + 1;   j < 4;   j++)
                {
                    float temp = values[i, j]; //Set temp to be matrix [i, j[
                    values[i, j] = values[j, i];  //Swaping rows and coloumns 
                    values[j, i] = temp; //set [j, i] to be temp
                }

            return this; //return this matrix
        }
    }
    public Matrix4B4 InvertTR()
    {
        Matrix4B4 rv = Identiy; 

        for (int i = 0;  i < 4;   i++)
            for (int j = i + 1;   j < 4;  j++)
            {
                rv.values[i, j] = values[j, i]; //Transpose matrix
            }
        rv.SetColumn(3, -(rv * GetColum(3))); //Set the colum to inverse of rv * column 4 
        return rv; //return RV
    }
    public void SetColumn(int column, Vector4 Value)
    {

        values[0, column] = Value.x; //Set row 0 colum X to x 
        values[1, column] = Value.y; //Set row 0 colum X to y  
        values[2, column] = Value.z; //Set row 0 colum X to z 
        values[3, column] = Value.w; //Set row 0 colum X to w 
    }
    public Vector4 GetRow(int row)
    {
        Vector4 RET; //Create RET 

        RET = new Vector4(values[row, 0], values[row, 1], values[row, 2], values[row, 3]); //set vector 4 to be values of row X colum 0, row x colum 1 etc

        return RET; //Return RET
    }
    public Vector4 GetColum(int column)
    {
        Vector4 RET; //Create RET 
        RET = new Vector4(values[0, column], values[1, column], values[2, column], values[3, column]); //set vector 4 to be values of row 0 colum X, row 1 colum X etc
        return RET; //Return RET 
    }
    public Matrix4B4 TranslationInverse()
    {
        Matrix4B4 rv = Identiy; //Create identity matrix
        rv.values[0, 3] = -values[0, 3]; //Set values of 0, 3 to be inverse of 0,3
        rv.values[1, 3] = -values[1, 3]; //Set values of 0, 3 to be inverse of 1,3
        rv.values[2, 3] = -values[2, 3]; //Set values of 0, 3 to be inverse of 2,3
        return rv; //Return RV
    }
    public Matrix4B4 RotationInverse()
    {
        return new Matrix4B4(GetRow(0), GetRow(1), GetRow(2), GetRow(3)); //set new matrix to be values of rows 
    }
    public Matrix4B4 ScaleInverse()
    {
    
        Matrix4B4 rv = Identiy; //Create identity matrix

        rv.values[0, 0] = 1.0f / values[0, 0]; //set 0,0 to be 1 divided by value [0,0]
        
        rv.values[1, 1] = 1.0f / values[1, 1];//set 1,1 to be 1 divided by value [1,1]

        rv.values[2, 2] = 1.0f / values[2, 2]; //set 2,2 to be 1 divided by value [2,2]

        return rv; //Return RV

    }
    public static Matrix4B4 QuatToMatrix(Quat quat)
    {

        //Rotating Objects Using Quaternions (Bobic, 1998)
        //Modefied to fit my maths
        Matrix4B4 RET = Identiy; //Create Identiy Matrix
        float wx, wy, wz, xx, yy, yz, xy, xz, zz, x2, y2, z2; //Creates coefficent values 


        // calculate coefficients
        x2 = quat.x + quat.x; //set X2 To be x * x
        y2 = quat.y + quat.y; //set Y2 To be Y * Y
        z2 = quat.z + quat.z; //set Z2 To be Z * Z
        xx = quat.x * x2; //Multiply x by X2
        xy = quat.x * y2; //Multiply x by Y2
        xz = quat.x * z2; //Multiply x by Z2
        yy = quat.y * y2; //Multiply y by Y2
        yz = quat.y * z2; //Multiply y by Z2
        zz = quat.z * z2; //Multiply z by Z2
        wx = quat.w * x2; //Multiply w by X2
        wy = quat.w * y2; //Multiply w by Y2
        wz = quat.w * z2; //Multiply w by Z2


        RET.values[0, 0] = 1.0f - (yy + zz); //Set 0, 0 to be 1 - (yy + zz)
        RET.values[1, 0] = xy - wz; //Set 1, 0 to be xy - wz
        RET.values[2, 0] = xz + wy; //Set 2, 0 to be xz wy
        RET.values[3, 0] = 0.0f; //Set 3, 0 to be 0.0f

        RET.values[0, 1] = xy + wz; //Set 0, 1 to be Xy + Wz
        RET.values[1, 1] = 1.0f - (xx + zz); //Set 1, 1 to be 1 - (xx + zz)
        RET.values[2, 1] = yz - wx; //Set 2, 1 to be yz - wx
        RET.values[3, 1] = 0.0f; //Set 3,1 to be 0


        RET.values[0, 2] = xz - wy; //Set 0, 2 to be xz - wy
        RET.values[1, 2] = yz + wx; //Set 1, 2 to be yz + wx
        RET.values[2, 2] = 1.0f - (xx + yy); //Set 2,2 to be 1 - (xx + yy)
        RET.values[3, 2] = 0.0f; //Set 3, 2 to be 0


        RET.values[0, 3] = 0; //Set 0, 3 to be 0
        RET.values[1, 3] = 0; //Set 1, 3 to be 0
        RET.values[2, 3] = 0; //Set 2, 3 to be 0
        RET.values[3, 3] = 1; //Set 3, 3 to be 1
        return RET; //Create RET
    }
}
