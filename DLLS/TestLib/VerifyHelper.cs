using System;

namespace TestLib
{
    /// <summary>
    /// 验证类集合
    /// </summary>
    public class VerifyHelper
    {
        /// <summary>
        /// 验证身份证 15/18位
        /// </summary>
        /// <param name="Id">需要验证的身份证号</param>
        /// <returns>true:验证为有效身份证格式 false:验证为不符合格式身份证</returns>
        public static bool CheckIDCard(string Id)
        {
            if (Id.Length != 15 && Id.Length != 18)
            {
                return false;//位数验证
            }
            long n = 0;
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (Id.Length == 15)
            {
                if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
                {
                    return false;//数字验证
                }
                if (address.IndexOf(Id.Remove(2)) == -1)
                {
                    return false;//省份验证
                }
                string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证
                }
            }
            else
            {
                if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
                {
                    return false;//数字验证
                }
                if (address.IndexOf(Id.Remove(2)) == -1)
                {
                    return false;//省份验证
                }
                string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证
                }
                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = Id.Remove(17).ToCharArray();
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                }
                int y = -1;
                Math.DivRem(sum, 11, out y);
                if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
                {
                    return false;//校验码验证
                }
            }
            return true;//符合身份证标准
        }
         

        //public static bool CheckMobile(string mobile)
        //{
        //    return System.Text.RegularExpressions.Regex.IsMatch(mobile, @"^[1]+\d{10}");
        //}
    }
}
