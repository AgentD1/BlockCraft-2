using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Rigidbody rb;
    public MeshTest meshTest;
    public float movementSpeed = 4f;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;
    public float minimumX = -360f;
    public float maximumX = 360f;
    public float minimumY = 0f;
    public float maximumY = 180f;
    public float jumpPower = 2.1f;
    public GameObject cameraGameObject;
    float rotationX = 0f;
    float rotationY = 0f;
    Quaternion originalRotation;
    System.Type equippedBlockType = typeof(GrassBlock);

    public Text equippedText;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        originalRotation = rb.rotation;
    }

    // Update is called once per frame
    void Update () {
        Vector3 movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            movementVector += transform.forward;
        }
        if (Input.GetKey(KeyCode.S)) {
            movementVector += -transform.forward;
        }
        if (Input.GetKey(KeyCode.A)) {
            movementVector += -transform.right;
        }
        if (Input.GetKey(KeyCode.D)) {
            movementVector += transform.right;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            equippedBlockType = typeof(GrassBlock);
            equippedText.text = "Grass";
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            equippedBlockType = typeof(DirtBlock);
            equippedText.text = "Dirt";
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            equippedBlockType = typeof(GlassBlock);
            equippedText.text = "Glass";
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            equippedBlockType = typeof(StoneBlock);
            equippedText.text = "Stone";
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            equippedBlockType = typeof(GlowstoneBlock);
            equippedText.text = "Glowstone";
        }

        movementVector.Normalize();
        movementVector *= movementSpeed;

        if (Input.GetKey(KeyCode.Space) && rb.velocity.y < 0.001f) {
            movementVector += Vector3.up * jumpPower;
        }

        rb.velocity = new Vector3(movementVector.x, rb.velocity.y + movementVector.y, movementVector.z);

        //TODO: Make this original, credit https://answers.unity.com/questions/29741/mouse-look-script.html
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.Euler(rotationX * Vector3.up);
        Quaternion yQuaternion = Quaternion.Euler(rotationY * -Vector3.right);
        rb.MoveRotation(originalRotation * xQuaternion);
        cameraGameObject.transform.rotation = transform.rotation * yQuaternion;


        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Display.main.renderingWidth / 2, Display.main.renderingHeight / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000)) {
                if (!Physics.CheckBox(new Vector3(Mathf.Round(hit.point.x + (hit.normal.x * 0.01f)), Mathf.Round(hit.point.y + (hit.normal.y * 0.01f)), Mathf.Round(hit.point.z + (hit.normal.z * 0.01f))), Vector3.one * 0.45f)) {
                    if (hit.collider.gameObject.CompareTag("Block")) {
                        IBlock block = (IBlock)System.Activator.CreateInstance(equippedBlockType, new object[] { new Vector3(Mathf.Round(hit.point.x + (hit.normal.x * 0.01f)), Mathf.Round(hit.point.y + (hit.normal.y * 0.01f)), Mathf.Round(hit.point.z + (hit.normal.z * 0.01f))) }); //new DirtBlock(new Vector3(Mathf.Round(hit.point.x + (hit.normal.x * 0.01f)), Mathf.Round(hit.point.y + (hit.normal.y * 0.01f)), Mathf.Round(hit.point.z + (hit.normal.z * 0.01f))));
                        BlockManager.AddBlock(block);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Display.main.renderingWidth / 2, Display.main.renderingHeight / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000)) {
                Vector3 blockPos = new Vector3(Mathf.Round(hit.point.x + (hit.normal.x * -0.01f)), Mathf.Round(hit.point.y + (hit.normal.y * -0.01f)), Mathf.Round(hit.point.z + (hit.normal.z * -0.01f)));
                BlockManager.RemoveBlock(blockPos);
            }
        }



    }

    //also make original, https://answers.unity.com/questions/29741/mouse-look-script.html
    public float ClampAngle(float angle, float min, float max) {
        if (angle < -360F)
         angle += 360F;
        if (angle > 360F)
         angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

}
