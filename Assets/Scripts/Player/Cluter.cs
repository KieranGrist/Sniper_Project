    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluter : MonoBehaviour {
    public List<GameObject> Ambient = new List<GameObject>();
    public Sniper Player;   // Use this for initialization
    public List<GameObject> Models = new List<GameObject>();
   public  MyVector3  OriginPoint = new MyVector3(0,0,0);
    MyVector3 OriginLast = new MyVector3(0, 0, 0);
    public MyVector3 radius = new MyVector3(0, 0, 0);
    public MyVector3 Distance = new MyVector3(0, 0, 0);
    public MyVector3 NormalisedVector = new MyVector3(0, 0, 0);
    public int CluterAmmount =500;
    int CluterAmmountLast =0;
  public  float DistanceToRadius =0;
    void Start () {
		
	}
    MyVector3 RandomVector(MyVector3 Min,MyVector3 Max)
    {
        MyVector3 Ret = new MyVector3(0,0,0);
        Ret.x = Random.Range(Min.x, Max.x);
        Ret.y = 4;
        Ret.z = Random.Range(Min.z, Max.z);
        return Ret;
    }

   // Update is called once per frame
    void Update()
    {
        myTransformation Ptransform = Player.GetComponent<myTransformation>();
        Distance = Ptransform.Translation - OriginPoint;

        DistanceToRadius = Distance.Length();
        if (OriginPoint != OriginLast)
        {
            OriginPoint = OriginLast;
        }
       

        if (CluterAmmount != CluterAmmountLast || DistanceToRadius >= 250)
        {
            MyVector3 NormalisedVector = new MyVector3(1, 0, 1);
           
            NormalisedVector *= 500;

            radius = Ptransform.Translation + NormalisedVector;

            MyVector3 MinExtent = Ptransform.Translation - NormalisedVector;
            MyVector3 MaxExtent = Ptransform.Translation + NormalisedVector;
            DistanceToRadius = 0;
            Debug.Log("origin change");
            OriginPoint = new MyVector3(0, 0, 0);
            OriginPoint = Ptransform.Translation;
            OriginLast = OriginPoint;
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
                int x = Random.Range(0, Models.Count);
                GameObject go = Instantiate(Models[x], transform.position, transform.rotation);
                go.AddComponent<myTransformation>();
                //    go.AddComponent<MyPhysics>();
                TransformationInit Temp;
                Temp.Translation = RandomVector(MinExtent,MaxExtent);
                Temp.Feat = 2;
                Temp.Head = 2;
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
