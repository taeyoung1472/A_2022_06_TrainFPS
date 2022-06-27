using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHitAble
{
    #region 생명관련 변수
    [SerializeField] private float maxHp;
    [SerializeField] private ImageSlider slider;
    [SerializeField] private List<Armor> armorList;
    float hp;
    #endregion

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
    [SerializeField] private AnimationCurve recoilControllCurve;
    [SerializeField] private Transform camAxis;
    private Vector2 recoil;
    private float fixSenservityValue = 1;
    private float camY;
    private float camZ;
    private bool useFixValue = false;
    private bool isCamRight;
    private bool isCamLeft;
    #endregion

    #region GetSet 프로피터
    public float FixSenservityValue { set { fixSenservityValue = value; } }
    public bool UseFixValue { set { useFixValue = value; } }
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
        Cursor.lockState = CursorLockMode.Locked;
        hp = maxHp;
        foreach (var armor in armorList)
        {
            armor.Set();
        }
    }
    void Update()
    {
        Controll();
        Rotate();
        CheckInput();
    }

    private void CheckInput()
    {
        float goalCamZ;
        if (Input.GetKey(KeyCode.Q))
        {
            goalCamZ = 20;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            goalCamZ = -20;
        }
        else
        {
            goalCamZ = 0;
        }
        camZ = Mathf.Lerp(camZ, goalCamZ, Time.deltaTime * 5);
        camAxis.localEulerAngles = new Vector3(0, 0, camZ);
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

    public void Hit(float power, Vector3 shootPos)
    {
        print($"{power}의 힘으로 공격받음");
        for (int i = armorList.Count - 1; i >= 0; i--)
        {
            if (armorList[i].GetDamage(power))
            {
                return;
            }
        }
        hp -= power;
        slider.SetValue(hp, maxHp);
        if(hp <= 0)
        {
            print("나 죽었어!");
        }
    }
}

[Serializable]
public class Armor
{
    float maxHp = 50;
    public float hp = 50;
    public ImageSlider slider;
    public void Set()
    {
        hp = maxHp;
    }
    public bool GetDamage(float damage)
    {
        hp -= damage;
        slider.SetValue(hp, maxHp);
        if (hp <= 0)
        {
            return false;
        }
        return true;
    }
    public float Repair(float repairAmount)
    {
        float returnValue = 0;

        hp += repairAmount;
        if(hp > maxHp)
        {
            returnValue = hp - maxHp;
            hp = maxHp;
        }
        slider.SetValue(hp, maxHp);
        return returnValue;
    }
}