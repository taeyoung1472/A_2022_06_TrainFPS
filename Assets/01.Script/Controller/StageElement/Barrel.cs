using DG.Tweening;
using UnityEngine;

public class Barrel : ElementAudioFeedBack
{
    [SerializeField] private GameObject defaultBarrel;
    [SerializeField] private GameObject destroyedBarrel;
    [SerializeField] private AudioClip explosionClip;

    bool isDestroyed= false;
    Rigidbody rb = null;
    MeshRenderer meshRenderer = null;
    float hp = 100;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = destroyedBarrel.GetComponent<MeshRenderer>();
    }
    public override void Hit(float power, Vector3 shootPos)
    {
        base.Hit(power, shootPos);
        hp -= power;
        if (hp <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(explosionClip, 1, Random.Range(0.75f, 1.25f));
            defaultBarrel.SetActive(false);
            destroyedBarrel.SetActive(true);
            Vector2 randXZ = Random.insideUnitCircle;
            Vector3 explosionForce = new Vector3(randXZ.x, 5, randXZ.y);
            rb.AddTorque(explosionForce * 10, ForceMode.Impulse);
            rb.AddForce(explosionForce, ForceMode.Impulse);
            foreach (var mat in meshRenderer.materials)
            {
                mat.DOColor(mat.color * new Color(0.25f, 0.25f, 0.25f,1), 1f);
            }
        }
    }
}
