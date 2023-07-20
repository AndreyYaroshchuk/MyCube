using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private const string ANIMATION_TRIGGER_JUMP = "Jump";
    private const string TAG_PICKUP = "Pickup";
    private const string TAG_UNTAGGED = "Untagged";
    private const string PREFABS_PLATFORM = "TrackGround";

    private AudioSource audioSource;
    private float PlatformRangeLeft = -2f;
    private float PlatformRangeRight = 2f;
    private float timeDestroyCubeStackEffect = 2f;
    private float transformCameraPositionZ;
    

    [SerializeField] private Touchpad touchpad;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private GameOver gameOver;
    [SerializeField] private CollectCubeText collectCubeTextSpawn;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject cubeStackEffect;
    [SerializeField] private Camera _camera;
    [SerializeField] private float speed = 1f;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private BodyParts[] allBodyParts;

    private void Awake()
    {
        Instance = this;
        CubeHolder.OnAddCubeHolder += CubeHolder_OnAddCubeHolder;
        gameOver.OnGameOver += GameOver_OnGameOver;
        audioSource = GetComponent<AudioSource>();
        cubeStackEffect.SetActive(true);
    }
    private void Start()
    {
        Time.timeScale = 0f;

        transformCameraPositionZ = gameObject.transform.position.z - _camera.transform.position.z;
    }
    private void Update()
    {
        Movet();
        MovetCamera();
    }
    private void OnDestroy()
    {
        CubeHolder.OnAddCubeHolder -= CubeHolder_OnAddCubeHolder;
    }
    private void GameOver_OnGameOver(object sender, EventArgs e)
    {
        int i = UnityEngine.Random.Range(1, audioClips.Count);
        audioSource.PlayOneShot(audioClips[i]);
        animator.enabled = false;
        this.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        foreach (var item in allBodyParts)
        {
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.GetComponent<Rigidbody>().AddForce(transform.forward * 200f);
        }
    }
    private void CubeHolder_OnAddCubeHolder(object sender, EventArgs e)
    {
        CubeStackEffect();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TAG_PICKUP)
        {
            other.gameObject.tag = TAG_UNTAGGED;
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Player.Instance.gameObject.transform.position = new Vector3(Player.Instance.gameObject.transform.position.x, Player.Instance.gameObject.transform.position.y + 1, Player.Instance.gameObject.transform.position.z);
            other.gameObject.transform.position = new Vector3(Player.Instance.gameObject.transform.position.x, Player.Instance.gameObject.transform.position.y - 0.5f, Player.Instance.gameObject.transform.position.z);
            other.GetComponent<CubeHolder>().enabled = true;
            CubeStackEffect();
            collectCubeTextSpawn.SpawnCollectCubeText();

        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == PREFABS_PLATFORM)
        {
            trailRenderer.enabled = true;
        }
        else
        {
            trailRenderer.enabled = false;
        }

    }
    private void MovetCamera()
    {
        _camera.transform.localPosition = new Vector3(_camera.transform.position.x, _camera.transform.position.y, transform.position.z - transformCameraPositionZ);
    }

    private void CubeStackEffect()
    {
        animator.SetTrigger(ANIMATION_TRIGGER_JUMP);
        GameObject gameobjectCubeStackEffect = Instantiate(cubeStackEffect, cubeStackEffect.transform.position, Quaternion.identity);
        audioSource.PlayOneShot(audioClips[0]);
        Destroy(gameobjectCubeStackEffect, timeDestroyCubeStackEffect);
    }
    public void Movet()
    {
        transform.localPosition += Vector3.forward * Speed() * Time.deltaTime;
        if (transform.position.x > PlatformRangeLeft)
        {
            if (touchpad.GetDirection().x <= -0.1f/*Input.GetKey(KeyCode.A)*/)
            {
                transform.localPosition += -transform.right * speed * Time.deltaTime;
            }
        }
        if (transform.position.x < PlatformRangeRight)
        {
            if (touchpad.GetDirection().x >= 0.1f /*Input.GetKey(KeyCode.D)*/)
            {
                transform.localPosition += transform.right * speed * Time.deltaTime;
            }
        }
    }
    public float Speed()
    {
        return speed;  
    }
}
