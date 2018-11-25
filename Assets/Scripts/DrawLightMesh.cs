using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLightMesh : MonoBehaviour {

    [Range(0.1f,10)]
    public float resolution = 1; // number of rays cast per degree

    [Range(1, 100)]
    public float maxDistance = 10f; // number of units the ray will cast before not hitting something


    [Range(3, 1000)]
   // public int count = 10;

    public MeshFilter mesh;
    Mesh LightMesh;


    Vector3[] LightMeshPoints;



    public LineRenderer line;


    public LayerMask layerMask;



    // Use this for initialization
    void Start () {
        //creating a mesh and putting it on the meshFilter component.
        LightMesh = new Mesh();
        mesh.mesh = LightMesh;
	}
	
	// Update is called once per frame
	void Update () {



        float count = 360 * resolution;
    
        LightMeshPoints = TestHits(layerMask, count, maxDistance);

        Draw(LightMeshPoints);



    }


    Vector3[] TestHits(LayerMask layer, float count, float distance)
    {
        float angleBetween = 360 / (float)count;
        Vector2 pos = transform.position;

        Vector2[] points = new Vector2[(int)count];

        for (int i = 0; i < count; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, DirectionFromAngle(angleBetween * i), distance*2);
        


            if(hit.collider == null)
            {
                points[i] =  DirectionFromAngle(angleBetween * i)*distance;
            }
            else
            {
                points[i] = transform.InverseTransformPoint(hit.point);
            }
        }



        // Debug.DrawRay((Vector2)transform.position, Vector2.one*2);

        return V2toV3Array(points,0);
    }


    public Vector2 DirectionFromAngle(float angleDegrees)
    {
   
        return new Vector2(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }

    void Draw(Vector3[] ArrayOfPoints)
    {
        int count = ArrayOfPoints.Length;
  
        //line renderer
        line.positionCount = ArrayOfPoints.Length;
        line.loop = true;
        line.SetPositions(ArrayOfPoints);
        //line renderer
        

        Vector3[] points = new Vector3[count + 1];
        points[0] = Vector2.zero;

        for (int i = 0; i < ArrayOfPoints.Length; i++)
        {
            points[i + 1] = ArrayOfPoints[i];
        }

        int[] triangles = new int[3 * (points.Length - 1)];

        for (int i = 0; i < points.Length - 1; i++)
        {
            triangles[(3 * i)] = 0;
            triangles[(3 * i) + 1] = i + 1;
            triangles[(3 * i) + 2] = i + 2;
            if (triangles[(3 * i) + 2] > points.Length - 1)
            {
                triangles[(3 * i) + 2] = 1;
            }
        }

        LightMesh.vertices = points;
        LightMesh.triangles = triangles;
        LightMesh.RecalculateNormals();
    }


    Vector2[] CircleOfPoints(int totalPoints, float radius)
    {

        float temp = (float)totalPoints;
        float angle = 360 / temp;

        Vector2[] points = new Vector2[totalPoints];

        for (int i = 0; i < totalPoints; i++)
        {

            points[i] = (FindPointFromAngle(angle * i, radius, false));

        }

        return points;

    }

    Vector2 FindPointFromAngle(float angle, float radius, bool isRadians)
    {
        if (isRadians == false)
        {
            angle = Mathf.Deg2Rad * angle;

        }

        float x = radius * Mathf.Sin(angle);
        float y = radius * Mathf.Cos(angle);


        return new Vector2(x,y);
    }


    Vector3[] V2toV3Array(Vector2[] Vector2Array, float commonZValue)
    {

        Vector3[] Vector3Array = new Vector3[Vector2Array.Length];


        for (int i = 0; i < Vector2Array.Length; i++)
        {
            Vector3Array[i] = new Vector3(Vector2Array[i].x, Vector2Array[i].y, commonZValue);
        }


        return Vector3Array;
    }
}



struct LightRay
{
  

    public LightRay(bool hit, Vector2 point)
    {


    }

}
