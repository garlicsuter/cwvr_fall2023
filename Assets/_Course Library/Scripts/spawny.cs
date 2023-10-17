using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawny : MonoBehaviour
{
    public GameObject spawnThis;

   public void Spawny()
    {
        for (var i = 0; i < 10; i++)
                {
                    Instantiate(spawnThis, new Vector3(0, 1, 0), Quaternion.identity);
                }
    }

        
            

}
