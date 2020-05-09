using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace mh
{
    static class Global
    {
        static Global()
        {
            load_map_reg();
            load_skill_info();
            load_goodsName();
            load_BiaoJuNPCName();
        }
        public static Dictionary<int, string> goods_names = new Dictionary<int, string>();
        public static Dictionary<string, int> biaoju_names = new Dictionary<string, int>();
        public static Mutex mouseMx = new Mutex();
        //梦幻句柄函数地址
        public static Dictionary<IntPtr, LoadDll.MhFuncAddrs> mh_func = new Dictionary<IntPtr, LoadDll.MhFuncAddrs>();

        /// <summary>
        /// 物品名称初始化
        /// </summary>
        public static void load_goodsName()
        {


        }
        public static void load_BiaoJuNPCName()
        {

            biaoju_names["空度禅师"] = 536871333;
            biaoju_names["程咬金"] = 536871346;
            biaoju_names["秦琼"] = 536871348;
            biaoju_names["李靖"] = 536871510;
            biaoju_names["杨戬"] = 536871511;
            biaoju_names["东海龙王"] = 536871537;
            biaoju_names["地藏王"] = 536871563;
            biaoju_names["二大王"] = 536871578;
            biaoju_names["三大王"] = 536871579;
            biaoju_names["大大王"] = 536871580;
            biaoju_names["菩提祖师"] = 536871586;
            biaoju_names["观音姐姐"] = 536871593;
            biaoju_names["孙婆婆"] = 536871602;
            biaoju_names["花十娘"] = 536871603;
            biaoju_names["冰冰姑娘"] = 536871604;
            biaoju_names["牛魔王"] = 536871605;
            biaoju_names["镇元大仙"] = 536871611;

        }
        /// <summary>
        /// 加载技能信息
        /// </summary>
        public static void load_skill_info()
        {
            List<model.MhxySkill> s = model.MhxySkill.GetAll();
            for (int i = 0; i < s.Count; i++)
            {
                skill_map[s[i].SkillId] = s[i];
            }

        }
        /// <summary>
        /// 加载地图区域信息
        /// </summary>
        public static void load_map_reg()
        {
            List<model.MhxyMapRegion> mmr = model.MhxyMapRegion.GetMapAll();
            for (int i = 0; i < mmr.Count; i++)
            {
                if (!map_reg.ContainsKey(mmr[i].map_id))
                {
                    map_reg[mmr[i].map_id] = new List<model.MhxyMapRegion>();
                }

                map_reg[mmr[i].map_id].Add(mmr[i]);
            }
        }
        /// <summary>
        /// 游戏基础地址
        /// </summary>
        public static mhxy.Addr addr = null;
        //游戏窗口
        public static Dictionary<IntPtr, string> win_list = new Dictionary<IntPtr, string>();
        //梦幻窗口类名
        public static string mh_class = "WSGAME";
        /// <summary>
        /// 默认存储表名
        /// </summary>
        public static string save_table = "mhxy_log";
        //人物标识
        public static int people_type = 0;
        //宝宝标识
        public static int baby_type = 1;


        //收取数据
        public static int rev_type = 0;
        //发送数据
        public static int send_type = 1;

        /// <summary>
        /// 游戏窗口配置
        /// </summary>
        public static Dictionary<IntPtr, model.MhxyConfig> mh_cfg = new Dictionary<IntPtr, model.MhxyConfig>();
        /// <summary>
        /// 地图区域
        /// </summary>
        public static Dictionary<int, List<model.MhxyMapRegion>> map_reg = new Dictionary<int, List<model.MhxyMapRegion>>();
        /// <summary>
        /// 技能信息
        /// </summary>
        public static Dictionary<int, model.MhxySkill> skill_map = new Dictionary<int, model.MhxySkill>();
    }
}
