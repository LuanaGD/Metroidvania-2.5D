using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LayerChange : MonoBehaviour
{
    public GameObject destination;
    public Transform arrivalPoint;
    public GameObject player;
    public GameObject inputText;

    private PlayerController3D playerController;

    public bool layerChangeOk;

    [SerializeField]
    private float changeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController3D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (Input.GetKeyDown(KeyCode.A) && layerChangeOk)
        {
            //player.SetActive(false);
            player.transform.position = Vector3.MoveTowards(player.transform.position, destination.transform.position, changeSpeed);
            //player.SetActive(true);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("touching");
            inputText.SetActive(true);
            layerChangeOk = true;
            //player.SetActive(false);
            StartCoroutine(TeleportTime());
            player.transform.position = destination.transform.position;
            //player.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inputText.SetActive(false);
            layerChangeOk = false;
        }
    }

    private IEnumerator TeleportTime()
    {
        playerController.canControl = false;
        playerController.MoveSpeed = 0f;
        Debug.Log("Teleported");
        yield return new WaitForSeconds(changeSpeed);
    }
}
