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
        // ì¶©ëŒ??ë¬¼ì²´ê°€ Player ?œê·¸ë¥?ê°€ì§?ê²½ìš°
        if (other.CompareTag("Player"))
        {
            int num;    // turret position num
            bool turretSpawned = false;

            // ìµœë? 8ë²ˆê¹Œì§€ ?œë„
            for (int i = 0; i < 8; i++)
            {
                num = Random.Range(0, 8);

                // spawnTurret???±ê³µ?˜ë©´ ë£¨í”„ë¥?ì¢…ë£Œ
                if (spawnTurret(num))
                {
                    turretSpawned = true;
                    break;
                }
            }

            // ë§Œì•½ 8ë²??œë„ ?„ì—???°ë ›???ì„±?˜ì? ëª»í–ˆ?¤ë©´ ì¶”ê? ë¡œì§??ì¶”ê??????ˆìŠµ?ˆë‹¤.
            if (!turretSpawned)
            {
                Debug.LogError("Failed to spawn turret after 8 attempts.");
            }
        }
    }

    private bool spawnTurret(int num)
    {
        // "Place + num"??GameObjectë¥?ì°¾ì•„?µë‹ˆ??
        GameObject placeObject = GameObject.Find("Place " + num);

        if (placeObject != null)
        {
            // placeObject??Transform ì»´í¬?ŒíŠ¸ë¥??»ì–´?µë‹ˆ??
            Transform placeTransform = placeObject.transform;

            // placeObject??ì¢Œí‘œê°’ì„ ?»ì–´?µë‹ˆ??
            Vector3 placePosition = placeTransform.position;

            // placeObject???Œì „ê°’ì„ ?»ì–´?µë‹ˆ??
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

            return true; // ?°ë ›???±ê³µ?ìœ¼ë¡??ì„±?˜ì—ˆ?Œì„ ?˜í??…ë‹ˆ??
        }

        return false; // ?°ë › ?ì„±???¤íŒ¨?ˆìŒ???˜í??…ë‹ˆ??
    }
}
