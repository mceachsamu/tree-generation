using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Shapes3D.PipeSegment pipe = new Shapes3D.PipeSegment(){
            _EndRadius = 1.0f,
            _StartRadius = 0.5f,
            _End = new Vector3(0.0f,1.0f,0.0f),
            _Start = new Vector3(0.0f,0.0f,0.0f),
        };

        Shapes3D.PipeSegment pipe2 = new Shapes3D.PipeSegment(){
            _EndRadius = 1.4f,
            _StartRadius = 1.0f,
            _End = new Vector3(0.2f,2.0f,0.4f),
            _Start = new Vector3(0.0f,1.0f,0.0f),
        };


        Shapes3D.PipeSegment pipe3 = new Shapes3D.PipeSegment(){
            _EndRadius = 1.6f,
            _StartRadius = 1.4f,
            _End = new Vector3(0.5f,2.5f,0.7f),
            _Start = new Vector3(0.2f,2.0f,0.4f),
        };

        Mesh m = Shapes3D.CreatePipe(30, new Shapes3D.PipeSegment[]{pipe, pipe2, pipe3});

        this.GetComponent<MeshFilter>().mesh = m;
    }
}
