using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Explosion : MonoBehaviour {
    BoundingObject ExplosionBounds;
    public float ExplosionRadius = 5.0f;
    public float ExplosionStrength = 200.0f;
    public myTransformation Transformation;
    public MyPhysics Object;
	// Use this for initialization
	void Start () {
        
	}
    void FixedUpdate() { 
          ExplosionBounds = new BoundingCircle(Transformation.Translation, ExplosionRadius);
          Transformation = GetComponent<myTransformation>();
        BoundingCircle Bonds = new BoundingCircle(new MyVector3(0, 0, 0), ExplosionRadius);
        Bonds = ExplosionBounds as BoundingCircle;
        Bonds.CenterPoint = Transformation.Translation;
        if (Bonds.Intersects(Object.Transformation.BoundObject))
        {
            Debug.Log("Is Intersecting");
            MyVector3 ExplosionForce = VectorMaths.VectorNormalized((Object.Transformation.Translation = Transformation.Translation)) * ExplosionStrength;
            Object.Force += ExplosionForce;
            MyVector3 CentreOfMass = Transformation.Translation;
            BoundingCircle OtherObject = Object.Transformation.BoundObject as BoundingCircle;
            MyVector3 Distance = Object.Transformation.Translation = Transformation.Translation;
            MyVector3 Normalised = VectorMaths.VectorNormalized(Distance);
     
            MyVector3 ImpactPoint = Normalised * OtherObject.Radius; //Other Object to Explosion, Normalise this, Multiple it by the radius of the other object;
            Object.torque += VectorMaths.VectorCrossProduct(ExplosionForce, CentreOfMass - ImpactPoint);

        }
    }
    // Update is called once per frame
    void Update () {
  
        Transformation = GetComponent<myTransformation>();
	}
}
