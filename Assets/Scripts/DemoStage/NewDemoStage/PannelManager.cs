using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelManager : MonoBehaviour
{
    private static PannelManager instance = null;
        private void Awake()
        {
            if (null == instance)
            {
                //???대옒???몄뒪?댁뒪媛 ?꾩깮?덉쓣 ???꾩뿭蹂??instance??寃뚯엫留ㅻ땲? ?몄뒪?댁뒪媛 ?닿꺼?덉? ?딅떎硫? ?먯떊???ｌ뼱以??
                instance = this;
    
                //???꾪솚???섎뜑?쇰룄 ?뚭눼?섏? ?딄쾶 ?쒕떎.
                //gameObject留뚯쑝濡쒕룄 ???ㅽ겕由쏀듃媛 而댄룷?뚰듃濡쒖꽌 遺숈뼱?덈뒗 Hierarchy?곸쓽 寃뚯엫?ㅻ툕?앺듃?쇰뒗 ?살씠吏留? 
                //?섎뒗 ?룰컝由?諛⑹?瑜??꾪빐 this瑜?遺숈뿬二쇨린???쒕떎.
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                //留뚯빟 ???대룞???섏뿀?붾뜲 洹??ъ뿉??Hierarchy??GameMgr??議댁옱???섎룄 ?덈떎.
                //洹몃윺 寃쎌슦???댁쟾 ?ъ뿉???ъ슜?섎뜕 ?몄뒪?댁뒪瑜?怨꾩냽 ?ъ슜?댁＜??寃쎌슦媛 留롮? 寃?媛숇떎.
                //洹몃옒???대? ?꾩뿭蹂?섏씤 instance???몄뒪?댁뒪媛 議댁옱?쒕떎硫??먯떊(?덈줈???ъ쓽 GameMgr)????젣?댁???
                Destroy(this.gameObject);
            }
        }
        //寃뚯엫 留ㅻ땲? ?몄뒪?댁뒪???묎렐?????덈뒗 ?꾨줈?쇳떚. static?대?濡??ㅻⅨ ?대옒?ㅼ뿉??留섍퍘 ?몄텧?????덈떎.
        public static PannelManager Instance
        {
            get
            {
                if (null == instance)
                {
                    return null;
                }
                return instance;
            }
        }
        
        private List<Dictionary<string, object>> data;
        
        
    // Start is called before the first frame update
    void Start()
    {
        data = CSVReader.Read("Resources ?덉쓽 寃쎈줈");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
