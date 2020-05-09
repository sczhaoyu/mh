using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.bean
{
    /// <summary>
    /// 属性面板
    /// </summary>
    public class AttrsPanel
    {
        public int peopleHP;
        public int peopleMP;
        public int anger;
        public int peopleExp;
        public int bbHP;
        public int bbMP;
        public int bbExp;
        public void sayHI()
        {
            Log.WriteLine("人物【hp:{0},mp:{1},sp{2},exp{3}】_宝宝【hp:{4},mp:{5},exp{6}】", peopleHP, peopleMP, anger, peopleExp, bbHP, bbMP, bbExp);
        }
    }
}
