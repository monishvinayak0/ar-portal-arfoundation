

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour
{

    //This materials matter needs to be optimizated!
    public Material[] materials;

    private Vector3 camPostionInPortalSpace;

    bool wasInFront;
    bool inOtherWorld;

    bool hasCollided;

    public GameObject[] objects;
   public static int triggerTime ;
    // Start is called before the first frame update
    void Start()
    {
       // SetMaterials(false);
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    void SetMaterials(bool fullRender)
    {
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;

        foreach (var mat in materials)
        {
            mat.SetInt("_StencilComp", (int)stencilTest);
        }
    }

    //Set bidirectional function
    bool GetIsInFront()
    {
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 worldPos = MainCamera.transform.position + MainCamera.transform.forward * Camera.main.nearClipPlane;
        camPostionInPortalSpace = transform.InverseTransformPoint(worldPos);
        return camPostionInPortalSpace.y >= 0 ? true : false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (collider.transform != MainCamera.transform)
            return;
        wasInFront = GetIsInFront();
        hasCollided = true;

      

      


    }

    // Update is called once per frame
    void OnTriggerExit(Collider collider)
    {
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (collider.transform != MainCamera.transform)
            return;
        hasCollided = false;
        triggerTime++;
       
    }

    void whileCameraColliding()
    {
        if (!hasCollided)
            return;
        bool isInFront = GetIsInFront();
        if ((isInFront && !wasInFront) || (wasInFront && !isInFront))
        {
            inOtherWorld = !inOtherWorld;
            SetMaterials(inOtherWorld);
           
        }
        wasInFront = isInFront;
    }

    private void OnDestroy()
    {
        SetMaterials(true);
    }

    private void Update()
    {
        whileCameraColliding();
        if(triggerTime%2 == 0)
        {
            foreach (GameObject obj in objects)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject obj in objects)
            {
                obj.SetActive(true);
            }
        }
    }

}
