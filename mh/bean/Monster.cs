using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.bean
{
    public class Monster
    {
        //怪物名称
        public string name;
        //怪物所在位置
        public int postion;
        //生存状态
        public int life;
        public Monster(string name,int pos) {
            this.name = name;
            this.postion = pos;
            this.life = 1;
        }
    }
}
