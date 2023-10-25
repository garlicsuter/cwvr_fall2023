using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawny : MonoBehaviour
{
    public GameObject spawnThis;

   public void SpawnyApples()
    {
        for (var i = 0; i < 10; i++)
                {
                    Instantiate(spawnThis, new Vector3(0, 1, 0), Quaternion.identity);
                }
    }

        
            

}
