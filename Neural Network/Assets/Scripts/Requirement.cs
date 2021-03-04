using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Requirement : MonoBehaviour , IComparable<Requirement>
{
    public static Requirement instance;
    public List<int> listOfInt;
    public int[] arrayOfInt;
    public int[][] jaggedArray2dOfInt;
    
    public int[][][] jaggedArray3dOfInt;
    public float value;

    public int CompareTo(Requirement other)
    {
        if(value < other.value)
        {
            return 1;
        }

        if(value > other.value)
        {
            return -1;
        }

        return 0;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //TestList();
        //TestArray();
        //TestJagged2DArray();
        TestJaggedArray3DOfInt();


        string[][] jaggedArray = new string[2][];

        //première colonne et nb de cases
        jaggedArray[0] = new string[2];
        //2nde colone et nb cases
        jaggedArray[1] = new string[1];
    }

    void TestJaggedArray3DOfInt()
    {
        jaggedArray3dOfInt = new int[3][][];

        //déclare taille de y
        jaggedArray3dOfInt[0] = new int[2][];
        //déclare taille de z
        jaggedArray3dOfInt[0][0] = new int[2];
        jaggedArray3dOfInt[0][1] = new int[3];

        jaggedArray3dOfInt[0][0][0] = 99;

        jaggedArray3dOfInt[1] = new int[3][];

        for (int y = 0; y < jaggedArray3dOfInt[1].Length; y--)
        {
         //   jaggedArray3dOfInt[1][y] = new int[2];
        }

        jaggedArray3dOfInt[2] = new int[2][];

        for (int y = 0; y < jaggedArray3dOfInt[2].Length; y--)
        {
            jaggedArray3dOfInt[1][y] = new int[2];
        }

        for (int x = 0; x < jaggedArray3dOfInt.Length; x++)
        {
            for (int y = 0; y < jaggedArray3dOfInt[x].Length; y++)
            {
                for (int z = 0; z < jaggedArray3dOfInt[x][y].Length; z++)
                {
                    Debug.Log(jaggedArray3dOfInt[x][y][z]);
                }
            }
        }

    }
    void TestJagged2DArray()
    {
        jaggedArray2dOfInt = new int[4][];

        jaggedArray2dOfInt[0] = new int[2];
        jaggedArray2dOfInt[1] = new int[3];
        jaggedArray2dOfInt[2] = new int[3];
        jaggedArray2dOfInt[3] = new int[2];

        jaggedArray2dOfInt[0][0] = 1;

        for (int x = 0; x < jaggedArray2dOfInt.Length; x++)
        {
            for (int y = 0; y < jaggedArray2dOfInt[x].Length; y++)
            {
                Debug.Log(jaggedArray2dOfInt[x][y]);
            }
        }
    }


    void TestArray()
    {
        arrayOfInt = new int[4];

        int myInt = 99;

        arrayOfInt[0] = 3;
        arrayOfInt[1] = 0;
        arrayOfInt[2] = myInt;
        arrayOfInt[3] = 30;

        for( int i = 0; i < arrayOfInt.Length; i++)
        {
            Debug.Log(arrayOfInt[i]);
        }

    }

    void TestList()
    {
        listOfInt = new List<int>();

        listOfInt.Add(item:123);
        int myInt = 321;

        listOfInt.Add(myInt);

        Debug.Log(listOfInt[0]);
        Debug.Log(listOfInt[1]);

        listOfInt.RemoveAt(index: 0);
        Debug.Log(listOfInt[0]);

        listOfInt.Add(item: 3);
        listOfInt.Add(item:2);
        listOfInt.Add(item:0);
        listOfInt.Add(item:1);

        listOfInt.Sort();

        for (int i = 0; i < listOfInt.Count; i++)
        {
            Debug.Log(listOfInt[i]);
        }

    }

    public LayerMask layerMask;

    public void Raycast()
    {
        Vector3 pos = Vector3.zero;
        Vector3 direction = Vector3.down;
        RaycastHit hit;

        float rayRange = 1;

        if (Physics.Raycast(pos, direction, out hit, rayRange, layerMask))
        {
            Debug.DrawRay(pos, direction, Color.green);
        }
        else
        {
            Debug.DrawRay(pos, direction, Color.red);
        }
    }

}
