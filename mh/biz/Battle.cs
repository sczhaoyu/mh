using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mh.bean;
using System.Threading;

namespace mh.biz
{
    /// <summary>
    /// 人物战斗设置
    /// </summary>
    public class Battle
    {

        //攻击类型
        public int attack = 0;
        //怪物列表
        public List<Monster> monster = new List<Monster>();
        //怪物操作锁
        private Mutex mxMonster = new Mutex();
        //队友列表
        public List<Teammate> teammate = new List<Teammate>();
        public IntPtr hwnd;
        //宝宝是否参战
        public bool baby = false;
        //宝宝站位
        public int babyPos = 0;

        //是否是战斗状态
        public bool battle = false;
        //攻击准备
        public bool attackStatus = false;
        //自己姓名
        public string masterName;
        //自己位置
        public int masterPos = 0;
        //自己的存活状态
        public bool myLife = true;
        Execute ex;
        Thread checkThread = null;
        //技能随机数
        public bool skill_random = false;
        public bool skill_first = true;
        public Battle(Execute e)
        {

            this.ex = e;
            this.hwnd = e.hwnd;
            checkThread = new Thread(new ThreadStart(checkPeople));
            checkThread.Start();
        }
        /// <summary>
        /// 检测四个小人坐标
        /// </summary>
        public void checkPeople()
        {
            while (true)
            {
                //两秒检测一次
                System.Threading.Thread.Sleep(2000);
                //mhxy.CallFunc.clickPeople(hwnd.ToInt32());


            }
        }
        /// <summary>
        /// 收包动作处理器
        /// </summary>
        /// <param name="action"></param>
        /// <param name="data"></param>
        public void handle(int dataType, int action, byte[] data)
        {
            if (dataType != Global.rev_type && action != 0x47)
            {
                return;
            }
            switch (action)
            {
                case 0x47:
                    //战斗状态并且是发包
                    if (battle == false || dataType != Global.send_type) { return; }

                    Thread mt = new Thread(new ThreadStart(attackEnemy));
                    mt.Start();
                    break;
                case 0x61://进入战斗
                    battle = true;
                    skill_first = true;
                    Console.WriteLine("【进入战斗】");
                    break;
                case 0x62:
                    //62 00 00 退出战斗
                    if (data[2] == 0x00)
                    {
                        battle = false;
                        skill_first = true;
                    }
                    Console.WriteLine("【战斗结束】");
                    break;
                case 0x63:
                    Console.WriteLine("第【{0}】回合", (data[3] + 1));
                    break;
                case 0x64://怪物消息
                    byte[] tmp = null;
                    //自己的名称
                    masterName = mhxy.Common.GetMHName(hwnd.ToInt32());
                    for (int i = 14; i < data.Length; i++)
                    {
                        if (data[i] == 0x00)
                        {
                            tmp = data.Skip(14).Take(i - 14).ToArray();
                            break;
                        }
                    }
                    //名称获取
                    string name = StringUtil.GetChsFromHex(StringUtil.byteToHexStr(tmp));
                    int pos = data[3];
                    if (data[12] == 0x00 || data[12] == 0x01)
                    {
                        addMonster(name, pos);
                        Console.WriteLine("敌人:【{0}】—位置【{1}】", name, pos);
                    }
                    else
                    {
                        //队友列表
                        bean.Teammate t = new bean.Teammate();
                        t.name = name;
                        t.postion = pos;
                        t.life = 1;
                        //宝宝扶不起来
                        if (data[9] == 0x07)
                        {
                            t.rescue = false;
                        }
                        else
                        {
                            t.rescue = true;
                        }
                        //宝宝所站的位置
                        if (masterName == name)
                        {
                            masterPos = pos;
                        }
                        t.babyPos = data[4];
                        teammate.Add(t);
                        Log.WriteLine("队友【{0}】,位置【{1}】", name, pos);
                    }
                    //判断自己宝宝是否加入战斗
                    if (data[13] != 0x00)
                    {
                        //判断是否是自己的宝宝

                        int myPos = -1;
                        //int tm_babyPos = 0;
                        //根据窗口名称查找自己的位置
                        for (int i = 0; i < teammate.Count; i++)
                        {
                            if (teammate[i].name == masterName)
                            {
                                ex.mySelf.name = masterName;
                                myPos = teammate[i].postion;

                                break;
                            }
                        }
                        if (data[13] == myPos)
                        {
                            baby = true;
                            babyPos = pos;
                            ex.myBaby.name = name;

                        }

                    }
                    break;
                case 0x65:
                    //65 02 00 02 00 位置有被撂倒
                    this.killEnemy(data[3]);
                    break;



            }
        }
        /// <summary>
        /// 添加怪物
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        public void addMonster(string name, int pos)
        {
            Monster m = new Monster(name, pos);
            monster.Add(m);
        }
        /// <summary>
        /// 获取怪物的随机位置
        /// </summary>
        /// <returns></returns>
        public Monster GetRandomMonsterPosIndx()
        {
            if (monster.Count == 1) { return monster[0]; }
            Random r = new Random();
            int idx = r.Next(0, monster.Count - 1);
            return monster[idx];

        }
        /// <summary>
        /// 攻击指令
        /// 存在同步和等待的情况
        /// </summary>
        public void attackEnemy()
        {

            mxMonster.WaitOne();

            if (monster.Count > 0)
            {
                System.Threading.Thread.Sleep(1000);
                Monster mon = GetRandomMonsterPosIndx();
                int peopleCode = 0x00;//普通攻击
                //检查有没有宝宝
                int bb_pos = checkBB();
                //判断是否自动释放技能
                if (Global.mh_cfg.ContainsKey(hwnd) == true && Global.mh_cfg[hwnd].skill_auto == 1)
                {
                    peopleCode = Global.mh_cfg[hwnd].skill;
                    string cmd = "";
                    //没有宝宝发动攻击,或者魔法不够直接攻击
                    if (bb_pos == -1 || ex.mySelf.mp < 26)
                    {
                        //检测自己使用魔法时气血和所剩魔法是否满足，不够使用普通攻击

                        if (ex.mySelf.mp < Global.skill_map[peopleCode].mp || ex.mySelf.hp < Global.skill_map[peopleCode].hp || peopleCode <= 0)
                        {
                            cmd = mhxy.GameCmd.CmdBattleAttack(0, mon.postion, 0, 0x61, 0);
                        }
                        else
                        {
                            cmd = mhxy.GameCmd.CmdBattleAttack(0, mon.postion, 0, 0x62, peopleCode);
                            //技能附着随机码
                            Console.WriteLine("技能附着随机码【{0}】:【{1}】", skill_first ,skill_random);
                            if (skill_first && skill_random)
                            {
                                Random rm = new Random();
                                cmd = cmd.Substring(0, cmd.Length - 3);
                                //获取怪物的个数

                                if (monster.Count > 1)
                                {
                                    //两个怪物 随机码两个，加长
                                    cmd = cmd.Replace("04", "06");
                                    cmd = String.Format(cmd + " {0} {1} 00", StringUtil.FormatIntToHexStyle(rm.Next(60, 235), false), StringUtil.FormatIntToHexStyle(rm.Next(60, 235), false));
                                }
                                else
                                {
                                    //1个怪物 随机码1个，加长
                                    cmd = cmd.Replace("04", "05");
                                    cmd = String.Format(cmd + " {0} 00", StringUtil.FormatIntToHexStyle(rm.Next(60, 235), false));
                                }
                                skill_first = false;
                            }
                        }

                    }
                    else
                    {
                        //抓宝宝
                        cmd = mhxy.GameCmd.CmdBattleAttack(0, bb_pos, 0, 0x6F, 0);
                    }
                    Console.WriteLine("【{0}】:【{1}】", masterName, cmd);

                    LoadDll.sendMsg(hwnd, cmd);
                    if (baby)
                    {

                        System.Threading.Thread.Sleep(1000);
                        cmd = mhxy.GameCmd.CmdBattleAttack(babyPos, mon.postion, 1, 0, 0);

                        int bb_code = Global.mh_cfg[hwnd].bb_skill;

                        //判断宝宝魔法是否够发出技能
                        if (ex.myBaby.mp >= Global.skill_map[bb_code].mp && ex.mySelf.hp >= Global.skill_map[bb_code].hp && bb_code > 0)
                        {
                            cmd = mhxy.GameCmd.CmdBattleAttack(babyPos, mon.postion, 1, 0x6B, bb_code);

                        }
                        Console.WriteLine("【宝宝技能】:【{0}】", cmd);
                        LoadDll.sendMsg(hwnd, cmd);
                    }
                }

            }
            mxMonster.ReleaseMutex();


        }
        /// <summary>
        /// 检查是否有宝宝
        /// </summary>
        public int checkBB()
        {
            //6F 02 02 00
            for (int i = 0; i < monster.Count; i++)
            {
                if (monster[i].name.IndexOf("宝宝") > -1)
                {
                    Log.WriteLine("检测到宝宝【{0}】，位置【{1}】", monster[i].name, monster[i].postion);
                    return monster[i].postion;
                }
            }
            return -1;
        }
        //消灭敌人
        public void killEnemy(int postion)
        {
            mxMonster.WaitOne();
            //检测怪物位置
            for (int i = 0; i < monster.Count; i++)
            {
                if (monster[i].postion == postion)
                {
                    Console.WriteLine("【{0}:{1}】消灭", monster[i].name, monster[i].postion);
                    monster.RemoveAt(i);

                }
            }
            //检测是否是自己宝宝
            if (postion == babyPos)
            {
                Console.WriteLine("自己宝宝消灭");
                baby = false;
            }
            //检测是否是自己
            if (masterPos == postion)
            {
                myLife = false;
                Console.WriteLine("自己被消灭");
            }
            mxMonster.ReleaseMutex();

        }
    }
}
