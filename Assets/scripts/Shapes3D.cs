using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

//this is a class used to generate 3d shapes on program startup
public class Shapes3D
{
    //creates a simple plane mesh to be used as the water surface
    public Mesh CreatePlane(int numSegs, float segSize){
        Mesh m = new Mesh();
        m.name = "mesh";
        m.Clear();

        float size = numSegs * segSize;

        Vector3[] vs = new Vector3[(int)(numSegs*numSegs)*6];
        Vector3[] nm = new Vector3[(int)(numSegs*numSegs)*6];
        Vector2[] us = new Vector2[(int)(numSegs*numSegs)*6];
        int[] tri = new int[(int)(numSegs*numSegs)*6];

        float width = (float)(numSegs) * segSize;
        int count = 0;
            for (int i = 0; i < numSegs; i++){
                for (int j = 0; j < numSegs; j++){
                //first traingle
                vs[count] = new Vector3(i*segSize - size/2.0f ,0.0f, j*segSize- size/2.0f);
                us[count] = new Vector2( (float)i / (float)numSegs, (float)j /(float)numSegs);
                tri[count] = count;
                count++;

                vs[count] = new Vector3(i*segSize - size/2.0f,0.0f,(j*segSize + segSize) - size/2.0f);
                us[count] = new Vector2( ((float)i) / (float)numSegs,  ((float)j+1.0f) / (float)numSegs);
                tri[count] = count;
                count++;

                vs[count] = new Vector3(i*segSize + segSize - size/2.0f,0.0f,j*segSize - size/2.0f);
                us[count] =  new Vector2( ((float)i+1.0f) / (float)numSegs,  ((float)j) / (float)numSegs);
                tri[count] = count;
                count++;

                //second triangle
                vs[count] = new Vector3(i*segSize + segSize - size/2.0f,0.0f,j*segSize - size/2.0f);
                us[count] = new Vector2( ((float)i+1.0f) / (float)numSegs,  ((float)j) / (float)numSegs);
                tri[count] = count;
                count++;

                vs[count] = new Vector3(i*segSize - size/2.0f,0.0f,j*segSize + segSize - size/2.0f);
                us[count] = new Vector2(((float)i) / (float)numSegs,  ((float)j+1.0f) / (float)numSegs);
                tri[count] = count;
                count++;

                vs[count] = new Vector3(i*segSize + segSize - size/2.0f,0.0f,j*segSize + segSize - size/2.0f);
                us[count] = new Vector2( ((float)i + 1.0f) / (float)numSegs,  ((float)j + 1.0f) / (float)numSegs);
                tri[count] = count;
                count++;
            }
        }
        m.vertices = vs;
        m.uv = us;
        m.triangles = tri;
        m.RecalculateNormals();
        return m;
    }

    public Mesh CreateCylandar(float radius, float length, int numSegsRound, int numSegsLong) {
        Mesh m = new Mesh();
        m.name = "mesh";
        m.Clear();

        Vector3[] vs = new Vector3[(int)(numSegsLong*numSegsRound)*6];
        Vector2[] us = new Vector2[(int)(numSegsLong*numSegsRound)*6];
        int[] tri = new int[(int)(numSegsLong*numSegsRound)*6];

        int count = 0;
        for (int i = 0; i < numSegsLong; i++){
            for (int j = 0; j < numSegsRound; j++){
                float angle = (Mathf.PI*2 / numSegsRound) * j;
                float angle2 = (Mathf.PI*2 / numSegsRound) * (j + 1);

                float z = (length / numSegsLong) * i;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;

                float z2 = (length / numSegsLong) * (i+1);
                float x2 = Mathf.Cos(angle2) * radius;
                float y2 = Mathf.Sin(angle2) * radius;

                vs[count] = new Vector3(x ,y, z);
                us[count] = new Vector2((float)j / (float)numSegsRound, (float)i / (float)numSegsLong);
                tri[count] = count;
                count++;

                vs[count] = new Vector3(x2 ,y2, z);
                us[count] = new Vector2((float)(j+1) / (float)numSegsRound, (float)i / (float)numSegsLong);
                tri[count] = count;
                count++;

                vs[count] = new Vector3(x2 ,y2, z2);
                us[count] = new Vector2((float)(j+1) / (float)numSegsRound, (float)(i + 1)/ (float)numSegsLong);
                tri[count] = count;
                count++;

                vs[count] = new Vector3(x2 ,y2, z2);
                us[count] = new Vector2((float)(j+1) / (float)numSegsRound, (float)(i + 1)/ (float)numSegsLong);
                tri[count] = count;
                count++;

                vs[count] = new Vector3(x ,y, z2);
                us[count] = new Vector2((float)j / (float)numSegsRound, (float)(i + 1)/ (float)numSegsLong);
                tri[count] = count;
                count++;

                vs[count] = new Vector3(x ,y, z);
                us[count] = new Vector2((float)j / (float)numSegsRound, (float)i/ (float)numSegsLong);
                tri[count] = count;
                count++;
            }
        }
        m.vertices = vs;
        m.uv = us;
        m.triangles = tri;
        m.RecalculateNormals();
        return m;
    }

