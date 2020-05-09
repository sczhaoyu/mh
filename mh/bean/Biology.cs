using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace mh.bean
{
    public abstract class Biology
    {
        biz.Execute ex;
        public Biology(biz.Execute e)
        {
            this.ex = e;
            checkAttrThread = new Thread(new ThreadStart(runCheck));
            checkAttrThread.Start();
        }
        /// <summary>
        /// 名字
        /// </summary>
        public string name;
        /// <summary>
        /// ID
        /// </summary>
        public int id = 0;
        /// <summary>
        /// 等级
        /// </summary>
        public int lv;
        /// <summary>
        /// 气血值
        /// </summary>
        public int hp=100;
        /// <summary>
        /// 最大气血值
        /// </summary>
        public int maxHP;
        /// <summary>
        /// 魔法值
        /// </summary>
        public int mp = 100;
        /// <summary>
        /// 魔法值
        /// </summary>
        public int maxMP;
        /// <summary>
        /// 经验值
        /// </summary>
        public int exp;
        /// <summary>
        /// 最大升级经验值
        /// </summary>
        public int maxExp;
        /// <summary>
        /// 愤怒值
        /// </summary>
        public int anger;
        Thread checkAttrThread;
        public bool checkStatus = true;

        /// <summary>
        /// 输出属性
        /// </summary>
        public void sayHi()
        {
            Log.WriteLine("【名称:{0}[{1}]】【气血:{2}_max:{7}】【魔法:{3}_max:{8}】【愤怒:{4}】【经验:{5}】【最大经验:{6}】", name, lv, hp, mp, anger, exp, maxExp, maxHP, maxMP);
        }
        /// <summary>
        /// 补充蓝血
        /// </summary>
        public abstract void AddHPMP(IntPtr hwnd);
        public void runCheck()
        {
           
            while (checkStatus)
            {
              
                //检测加蓝物品，如果没有就报警
                if (Global.mh_cfg.ContainsKey(ex.hwnd) == false)
                {
                    continue;
                }
              
                //非战斗状态待用
                if (ex.battle.battle == false)
                {
                    System.Threading.Thread.Sleep(2000);
                    AddHPMP(ex.hwnd);
                   
                }

            }
        }

    }
}
