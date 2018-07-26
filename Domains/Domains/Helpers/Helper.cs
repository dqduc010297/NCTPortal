using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Helpers
{
    public class Helper
    {
		//Generate code
		public static string GenerateCode(DateTime dateTime, long id)
		{
			string day = ConverIntToString(dateTime.Day);
			string month = ConverIntToString(dateTime.Month);
			string hour = ConverIntToString(dateTime.Hour);
			string minute = ConverIntToString(dateTime.Minute);
			string second = ConverIntToString(dateTime.Second);
			string code = day + month + dateTime.Year.ToString() + hour + minute + second+"GG" + id.ToString();
			return code;
		}

		public static string ConverIntToString(int input)
		{
			string output = "";
			if (input < 10)
				output = "0" + input.ToString();
			else output = input.ToString();
			return output;
		}
        public static double sub(double val1, double val2)
        {
            return Math.Abs(val1 - val2);
        }
        public static string ResizeAddress(string address)
        {
            address = address.Replace("Thanh pho", "TP. ");
            address = address.Replace("Phường", "P.");
            address = address.Replace("Quận", "Q.");
            address = address.Replace("Thanh pho", "");
            address = address.Replace("Thành phố", "");
            address = address.Replace("Việt Nam", "VN");
            address = address.Replace("Vietnam", "VN");
            address = address.Replace("Ho Chi Minh", "HCM");
            address = address.Replace("Hồ Chí Minh", "HCM");
            return address;
        }
    }
}
