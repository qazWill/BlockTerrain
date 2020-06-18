using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{

    public GameObject blockPrefab;

    private GameObject[,,] blocks = new GameObject[10, 10, 10];

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int z = 0; z < 10; z++)
                {
                    blocks[x,y,z] = Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.identity) as GameObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
