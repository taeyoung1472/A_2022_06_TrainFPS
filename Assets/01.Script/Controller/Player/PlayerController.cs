using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region �̵����� ����
    [Header("�̵����� ��ġ")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 2.5f;
    Vector3 moveDir;
    #endregion

    #region ȸ������ ����
    [Header("ȸ������ ��ġ")]
    [SerializeField] private Transform cam;
    [SerializeField] private Vector2 camLimit = new Vector2(-45f, 45f);
    [SerializeField] private float senservity = 500f;
    [SerializeField] private AnimationCurve recoilControllCurve;
    private Vector2 recoil;
    private float fixSenservityValue = 1;
    private float camY;
    private bool useFixValue = false;
    #endregion

    #region GetSet ��������
    public float FixSenservityValue { set { fixSenservityValue = value; } }
    public bool UseFixValue { set { useFixValue = value; } }
    #endregion

    #region �̵����� ����
    CharacterController cc;
    #endregion
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        float inputX = (mouseX + recoil.x) * (useFixValue ? fixSenservityValue : 1)
            * senservity * Time.deltaTime;
        float inputY = (mouseY + recoil.y) * (useFixValue ? fixSenservityValue : 1)
            * senservity * Time.deltaTime;
        transform.rotation = transform.rotation * Quaternion.Euler(0, inputX, 0);
        camY += inputY;
        camY = Mathf.Clamp(camY, camLimit.x, camLimit.y);
        cam.eulerAngles = new Vector3 (camY, cam.eulerAngles.y, cam.eulerAngles.z);
    }
    public void Recoil(Vector2 value, float time)
    {
        StartCoroutine(Recol(time, value));
    }
    IEnumerator Recol(float time, Vector2 value)
    {
        Camera camera = cam.GetComponent<Camera>();
        float defaultFOV = camera.fieldOfView;
        camera.fieldOfView = defaultFOV * 1.035f;
        float fixValue = 1f / time;
        float curTime = 0;
        while (1 >= curTime)
        {
            curTime += Time.deltaTime * fixValue;
            recoil.y = recoilControllCurve.Evaluate(curTime) * value.y * 0.1f;
            recoil.x = recoilControllCurve.Evaluate(curTime) * value.x * 0.1f;
            yield return null;
        }
        recoil = Vector2.zero;
        camera.fieldOfView = defaultFOV;
    }
}
