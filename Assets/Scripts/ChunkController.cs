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
        for (int x = 0; x < 10; x += 9)
        {
            for (int y = 0; y < 10; y += 9)
            {
                for (int z = 0; z < 10; z += 9)
                {
                    blocks[x,y,z] = Instantiate(blockPrefab, new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z), Quaternion.identity) as GameObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
