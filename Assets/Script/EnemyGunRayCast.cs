using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunRayCast : MonoBehaviour
{

    public Transform fireTransform; 

    public ParticleSystem muzzleFlashEffect; 
    public ParticleSystem shellEjectEffect; 

    private LineRenderer bulletLineRenderer; 

    private AudioSource gunAudioPlayer; 
    public AudioClip shotClip; 

    public float damage = 25; 
    private float fireDistance = 50f; 

    public float timeBetFire = 0.12f;
    public float reloadTime = 1.8f; 
    private float lastFireTime; 

    private void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    public void Fire()
    {

        if (Time.time >= lastFireTime + timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
        }

    }

    private void Shot()
    {
        RaycastHit hit;

        Vector3 hitPosition = Vector3.zero;
        
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            
            if (hit.collider.CompareTag("Player"))
            {
                if (CameraController.Instance != null)
                {
                    CameraController.Instance.DecreaseViewOnDamage();
                }


            }
        }

        else
        {
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));

    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {

        muzzleFlashEffect.Play();
        shellEjectEffect.Play();

        gunAudioPlayer.PlayOneShot(shotClip);

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);


        // 라인 렌더러를 활성화하여 총알 궤적을 그린다
        bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;
    }

}
