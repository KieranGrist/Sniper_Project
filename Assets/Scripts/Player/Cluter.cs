    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluter : MonoBehaviour {
     List<GameObject> Ambient = new List<GameObject>();
     Sniper Player;   // Use this for initialization
    public List<GameObject> Models = new List<GameObject>();
     MyVector3  OriginPoint = new MyVector3(0,0,0);
     MyVector3 Distance = new MyVector3(0, 0, 0);
     MyVector3 NormalisedVector = new MyVector3(0, 0, 0);
     MyVector3 OriginLast;
     float radius;
     int CluterAmmount =500;
    int CluterAmmountLast =0;
    float DistanceToRadius =0;
    void Start () {
		
	}
    MyVector3 RandomVector(MyVector3 random)
    {
        MyVector3 Ret = new MyVector3(0,0,0);
        Ret.x = Random.Range(-random.x, random.x);
        Ret.y = 4;
        Ret.z = Random.Range(-random.z, random.z);
        return Ret;
    }
   // Update is called once per frame
    void Update()
    {
        Player = GetComponent<Sniper>();
        myTransformation Ptransform = Player.GetComponent<myTransformation>();
        Distance = Ptransform.Translation - OriginPoint;
        DistanceToRadius = Distance.Length();

       if (CluterAmmount != CluterAmmountLast || DistanceToRadius >= 250)
        {
   
          OriginPoint = new MyVector3(Ptransform.Translation.x, Ptransform.Translation.y, Ptransform.Translation.z);
         
          if (Ambient != null)
            {
                if (Ambient.Count >= 0)
                {
                    for (int i = 0; i < Ambient.Count; i++)
                    {
         
                        Destroy(Ambient[i]);

                    }
                }
            }
            Ambient.Clear();

            for (int i = 0; i < CluterAmmount; i++)
            {
                radius =Random.Range(0,5000.0f);
                int x = Random.Range(0, Models.Count);
                GameObject go = Instantiate(Models[x], transform.position, transform.rotation);
                go.AddComponent<myTransformation>();
               //go.AddComponent<MyPhysics>();
                TransformationInit Temp;
                Temp.Translation = new MyVector3(Random.Range(-1.0f, 1.0f), 6, Random.Range(-1.0f, 1.0f));
                Temp.Translation = VectorMaths.VectorNormalized(Temp.Translation);
                Temp.Translation *= radius;
                Temp.Translation += OriginPoint;
                Temp.Translation.y = 6;
                //RandomVector(radius);
                Temp.Rotation = new MyVector3();
                Temp.Scale = new MyVector3(5, 5, 5);

                go.GetComponent<myTransformation>().Initialise(Temp);
                go.name = "Cluter" + i;
                Ambient.Add(go);
            }

        }
        CluterAmmountLast = CluterAmmount;
    
    }
    }
