using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.bean
{
    /// <summary>
    /// 队友
    /// </summary>
    public class Teammate
    {
        //名称
        public string name;
        //位置
        public int postion;
        //生存状态
        public int life;
        //是否可救援
        public bool rescue;
        //队友类型,0人物，1宝宝
        public int type;
        //人物的宝宝站位
        public int babyPos;
    }
}
