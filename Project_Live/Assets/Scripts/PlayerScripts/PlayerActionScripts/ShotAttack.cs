using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者：桑原

public class ShotAttack : MonoBehaviour
{
    [Header("発射する弾")]
    [SerializeField] GameObject bulletPrefab;
    [Header("発射位置")]
    [SerializeField] Transform shotPos;
    [Header("弾の速度")]
    [SerializeField] float bulletSpeed;
    [Header("発射までの時間")]
    [SerializeField] float chargeTime = 0.5f;
    [Header("発射後、入力を再度受け付けるまでの時間")]
    [SerializeField] float shotInterval = 0.5f;
    [Header("弾を発射してから他の状態に遷移するまでの時間")]
    [SerializeField] float changeStateInterval = 0.5f;
    [Header("移動中、弾を発射できるか")]
    [SerializeField] bool canMovingShot = false;    

    float timeSinceLastShot = 0f;
    bool isCharging = false;
    bool hasPlayAnim = false;
    float currentChargeTime = 0f;

    public float ShotInterval { get { return shotInterval; } }
    public float ChangeStateInterval { get { return changeStateInterval; } }
    public bool CanMovingShot { get { return canMovingShot; } }
    public float TimeSinceLastShot { get { return timeSinceLastShot; } set { timeSinceLastShot = value; } }
    public bool IsCharging { get { return isCharging; } }
    public bool HasPlayAnim { get {  return hasPlayAnim; } set { hasPlayAnim = value; } }

    public void TryShot()
    {
        if (timeSinceLastShot < shotInterval || isCharging) return;

        hasPlayAnim = true;
        isCharging = true;
        currentChargeTime = 0f;
    }

    public void ShotAttackProcess()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!isCharging) return;

        currentChargeTime += Time.deltaTime;

        if (currentChargeTime >= chargeTime)
        {
            ShotBullet(); //一定時間経過したら発射
            isCharging = false;
        }
    }

    void ShotBullet() //弾の生成・加速処理
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPos.transform.position, shotPos.transform.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = shotPos.forward * bulletSpeed;

        timeSinceLastShot = 0f;
    }
}
