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

    public void Fire(Vector3 player_last_position)
    {

        if (Time.time >= lastFireTime + timeBetFire)
        {
            lastFireTime = Time.time;
            Shot(player_last_position);
        }

    }

    private void Shot(Vector3 player_last_position)
    {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        // 플레이어의 마지막 위치를 사용해 발사 방향 계산
        Vector3 directionToPlayer = player_last_position - fireTransform.position;
        directionToPlayer.Normalize();  // 방향 벡터 정규화

        // 플레이어의 위치를 향해 발사
        if (Physics.Raycast(fireTransform.position, directionToPlayer, out hit, fireDistance))
        {
            hitPosition = hit.point;
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
            // 플레이어가 범위 내에 없다면 최대 거리까지 발사
            hitPosition = fireTransform.position + directionToPlayer * fireDistance;
        }

        // 총구가 활성화되어 있는지 확인 후 효과 실행
        if (gameObject.activeInHierarchy)
        {
            ShotEffect(hitPosition);
        }
    }

    private void ShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();
        shellEjectEffect.Play();
        gunAudioPlayer.PlayOneShot(shotClip);

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        // Invoke를 사용하여 총알 궤적을 지연 삭제
        Invoke("DisableBulletLine", 0.03f);
    }

    private void DisableBulletLine()
    {
        bulletLineRenderer.enabled = false;
    }

}
