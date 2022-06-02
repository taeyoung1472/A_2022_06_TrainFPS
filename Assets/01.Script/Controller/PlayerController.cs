using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 이동관련 변수
    [Header("이동관련 수치")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 2.5f;
    Vector3 moveDir;
    #endregion

    #region 회전관련 변수
    [Header("회전관련 수치")]
    [SerializeField] private Transform cam;
    [SerializeField] private Vector2 camLimit = new Vector2(-45f, 45f);
    [SerializeField] private float senservity = 500f;
    float camY;
    #endregion

    #region 이동관련 변수
    CharacterController cc;
    #endregion
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        Controll();
        Rotate();
    }
    void Controll()
    {
        if (cc.isGrounded)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            moveDir = new Vector3(h, 0, v);

            moveDir = transform.TransformDirection(moveDir);

            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jumpForce;
            }
        }

        moveDir.y += Physics.gravity.y * Time.deltaTime;

        cc.Move(moveDir * Time.deltaTime * speed);
    }
    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        transform.rotation = transform.rotation * Quaternion.Euler(0, mouseX * Time.deltaTime * senservity, 0);//Rotate(mouseX * Time.deltaTime * senservity * Vector3.up);
        camY += mouseY * senservity * Time.deltaTime;
        camY = Mathf.Clamp(camY, camLimit.x, camLimit.y);
        cam.eulerAngles = new Vector3 (camY, cam.eulerAngles.y, cam.eulerAngles.z);
    }
}
