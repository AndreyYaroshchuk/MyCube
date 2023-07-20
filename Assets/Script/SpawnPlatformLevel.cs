using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlatformLevel : MonoBehaviour
{
    private float positionSpawnPlatformLevel = 30f;
    private float speed = 5f;
    private GameObject newspawnPlatformLevel;
    private float timeDestroySpawnPlatformLevel = 10f;

    [SerializeField] private List<GameObject> spawnPlatformLevel;

    private void Update()
    {
        MovetNewSpawnPlatformLevel();
        Movet();
        DestroyPlatformLevel();
    }
    private void Movet()
    {
        if (Player.Instance.transform.position.z >= transform.position.z - positionSpawnPlatformLevel)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + positionSpawnPlatformLevel);
            SpawnsPlatformLevel();
        }
    } 
    private void SpawnsPlatformLevel()
    {
        int i = Random.Range(0, spawnPlatformLevel.Count);
        newspawnPlatformLevel = Instantiate(spawnPlatformLevel[i], transform.position, Quaternion.identity);      
    }
    private void DestroyPlatformLevel()
    {
        Destroy(newspawnPlatformLevel, timeDestroySpawnPlatformLevel);
    } 
    public void MovetNewSpawnPlatformLevel()
    {
        if (newspawnPlatformLevel != null)
        {
            if (newspawnPlatformLevel.transform.position.y <= 0f)
            {
                newspawnPlatformLevel.transform.position += Vector3.up * speed * Time.deltaTime;
            }
            if (newspawnPlatformLevel.transform.position.y >= 0f)
            {
                newspawnPlatformLevel.transform.position = new Vector3 (newspawnPlatformLevel.transform.position.x, 0 ,newspawnPlatformLevel.transform.position.z);
            }
        }
    }

}
