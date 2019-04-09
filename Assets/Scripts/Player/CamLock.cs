
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLock : MonoBehaviour {
     Camera AlphaCam;
    public Bullet BFocus;
    public Sniper SFocus;
    public bool BulletCam = false;
    public MyVector3 Euler;
    MyVector3 Offset = new MyVector3();
    // Use this for initialization
    void Start() {
        AlphaCam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update() {
        if (BFocus == null)
        {
            BulletCam = false;
            AlphaCam = GetComponent<Camera>();
        }
        else if (BFocus.Alive == false)
        {
            BulletCam = false;
            AlphaCam = GetComponent<Camera>();
        }
        if (BulletCam == false)
        {
            myTransformation Focus = SFocus.GetComponent<myTransformation>();
            MyVector3 Camera = new MyVector3(Focus.Rotation.x, Focus.Rotation.y, Focus.Rotation.z);
            MyVector3 CameraInRad = new MyVector3();
            CameraInRad.x = VectorMaths.Deg2Rad(Camera.x);
            CameraInRad.y = VectorMaths.Deg2Rad(Camera.y);
            CameraInRad.z = VectorMaths.Deg2Rad(Camera.z);
            Quat Temp = Quat.EulerToQuat(CameraInRad);
            AlphaCam.transform.position =new Vector3(Focus.Translation.x, Focus.Translation.y, Focus.Translation.z);
            AlphaCam.transform.rotation = new Quaternion(
               - Temp.x,
               - Temp.y,
                Temp.z,
                Temp.w);
        }
    
        if (BulletCam == true)
        {
            Euler = new MyVector3(0, 0, 0);
            AlphaCam.transform.rotation = new Quaternion(0, 0, 0, 0);
                 MyVector3 CameraPosition = new MyVector3();
        
            CameraPosition =new MyVector3(BFocus.Transformation.Translation.x, BFocus.Transformation.Translation.y + 25, BFocus.Transformation.Translation.z);
            CameraPosition += (VectorMaths.VectorNormalized(BFocus.Physics.Velocity) *-1.0f) *20 ;
            Offset.x += 0.05f;
            Offset.y += 0.05f;

            CameraPosition += Offset;
            AlphaCam.transform.position = new Vector3(CameraPosition.x, CameraPosition.y, CameraPosition.z);

            //Direction TargetPostion - Alpha Cam         Direction to the target, convert to a angle format(Euler easiet)
            MyVector3 Direction = BFocus.Transformation.Translation - CameraPosition;
            
            Euler = VectorMaths.DirectionToEuler(VectorMaths.VectorNormalized(Direction));
            Quat Temp = Quat.EulerToQuat(Euler);

            AlphaCam.transform.rotation = new Quaternion(Temp.x, Temp.y, Temp.z, Temp.w);
        }

    }
}
