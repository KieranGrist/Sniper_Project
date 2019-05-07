using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ExplosionInit
{
    public float ExplosionRadius;
    public float ExplosionStrength;
}
public class Explosion : MonoBehaviour
{
    BoundingObject ExplosionBounds;
    public float ExplosionRadius = 5.0f;
    public float ExplosionStrength = 200.0f;
    public myTransformation Transformation;
    public MyPhysics[] Object;
    public MyVector3 ExplosionForce;
    public MyVector3 ExplosionTorque;
    public MyVector3 CentreOfMass;
    public BoundingCircle OtherObject;
    public MyVector3 Distance;
    public MyVector3 Normalised;
    public MyVector3 ImpactPoint;
    float timer;
    // Use this for initialization
    void Start()
    {

    }


    public void Init(ExplosionInit Values)
    {
        this.ExplosionRadius = Values.ExplosionRadius;
        this.ExplosionStrength = Values.ExplosionStrength;
    }
    void FixedUpdate()
    {
        ExplosionStrength = FindObjectOfType<ExplosionStrengthText>().slider.value;
        ExplosionRadius = FindObjectOfType<ExplosionRadiusText>().slider.value;
        //THIS IS HOW YOU DO GRAVITY BUT OPPOSITE HAS THIS REPELS THE OBJECTS
        ExplosionBounds = new BoundingCircle(Transformation.Translation, ExplosionRadius);
        Transformation = GetComponent<myTransformation>();
        BoundingCircle Bonds = new BoundingCircle(new MyVector3(0, 0, 0), ExplosionRadius);
        Bonds = ExplosionBounds as BoundingCircle;
        Bonds.CenterPoint = Transformation.Translation;


        Object = FindObjectsOfType<MyPhysics>();
        for (int i = 0; i < Object.Length; i++)
        {
            if (Bonds.Intersects(Object[i].Transformation.BoundObject))
            {
              
                float Radius = float.MinValue;
                if (Object[i].Transformation.BoundObject is AABB)
                {
                    AABB Box1 = Object[i].Transformation.BoundObject as AABB;

                    if (Box1.MaxExtent.x > Radius)
                    {
                        Radius = Box1.MaxExtent.x;
                    }
                    if (Box1.MaxExtent.y > Radius)
                    {
                        Radius = Box1.MaxExtent.y;
                    }
                    if (Box1.MaxExtent.z > Radius)
                    {
                        Radius = Box1.MaxExtent.z;
                    }


                }
                if (Object[i].Transformation.BoundObject is BoundingCircle)
                {
                    BoundingCircle Sphere1 = Object[i].Transformation.BoundObject as BoundingCircle;
                    Radius = Sphere1.Radius;
                }
                if (Object[i].Transformation.BoundObject is BoundingCapsule)
                {
                    BoundingCapsule Capsule1 = Object[i].Transformation.BoundObject as BoundingCapsule;
                    if (Capsule1.B.x > Radius)
                    {
                        Radius = Capsule1.B.x;
                    }
                    if (Capsule1.B.y > Radius)
                    {
                        Radius = Capsule1.B.y;
                    }
                    if (Capsule1.B.z > Radius)
                    {
                        Radius = Capsule1.B.z;
                    }


                }

                ExplosionForce = VectorMaths.VectorNormalized(Object[i].Transformation.Translation - Transformation.Translation) * ExplosionStrength;
                Object[i].Force += ExplosionForce;
                CentreOfMass = Transformation.Translation;

                Distance = Object[i].Transformation.Translation - Transformation.Translation;
                Normalised = VectorMaths.VectorNormalized(Distance);



                ImpactPoint = Normalised * Radius; //Other Object to Explosion, Normalise this, Multiple it by the radius of the other object;
                ExplosionTorque = VectorMaths.VectorCrossProduct(ExplosionForce, CentreOfMass - ImpactPoint);
                Object[i].torque += ExplosionTorque;

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        ExplosionStrength = FindObjectOfType<ExplosionStrengthText>().slider.value;
        ExplosionRadius = FindObjectOfType<ExplosionRadiusText>().slider.value;
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            Destroy(this.gameObject);
            Destroy(this);

        }
        Transformation = GetComponent<myTransformation>();

    }
}
