using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCubeText : MonoBehaviour
{
    private float timeDestroyCollectCubeText = 2f;

    [SerializeField] GameObject collectCubeText;

    private void Start()
    {
        CubeHolder.OnAddCubeHolder += CubeHolder_OnAddCubeHolder;
    }
    private void OnDestroy()
    {
        CubeHolder.OnAddCubeHolder -= CubeHolder_OnAddCubeHolder;
    }
    private void CubeHolder_OnAddCubeHolder(object sender, System.EventArgs e)
    {
        SpawnCollectCubeText();
        
    } 
    public void SpawnCollectCubeText()
    {
        GameObject newCollectCubeText = Instantiate(collectCubeText, transform.position, Quaternion.identity);
        Destroy(newCollectCubeText, timeDestroyCollectCubeText);
    }


}
