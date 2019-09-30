using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
[System.Serializable]
public struct ExplosionInit
{
    public float ExplosionRadius; //Explosion Radius
    public float ExplosionStrength; //Explosion Strength
}
public class Explosion : MonoBehaviour
{
    BoundingObject ExplosionBounds; //Collision Object for explosion
    public float ExplosionRadius = 5.0f; //How far the object impacts
    public float ExplosionStrength = 200.0f; //How strong the impact of the explosion is
    public myTransformation Transformation; //Position of explosion
    public MyPhysics[] Object; //Physic objects array
    public MyVector3 ExplosionForce; //Force of explosion
    public MyVector3 ExplosionTorque; //Rotation force of explosion
    public MyVector3 CentreOfMass; //Centre Of explosion
    public MyVector3 Distance; //Distance from explosion to object
    public MyVector3 Normalised; //Normalised value
    public MyVector3 ImpactPoint; //Impact point of explosion
    float timer; //Timer used for destruction 
    // Use this for initialization
    void Start()
    {

    }


    public void Init(ExplosionInit Values)
    {
        this.ExplosionRadius = Values.ExplosionRadius; //Set radius to be init radius
        this.ExplosionStrength = Values.ExplosionStrength; //Set strength to be init strength
    }
    void FixedUpdate()
    {
        ExplosionStrength = FindObjectOfType<ExplosionStrengthText>().slider.value; //Set Strength to be slider value
        ExplosionRadius = FindObjectOfType<ExplosionRadiusText>().slider.value; //Set radius to be slider value
        //THIS IS HOW YOU DO GRAVITY BUT OPPOSITE THIS REPELS THE OBJECTS
        ExplosionBounds = new BoundingCircle(Transformation.Translation, ExplosionRadius); //Set bounds of explosion
        Transformation = GetComponent<myTransformation>(); //Get Transformation component of object
        BoundingCircle Bounds = new BoundingCircle(new MyVector3(0, 0, 0), ExplosionRadius); //Bounds Of explosion 
        Bounds = ExplosionBounds as BoundingCircle; //Set bounds to be xplosion bounds
        Bounds.CentrePoint = Transformation.Translation; //Make sure centre point is position of expplosion

        Object = FindObjectsOfType<MyPhysics>(); //Find all physic objects
        for (int i = 0;   i < Object.Length;  i++)
        {
            if (Bounds.Intersects(Object[i].Transformation.BoundObject))
            {
              
                float Radius = float.MinValue; //Set radius to be minium value
                if (Object[i].Transformation.BoundObject is AABB)
                {
                    AABB Box1 = Object[i].Transformation.BoundObject as AABB; //Get the object collision box

                    if (Box1.MaxExtent.x > Radius)
                    {
                        Radius = Box1.MaxExtent.x; //Set radius to be max extent x
                    }
                    if (Box1.MaxExtent.y > Radius)
                    {
                        Radius = Box1.MaxExtent.y; //Set radius to be max extent y
                    }
                    if (Box1.MaxExtent.z > Radius)
                    {
                        Radius = Box1.MaxExtent.z; //Set radius to be max extent z
                    }


                }
                if (Object[i].Transformation.BoundObject is BoundingCircle)
                {
                    BoundingCircle Sphere1 = Object[i].Transformation.BoundObject as BoundingCircle; //Get the object bounding sphere
                    Radius = Sphere1.Radius; //Set radius to be object radius
                }
         
                ExplosionForce = VectorMaths.VectorNormalized(Object[i].Transformation.Translation - Transformation.Translation) * ExplosionStrength; //Set force of explosion to be normalised of object postion - explosion position 
                Object[i].Force += ExplosionForce; //Increase object force by explosion force
                CentreOfMass = Transformation.Translation; //SEt centre of mass to explosion point

                Distance = Object[i].Transformation.Translation - Transformation.Translation;//Distance from object to explosion
                Normalised = VectorMaths.VectorNormalized(Distance); //Normalised Distance



                ImpactPoint = Normalised * Radius; //Set impact point to be normalised * radius
                ExplosionTorque = VectorMaths.VectorCrossProduct(ExplosionForce, CentreOfMass - ImpactPoint); //Toorque os the cross product of explosion force and centre of mass - impact point
                Object[i].torque += ExplosionTorque; //Torque is increaess by explosion torque

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        ExplosionStrength = FindObjectOfType<ExplosionStrengthText>().slider.value; //Set Strength to be slider value
        ExplosionRadius = FindObjectOfType<ExplosionRadiusText>().slider.value; //Set radius to be slider value
        timer += Time.deltaTime; //Increase timer by delta time
        if (timer >= 2)
        {
            Destroy(this.gameObject); //Destroy Explosion game object
            Destroy(this); //Destroy this component 

        }
        Transformation = GetComponent<myTransformation>(); //Get Transformation Componenet

    }
}
