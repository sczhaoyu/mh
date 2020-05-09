using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.mhxy
{
    //switch (code)
    //{
    //    case 1082:
    //        //【日光华】【62 04 03 3A 04 00】
    //        return String.Format("62 04 {0} {1} 00", hexAttackPos, hexCode);
    //    case 1083:
    //        //【靛沧海】【62 04 03 3B 04 00】
    //        return String.Format("62 04 {0} {1} 00", hexAttackPos, hexCode);
    //    case 1085:
    //        //【苍茫树】【62 04 03 3D 04 00】
    //        return String.Format("62 04 {0} {1} 00", hexAttackPos, hexCode);
    //    case 1535:
    //        //【横扫千军】【62 04 03 FF 05 00】
    //        return String.Format("62 04 {0} {1} 00", hexAttackPos, hexCode);
    //    case 1526:
    //        //【小试牛刀】【62 04 03 F6 05 00】
    //        //62 04 03 F6 05 00
    //        return String.Format("62 04 {0} {1} 00", hexAttackPos, hexCode);
    //    case 1544:
    //        //【斩雷】【62 04 04 08 06 00】
    //        return String.Format("62 04 {0} {1} 00", hexAttackPos, hexCode);
    //    case 1547:
    //        //【龙卷雨击】【62 04 04 0B 06 00】
    //        return String.Format("62 04 {0} {1} 00", hexAttackPos, hexCode);
    //}

    /// <summary>
    /// 游戏控制指令
    /// </summary>
    class GameCmd
    {

        /// <summary>
        /// 生成攻击指令
        /// </summary>
        /// <param name="babyPos">宝宝站位</param>
        /// <param name="attackPos">攻击站位</param>
        /// <param name="roleType">0人物技能，1宝宝技能</param>
        ///  <param name="router">技能方式路由</param>
        /// <param name="code">技能指令</param>
        /// <returns></returns>
        public static string CmdBattleAttack(int babyPos, int attackPos, int roleType, int router, int code)
        {
            string hexBabyPos = StringUtil.FormatIntToHexStyle(babyPos, false);
            string hexAttackPos = StringUtil.FormatIntToHexStyle(attackPos, false);
            byte[] tmp = BitConverter.GetBytes(code);
            string hexCode = StringUtil.FormatBytesToHex(new byte[] { tmp[0], tmp[1] }).Trim();
            switch (roleType)
            {
                case 0:
                    switch (router)
                    {
                        //【普通攻击】【61 02 04 00】 【第三位是攻击位置】
                        case 0x00:
                        case 0x61:
                            return String.Format("61 02 {0} 00", hexAttackPos);
                        //技能释放
                        case 0x62:

                            return String.Format("62 04 {0} {1} 00", hexAttackPos, hexCode);
                        case 0x6F:
                            //抓怪物
                            return String.Format("6F 02 {0} 00", hexAttackPos);

                    }
                    break;
                case 1:
                    switch (router)
                    {
                        case 0x00:
                        case 0x6A:
                            //【普通攻击】【6A 03 02 04 00】 【有宝宝加入战斗发指令,3个是宝宝自身位置，4是攻击位置】
                            return String.Format("6A 03 {0} {1} 00", hexBabyPos, hexAttackPos);
                        case 0x6B:
                            return String.Format("6B 05 {0} {1} {2} 00", hexBabyPos, hexAttackPos, hexCode);
                    }
                    break;

            }
            return "";
        }

    }

}
