# Computer Graphics Term Project Proposal

최초 작성: 2024.12.11

최종 수정: 2024.12.24

*이하 내용은 지속적으로 추가 예정입니다.*

\-

**Quaternion**

송승우, 장민준, 장승우, 최세영

---

## 결정해야되는 사항들

게임 스타일을 **1인칭**(FPS), **3인칭(TPS)**, **혼합형** 중 어떤 방식으로 구현할 지 결정합니다.

기획 과정에서 저희가 구현하는 주제의 게임은 오히려 3인칭 게임으로 만드는 것이 더 좋을 수도 있다는 생각이 들어 각각 장단점 확인하시고 결정해주시면 좋을 것 같습니다!!!

세 개의 스타일 모두 필수 구현 사항을 만족하는 데에는 무리가 없습니다.

- **1인칭**(FPS): 플레이어가 드론의 시야를 직접 경험하게 되어 제한된 시야와 긴장감을 극대화할 수 있으나, near plane을 넓게 만들어 구현하는 것이 게임 카메라 상으로는 어색하게 보이거나 플레이어가 가까운 오브젝트를 제대로 볼 수 없어 게임 플레이가 답답하게 느껴질 수 있습니다.

- **3인칭**(TPS): 그래픽적 구현의 참신함은 조금 떨어지지만, 플레이어가 적을 관찰하며 전략을 세우는 것을 즐기게 만들 수 있고 캐주얼한 재미를 만들 수 있습니다. 사격 구현시에도 y축 구현을 신경쓰지 않아도 되어 구현이 조금 편해질 수 있습니다(마우스 방향 사격으로 구현 가능).
![Bullet Echo](https://play-lh.googleusercontent.com/4yhl7slTkrhw2k2cnpHfEv7LokpLdxxOaUb5AFdsGTpqhOIhwvXoK4PRorwP9FPv3zk=w526-h296-rw)

- **혼합형**: 개인적으로 가장 추천하는 방식입니다. 기본적으로 3인칭 방식으로 제작하되, 슈팅 시에는 1인칭으로 현재 보고 있는 방향에 따라 제한된 범위 내에서 카메라를 돌려 보이는 적 물체에 사격할 수 있도록 합니다. 3인칭-1인칭 간 전환에는 시네머신(Cinemachine)을 사용합니다.

  - V키를 통해 1인칭, 3인칭 카메라를 전환하는 예시 코드

```csharp
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;

    private void Update()
    {
        // 예시: 'V' 키를 눌러 3인칭/1인칭 전환
        if (Input.GetKeyDown(KeyCode.V))
        {
            bool isThirdPersonActive = thirdPersonCamera.gameObject.activeSelf;

            // 3인칭 카메라를 끄고 1인칭 카메라를 켬
            thirdPersonCamera.gameObject.SetActive(!isThirdPersonActive);
            firstPersonCamera.gameObject.SetActive(isThirdPersonActive);
        }
    }
}
```


목표 서류를 하나로 고정할 지, 3~5개 정도의 여러 서류들을 모아서 가져오는 것으로 하는 것이 좋을 지 결정해야 할 것 같습니다. 만약 여러개의 서류를 가져오는 것으로 하면 탄환 개수처럼 플레이어 화면에 현재 모은 서류 개수를 표현해주는 것이 좋겠습니다.


추가로 아이템은 사격 가능 총알 수를 제외하고 인벤토리 없이 흭득하면 바로 사용되도록 구현하는 것이 더 좋을 것 같은데 여러분 의견이 어떤지 궁금합니당


마지막에 생각해도 되긴 한데 게임 제목은 어떻게 할지도 생각해야 할 것 같습니다.

---

## 메모 + 각종 유용한 정보들

*프로젝트를 진행하며 지속적으로 추가해주세요!!*


### Unity Render Pipeline Compatibility

Unity는 **Built-in Renderer Pipeline**, **URP(Universal Render Pipeline)**, **HDRP(High Definition Render Pipeline)** 등 다양한 렌더링 파이프라인을 지원.

**Built-in Renderer Pipeline**은 빠르고 간단한 게임/레거시 프로젝트에 활용, **URP**는 **범용 렌더링 파이프라인**으로 일반적인 모바일/인디 게임에서 활용, **HDRP**는 AAA급 게임/고품질 시각화에 활용.

'**Edit > Render Pipeline > Upgrade Project Materials**'를 통해 Built-in, URP, HDRP 간 변환이 가능은 하나 완전하지 않을 수 있음.

이번 프로젝트에서는 범용적으로 사용되는 URP를 기준으로 제작할 예정이나 에셋 변환 후 크게 오류가 없음을 확인했다면 HDRP도 활용 가능. 에셋 변환 등 사전 작업을 최소화하기 위해 에셋 선택 시 사용중인 Unity의 버전과 렌더링 파이프 지원 여부를 확인하는 것이 좋음.


### git 협업 관련

- commit할 때 양식은 상관없으니 간단하게라도 어떤 부분을 추가/수정한 버전인지 적어주세용!!

- 원 프로젝트 리포에 branch 만들어서 작업하셔도 되구 포크해서 따로 작업 후 pull request 보내셔도 됩니다. 저는 둘 다 해서 pull request 드릴게용. merge는 원 프로젝트 리포에서만 진행해주세요!

- [참고 자료](https://wayhome25.github.io/git/2017/07/08/git-first-pull-request-story/)


### Unity 기본 용어 정리

- [참고 자료](https://opentutorials.org/module/3158/18580)

---

## 프로젝트 개요

**시야가 제한되어있는 드론이 연구소/사무실에 잠입하여 중요 서류를 가지고 다시 탈출하는 잠입 액션 게임**을 구현합니다.

드론의 **시야**는 게임 내에서 **HP와 같은 개념**이며, **시야가 0이 되면 게임 오버**입니다.

드론의 시야는 **가까운 물체를 쉽게 확인할 수 없도록** 합니다. Unity에서는 **1인칭에서는 near plane을 멀게 조절**, **3인칭에서는 드론을 중심으로 도넛 형태**를 하고 있는 시야 형태를 직접 구현하여 제작하여 구현합니다.

시야 내에서 어떤 물건을 렌더링 할 지는 물건 구현에는 레이캐스트를 활용합니다(아래 예시 코드 참조).

- 3인칭 상 제한된 드론의 시야에서 감지되는 물건을 출력하는 DroneVision 예시 코드
```csharp
using UnityEngine;

public class DroneVision : MonoBehaviour
{
    public float minViewDistance = 5f; // 가까운 거리
    public float maxViewDistance = 20f; // 가장 먼 거리
    public float viewAngle = 45f; // 시야 각도

    void Update()
    {
        CheckForVisibleTargets();
    }

    void CheckForVisibleTargets()
    {
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, maxViewDistance); // 최대 시야 범위 내 타겟

        foreach (Collider target in targetsInView)
        {
            Vector3 directionToTarget = target.transform.position - transform.position;
            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget >= minViewDistance && distanceToTarget <= maxViewDistance)
            {
                float angle = Vector3.Angle(transform.forward, directionToTarget);

                if (angle < viewAngle / 2f) // 시야 각도 내에 있으면
                {
		    // 레이캐스트 활용
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, directionToTarget.normalized, out hit, maxViewDistance))
                    {
                        if (hit.collider == target)
                        {
                            // 타겟이 시야 범위 내에 있고, 장애물이 없다면 Log로 출력
                            Debug.Log("Target in view: " + target.name);
                        }
                    }
                }
            }
        }
    }
}
```

---

## 주요 기능 및 특징

- 드론 조작: **높이**(Unity Gizmo 상 y축)가 **일정하게 고정**되어 있는 드론을 조작합니다. 드론은 맵 상에서 허용되어 있는 구역 내에서만 움직일 수 있도록 제한하여 벽을 뚫고 지나가는 경우 등이 없도록 하고, 지상 물체 조작이 아닌 공중 물체 조작인 만큼, **움직임을 멈추더라도 일정 거리를 미끄러지듯 움직이도록 구현**(Rigidbody의 Drag 등을 조절)합니다.

드론은 **사격 기능**을 포함하고 있지만, 사격 가능한 탄환 수는 제한되어 있고, 아이템을 통하여 탄환 수를 추가할 수 있습니다. 3인칭에서는 현재 마우스 위치를 대상으로 사격합니다.

드론의 시야는 제한되어 있습니다. **확인할 수 있는 가까운 거리(near)는 일정하게 고정**이 되어있지만, 아이템을 통해 일시적으로 확장할 수 있도록 합니다. 확인할 수 있는 먼 거리(far)는 시간이 지남에 따라 천천히 줄어들고, **지속적으로 아이템을 얻어 확장**시켜야 합니다. 시야가 완전히 0이 되면 게임 오버입니다.

경비원에게 발각되면 경비원이 드론을 향해 사격을 개시합니다. 적에게 공격받은 드론은 시야(far)가 점점 줄어듭니다.

- 아이템: 맵 내에 다양한 곳에 놓인 아이템을 흭득/사용합니다. 아이템의 종류는 다음과 같습니다.

  - 배터리: 제일 기본적인 체력 회복 아이템입니다. 흭득하면 **시야(far)가 확장**됩니다. 확장 가능한 시야에는 제한이 없습니다.

  - 카메라: 시야(near) 확장 아이템입니다. **10~20초 간 드론 바로 앞까지 시야가 확장**됩니다.

  - 탄환: **사격 가능 탄환 수를 추가**합니다.

  - 신호 발생기: 아이템으로 구현해도 좋고, 먼 곳에 떨어진 고정된 물체로 구현해도 좋습니다. 10~20초 간 CCTV의 작동을 멈춥니다.

  - 서류: 목표 물체입니다. 목표 서류는 하나일 수도, 여러 개 일수도 있습니다.

  - 추가적인 아이디어 있으시면 얘기해주세요!

- 적: 맵 곳곳에서 감시를 하는 적들입니다. **모든 적 Object에도 시야가 있고**, **드론의 사격을 통해 무력화**할 수 있습니다.

  - CCTV: 특정 위치에서 **고정된 방향으로 감시를 수행**하고 있습니다. 시야 범위 내에 드론이 발견되면 원형으로 설정된 구역 내의 경비원들을 nav mesh의 `SetDestination(destination.position)`를 통해 불러들입니다.

  - 경비원: 특정 위치에 고정되어 있거나 **정해진 경로를 이동하면서 감시를 수행**하고 있습니다. **시야 범위 내에 드론이 발견되면 사격을 수행**합니다.

- 추가 구현 사항(시간 되면 추가 구현)

  - 미니맵

  - UI

  - BGM/효과음

---

## 게임 시나리오 및 기획

- 주요 시나리오: 게임 시작화면 -> 게임 설명(생략 가능) -> 건물 입구에서 시작 -> 적을 피해 목표물을 가지고 탈출 -> 결과 화면

- 목표: 건물에 잠입하여 CCTV/경비원의 눈을 피해 중요 서류를 들고 탈출

- 승리 조건: 제한된 HP(**시야**)내에 게임 오버되지 않고 다시 출발점으로 돌아오기.

- 패배 조건: **HP가 모두 소진**되어 시야가 없어지면 게임 오버

- 제약 조건: 시간이 지날 수록 드론 시야의 far plane이 점점 줄어들게 되며(near plane은 조금 멀게 고정), 주어진 시야 내에서 최대한 빨리 목표 서류를 가지고 탈출해야 함.

- 점수 조건?: 단순히 탈출 성공을 목표로 하지 않고 최대한 빠른 시간 내에 탈출하여 더 빠른 탈출을 해야 더 높은 점수를 얻는 식으로 구현할 수도 있음. 단, 플레이마다 랜덤 맵을 생성하는 것은 미로 생성 알고리즘을 포함하여 레벨 디자인이 조금 복잡해져서.. 고정된 맵 안에서 최대한 빨리 탈출하는 것을 목표로 하는 것이 좋을 것 같습니다..

---

## 역할 별 개발 내용

- 레벨 디자인(장민준): 전체 맵을 구성하고 아이템을 배치합니다. CCTV/고정 경비원은 우선 상호작용 없이 임시로 위치만 지정하여 배치합니다.

- 드론 조작(장승우): 드론 조작 기능을 구현합니다. 1인칭/3인칭에 따라 가장 영향을 많이 받으실 수 있습니다. 이동/사격/체력(시야) 관리를 구현합니다.

- 아이템 효과(송승우): 아이템 사용 효과를 구현합니다. 사격 가능 탄환 개수 관리/화면 상 표시와 (여러 개의 서류로 구현하는 경우) 서류의 개수 또한 관리/표시합니다.

- npc 상호작용(최세영): CCTV/경호원과 상호작용을 구현합니다. npc의 시야/드론을 향한 사격/nav mesh를 구현합니다. nav mesh는 레벨 디자인 완료 후 맵이 어느정도 만들어지면 구현 가능하므로, nav mesh를 제외한 상호작용을 포함한 npc를 prefab으로 만들어 임시로 제작한 공간에서 nav mesh 등을 테스트해보시고 레벨 디자인 이후 경비원의 nav mesh를 맵에 구현합니다.

---

## Constraints

- URP 환경에서 작업

### 필수 구현 기능

- 키보드 또는 마우스를 사용한 입력 처리

- GameObject 간 충돌 처리

- Rigidbody를 활용한 GameObject 움직임 제어

- 스크립트를 사용한 Primitive GameObject Mesh 제어

- 최소 1개 이상의 Non-Primitive GameObject 활용 (예: 에셋 스토어 무료 모델 사용)

- Nav Mesh 활용

---

### References

#### Assets

*아래 에셋들은 아직 실제 설치 후 확인해보지 못한 것들이 많이 포함되어있어, 실제 사용 가능 여부는 추후 확인해 보아야 함.*

- [Starter Assets: Character Controllers | URP](https://assetstore.unity.com/packages/essentials/starter-assets-character-controllers-urp-267961)

![Starter Assets - FirstPerson | Updates in new CharacterController package](https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/cfcdf965-72a4-4ea3-abdf-652e880a89d1.webp)

유니티 공식 무료 에셋으로, 1인칭/3인칭 캐릭터 컨트롤러를 제공. 

- [School assets](https://assetstore.unity.com/packages/3d/environments/school-assets-146253)

![School assets](https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/531d7828-e669-495f-98a3-89cc5d1471b9.webp)

학교 테마와 각종 사물들이 많이 포함되어 있어 아이템 배치에 이점이 있을 것으로 보임.

- [Maze Modular Puzzle Kit](https://assetstore.unity.com/packages/3d/environments/maze-modular-puzzle-kit-302221)

![Maze Modular Puzzle Kit](https://assetstorev1-prd-cdn.unity3d.com/key-image/34d67d77-40fc-4f72-9ce8-bff89d059b50.webp)

미로를 생성하기 좋도록 모듈화가 잘 되어있으나, 게임과 분위기가 크게 어울리지는 않음.

- [Low Poly Storage Pack](https://assetstore.unity.com/packages/3d/environments/urban/low-poly-storage-pack-101732)

![Low Poly Storage Pack](https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/0a28bcda-e1e1-4517-8a28-dcc45c918775.webp)

보물 상자, 캐비닛 등 다른 에셋과 함께 사용하여 아이템 배치에 이점이 있어보임.

- [Sci fi like asset pack](https://assetstore.unity.com/packages/3d/environments/sci-fi-like-asset-pack-258473)

![Sci fi like asset pack](https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/f0720626-0333-48ba-b181-51783f958822.webp)

최초 의도한 분위기가 살아있으면서, 모듈화가 잘 되어 있어 맵 생성에 좋아보임.

- [Sci Fi Base Assets - free demo pack](https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-base-assets-free-demo-pack-131284)

![Sci Fi Base Assets - free demo pack](https://assetstorev1-prd-cdn.unity3d.com/key-image/da5586e8-0cf2-49cd-b5bf-b130f284348d.webp)

주제와 어울리면서 맵 구성하기에도 좋아보이나, 해상도가 높아 다소 어색한 분위기가 나올 수 있음.

- [FREE PBR Security Camera](https://assetstore.unity.com/packages/3d/props/electronics/free-pbr-security-camera-70061)

![FREE PBR Security Camera](https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/0465aaf6-9403-464f-93a4-3658a5fd9c5e.webp)

CCTV가 포함되어 있는데, 실제 화면에 모니터 화면도 보여줄 수 있지만, 카메라 모델만 사용할 수도 있어보임.

- [Surveillance Camera](https://assetstore.unity.com/packages/3d/props/surveillance-camera-264577)

![Surveillance Camera](https://assetstorev1-prd-cdn.unity3d.com/key-image/cf3e6d78-e546-44bb-a320-e1ee551052fd.webp)

무료 CCTV 에셋 중 가장 퀄리티가 좋아보이고, 모든 렌더링 파이프라인에서 사용 가능. 다만 해상도가 높아 맵 안에서 조금 눈에 띌 수도 있음.

- [Office Pack - Free](https://assetstore.unity.com/packages/3d/props/interior/office-pack-free-258600)

![Office Pack - Free](https://assetstorev1-prd-cdn.unity3d.com/key-image/c3f26526-8631-4d82-a2fa-9cb916ea00f7.webp)

각종 가구 배치나 아이템 배치에 유용해 보임.

- 그 외 아이템 등으로 사용할 만한 에셋들

 [Free Tools Kit](https://assetstore.unity.com/packages/3d/props/tools/free-tools-kit-155875)
![Free Tools Kit](https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/c7a4c2d5-37a9-4df6-a6bd-e45d6d16b413.webp)

[Low Poly Basic Items Pack - Household Items](https://assetstore.unity.com/packages/3d/props/low-poly-basic-items-pack-household-items-249507)
![Low Poly Basic Items Pack - Household Items](https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/332ee388-6d55-4115-af3d-9c2345dedfc6.webp)

[Low Poly Fantasy - Basics Pack](https://assetstore.unity.com/packages/3d/props/weapons/low-poly-fantasy-basics-pack-237364)
![Low Poly Fantasy - Basics Pack](https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/a10fde3c-689e-4efa-b1e9-24ce5484e420.webp)

- 드론 관련

[Simple Drone](https://assetstore.unity.com/packages/3d/vehicles/air/simple-drone-190684)
![Simple Drone](https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/37932f1d-f1dd-4082-a951-2bf008a6b829.webp)

- 아직 찾아보지는 못했지만 **애니메이션이 포함**되어 있는 **경비원(무기 휴대) 에셋**도 필요합니다.

- 탄환은 에셋을 사용해도 되고, Primitive Object를 활용해도 될 것 같습니다.

---

Written by Minjun Jang