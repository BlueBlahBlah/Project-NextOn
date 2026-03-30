using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSkillTriggerBox : MonoBehaviour
{
    [SerializeField] private DamageManager DamageManager;
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
        DamageManager = GameObject.Find("DamageManager").GetComponent<DamageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        // 異⑸룎??臾쇱껜媛 Player ?쒓렇瑜?媛吏?寃쎌슦
        if (other.CompareTag("Player"))
        {
            int num;    // turret position num
            bool turretSpawned = false;

            // 理쒕? 8踰덇퉴吏 ?쒕룄
            for (int i = 0; i < 8; i++)
            {
                num = Random.Range(0, 8);

                // spawnTurret???깃났?섎㈃ 猷⑦봽瑜?醫낅즺
                if (spawnTurret(num))
                {
                    turretSpawned = true;
                    break;
                }
            }

            // 留뚯빟 8踰??쒕룄 ?꾩뿉???곕젢???앹꽦?섏? 紐삵뻽?ㅻ㈃ 異붽? 濡쒖쭅??異붽??????덉뒿?덈떎.
            if (!turretSpawned)
            {
                Debug.LogError("Failed to spawn turret after 8 attempts.");
            }
        }
    }

    private bool spawnTurret(int num)
    {
        // "Place + num"??GameObject瑜?李얠븘?듬땲??
        GameObject placeObject = GameObject.Find("Place " + num);

        if (placeObject != null)
        {
            // placeObject??Transform 而댄룷?뚰듃瑜??살뼱?듬땲??
            Transform placeTransform = placeObject.transform;

            // placeObject??醫뚰몴媛믪쓣 ?살뼱?듬땲??
            Vector3 placePosition = placeTransform.position;

            // placeObject???뚯쟾媛믪쓣 ?살뼱?듬땲??
            Quaternion placeRotation = placeTransform.rotation;
            
            int MissileColor = DamageManager.Turret_Skill_BulletColor;
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

            return true; // ?곕젢???깃났?곸쑝濡??앹꽦?섏뿀?뚯쓣 ?섑??낅땲??
        }

        return false; // ?곕젢 ?앹꽦???ㅽ뙣?덉쓬???섑??낅땲??
    }
}