    public struct PipeSegment {      

        public PipeSegment(float length, float startRadius, float endRadius, Vector3 start, Vector3 end) {
            this._StartRadius = startRadius;
            this._EndRadius = endRadius;
            this._Start = start;
            this._End = end;
        }

        public Vector3 _Start;
        public float _StartRadius;

        public Vector3 _End;
        public float _EndRadius;
    }

    public static Mesh CreatePipe(int numSegsRound, PipeSegment[] segments) {
        Mesh m = new Mesh();
        m.name = "mesh";
        m.Clear();

        Vector3[] vs = new Vector3[(int)(segments.Length*numSegsRound)*6];
        Vector2[] us = new Vector2[(int)(segments.Length*numSegsRound)*6];
        int[] tri = new int[(int)(segments.Length*numSegsRound)*6];
        
        int count = 0;
        Vector3 xAxis = new Vector3(1.0f, 0.0f, 0.0f);

        for (int i = 0; i < segments.Length; i++){
            for (int j = 0; j < numSegsRound; j++){
                float angle = (360 / numSegsRound) * j;
                float angle2 = (360 / numSegsRound) * (j + 1);

                Vector3 directionStart = segments[i]._End - segments[i]._Start;

                if (i > 0) {
                    directionStart = segments[i-1]._End - segments[i-1]._Start;
                }
                directionStart = directionStart.normalized;

                Vector3 zDirStart = Vector3.Cross(directionStart, xAxis).normalized;
                
                Vector3 directionEnd = (segments[i]._End - segments[i]._Start).normalized;
                Vector3 zDirEnd = Vector3.Cross(directionEnd, xAxis).normalized;

                Vector3 normalStart1 = Quaternion.AngleAxis(angle, directionStart) * zDirStart;
                Vector3 normalStart2 = Quaternion.AngleAxis(angle2, directionStart) * zDirStart;

                Vector3 normalEnd1 = Quaternion.AngleAxis(angle, directionEnd) * zDirEnd;
                Vector3 normalEnd2 = Quaternion.AngleAxis(angle2, directionEnd) * zDirEnd;

                Vector3 p1 = segments[i]._Start + normalStart1 * segments[i]._StartRadius;
                Vector3 p2 = segments[i]._Start + normalStart2 * segments[i]._StartRadius;
                Vector3 p3 = segments[i]._End + normalEnd1 * segments[i]._EndRadius;
                Vector3 p4 = segments[i]._End + normalEnd2 * segments[i]._EndRadius;


                Debug.DrawLine(segments[i]._Start, p1, Color.red);
                Debug.DrawLine(segments[i]._Start, p2, Color.green);

                Debug.DrawLine(segments[i]._End, p3, Color.blue);
                Debug.DrawLine(segments[i]._End, p4, Color.yellow);

                vs[count] = p1;
                us[count] = new Vector2((float)j / (float)numSegsRound, (float)i / (float)segments.Length);
                tri[count] = count;
                count++;

                
                vs[count] = p2;
                us[count] = new Vector2((float)(j+1) / (float)numSegsRound, (float)i / (float)segments.Length);
                tri[count] = count;
                count++;

                vs[count] = p3;
                us[count] = new Vector2((float)(j+1) / (float)numSegsRound, (float)(i + 1)/ (float)segments.Length);
                tri[count] = count;
                count++;

                vs[count] = p4;
                us[count] = new Vector2((float)(j+1) / (float)numSegsRound, (float)(i + 1)/ (float)segments.Length);
                tri[count] = count;
                count++;

                vs[count] = p3;
                us[count] = new Vector2((float)j / (float)numSegsRound, (float)(i + 1)/ (float)segments.Length);
                tri[count] = count;
                count++;

                vs[count] = p2;
                us[count] = new Vector2((float)j / (float)numSegsRound, (float)i/ (float)segments.Length);
                tri[count] = count;
                count++;
            }
        }

        m.vertices = vs;
        m.uv = us;
        m.triangles = tri;
        m.RecalculateNormals();
        return m;
    }

}