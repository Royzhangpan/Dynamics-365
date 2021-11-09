using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Model
{
    public class CampusUserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string userType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StudentInfo studentInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ParentInfosItem> parentInfos { get; set; }

    }
    public class StudentInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string personalNo { get; set; }

        /// <summary>
        /// 广东省深圳市宝安区恒安花园恒安阁XXX 楼XX 房
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cardNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int cardType { get; set; }

        /// <summary>
        /// 一年级一班
        /// </summary>
        public string className { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string educateStatus { get; set; }

        /// <summary>
        /// 陈学生
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int orgId { get; set; }

        /// <summary>
        /// 上海金茂学校
        /// </summary>
        public string orgName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sex { get; set; }

    }

    public class ParentInfosItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string cardNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int cardType { get; set; }

        /// <summary>
        /// 陈学生父亲
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int parentType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string personalNo { get; set; }

    }
}
