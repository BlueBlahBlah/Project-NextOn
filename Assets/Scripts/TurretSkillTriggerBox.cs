using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSkillTriggerBox : MonoBehaviour
{
    public GameObject Normalturret;
    public GameObject Redturret;
    public GameObject Greenturret;
    public GameObject Bluelturret;
    public GameObject Yellowturret;
    public GameObject Bombturret;
    public GameObject turret;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체가 Player 태그를 가진 경우
        if (other.CompareTag("Player"))
        {
            int num;    // turret position num
            bool turretSpawned = false;

            // 최대 8번까지 시도
            for (int i = 0; i < 8; i++)
            {
                num = Random.Range(0, 8);

                // spawnTurret이 성공하면 루프를 종료
                if (spawnTurret(num))
                {
                    turretSpawned = true;
                    break;
                }
            }

            // 만약 8번 시도 후에도 터렛을 생성하지 못했다면 추가 로직을 추가할 수 있습니다.
            if (!turretSpawned)
            {
                Debug.LogError("Failed to spawn turret after 8 attempts.");
            }
        }
    }

    private bool spawnTurret(int num)
    {
        // "Place + num"의 GameObject를 찾아옵니다.
        GameObject placeObject = GameObject.Find("Place " + num);

        if (placeObject != null)
        {
            // placeObject의 Transform 컴포넌트를 얻어옵니다.
            Transform placeTransform = placeObject.transform;

            // placeObject의 좌표값을 얻어옵니다.
            Vector3 placePosition = placeTransform.position;

            // placeObject의 회전값을 얻어옵니다.
            Quaternion placeRotation = placeTransform.rotation;
            
            int MissileColor = GameObject.Find("StageManager").GetComponent<StageManager>().Turret_Skill_BulletColor;
            switch (MissileColor)
            {
                case 0:
                    turret = Normalturret;
                    break;
                case 1:
                    turret = Redturret;
                    break;
                case 2:
                    turret = Greenturret;
                    break;
                case 3:
                    turret = Bluelturret;
                    break;
                case 4:
                    turret = Yellowturret;
                    break;
                case 5:
                    turret = Bombturret;
                    break;
            }
            
            Instantiate(turret, placePosition, placeRotation);

            return true; // 터렛이 성공적으로 생성되었음을 나타냅니다.
        }

        return false; // 터렛 생성에 실패했음을 나타냅니다.
    }
}
