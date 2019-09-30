using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
[System.Serializable]
public class CamLock : MonoBehaviour {
    public myTransformation SFocus; 
    // Use this for initialization
    void Start() {
   

    }

    // Update is called once per frame
    void Update() {
        Camera AlphaCam; 
        AlphaCam = GetComponent<Camera>(); 
        myTransformation Focus = SFocus; 
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
}
