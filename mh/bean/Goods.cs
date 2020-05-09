using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.bean
{
    public class Goods
    {
        public int id;
        public int type;
        public int count;
        public string name;
        public int idx;
        public Goods(int id, int type, int count,int idx)
        {
            this.id = id;
            this.type = type;
            this.count = count;
            this.idx = idx;
        }
        public string GetName()
        {

            if (Global.goods_names.ContainsKey(type))
            {
                this.name = Global.goods_names[type];
            }
            return name;
        }
    }

}
