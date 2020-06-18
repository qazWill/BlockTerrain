using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{

    public GameObject chunkPrefab;
    public GameObject player;

    private GameObject[,,] chunks = new GameObject[5, 5, 5];

    private Vector3 lastChunkOrigin;

    // Start is called before the first frame update
    void Start()
    {
        // saves this position
        lastChunkOrigin = getOriginChunk();

        // initially creates chunks surrounding player
        for (int x = 0; x < chunks.GetLength(0); x++)
        {
            for (int y = 0; y < chunks.GetLength(1); y++)
            {
                for (int z = 0; z < chunks.GetLength(2); z++)
                {
                    // this is the position of the current chunk
                    Vector3 offset = new Vector3(x * 10,y * 10, z * 10);
                    Vector3 pos = lastChunkOrigin + offset;

                    // instantiates chunk from prefab
                    chunks[x, y, z] = Instantiate(chunkPrefab, pos, Quaternion.identity) as GameObject;
                }
            }
        }


    }


    private void Update()
    {

        // gets current chunk origin
        Vector3 currentChunkOrigin = getOriginChunk();

        // finds difference between last check
        Vector3 offset = currentChunkOrigin - lastChunkOrigin;

        // if they are different some chunks need to be deleted
        // and some chunks need to be loaded
        if (offset != Vector3.zero)
        {
            //refreshChunks(offset);
        }


        // updates chunk origin
        lastChunkOrigin = currentChunkOrigin;

    }

    private Vector3 getOriginChunk()
    {
        // finds how many chunks player is from edge(he's in center)
        int centerOffset = chunks.GetLength(0) / 2;

        // finds coordinates of chunk(0, 0, 0)
        int chunkX = (int)player.transform.position.x - ((int)player.transform.position.x % 10) - centerOffset * 10;
        int chunkY = (int)player.transform.position.y - ((int)player.transform.position.y % 10) - centerOffset * 10;
        int chunkZ = (int)player.transform.position.z - ((int)player.transform.position.z % 10) - centerOffset * 10;

        // negative modulus needs to be shifted
        if ((int)player.transform.position.x < 0)
        {
            chunkX -= 10;
        }
        if ((int)player.transform.position.y < 0)
        {
            chunkY -= 10;
        }
        if ((int)player.transform.position.z < 0)
        {
            chunkZ -= 10;
        }

        // saves this position
        return new Vector3(chunkX, chunkY, chunkZ);
    }


    private void refreshChunks(Vector3 offset)
    {
        // repeated shifts until no more offset
        while (offset != Vector3.zero)
        {
            int i = 0;
            int start = 0;
            int end = chunks.GetLength(0);
            int change = -1;
            int xyzChoice = 0;
            int x, y, z;

            // chooses coordinate and direction
            if (offset.x > 0)
            {
                offset.x -= 1;
                xyzChoice = 0;
            }
            else if (offset.x < 0)
            {
                offset.x += 1;
                xyzChoice = 0;
                start = end;
                end = -1;
            }
            else if (offset.y > 0)
            {
                offset.y -= 1;
                xyzChoice = 1;
            }
            else if (offset.y < 0)
            {
                offset.y += 1;
                xyzChoice = 1;
                start = end;
                end = -1;
            }
            else if (offset.z > 0)
            {
                offset.z -= 1;
                xyzChoice = 2;
            }
            else if (offset.z < 0)
            {
                offset.z += 1;
                xyzChoice = 2;
                start = end;
                end = -1;
            }
            else
            {
                break;
            }

            i = start;
            if (start == 0)
            {
                change = 1;
            }
            while (i != end)
            {
                for (int j = 0; j < chunks.GetLength(0); j++)
                {
                    for (int k = 0; k < chunks.GetLength(0); k++)
                    {

                        // selects dimensions
                        if (xyzChoice == 0)
                        {
                            x = i;
                            y = j;
                            z = k;
                            
                        }
                        else if (xyzChoice == 1)
                        {
                            x = j;
                            y = i;
                            z = k;
                        }
                        else
                        {
                            x = j;
                            y = k;
                            z = i;
                        }


                        // delete
                        if (i == start)
                        {
                            Destroy(chunks[x, y, z]);
                            chunks[x, y, z] = null;
                        }

                        // load
                        if (i == end - change)
                        {
                            // load a chunk
                        }


                        // shift
                        else
                        {
                            GameObject nextChunk;
                            if (xyzChoice == 0)
                            {
                                nextChunk = chunks[x + change, y, z];
                            }
                            else if(xyzChoice == 1)
                            {
                                nextChunk = chunks[x, y + change, z];
                            }
                            else
                            {
                                nextChunk = chunks[x, y, z + change];
                            }

                            chunks[x, y, z] = nextChunk;
                        }
                   
                    }
                }

                // step
                i += change;
            }
        }
    }
}
