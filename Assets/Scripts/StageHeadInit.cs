using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHeadInit : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject stageHead;

    // Update is called once per frame
    void Start()
    {
        
      Instantiate(stageHead, transform.position, Quaternion.identity);
        
    }
}
