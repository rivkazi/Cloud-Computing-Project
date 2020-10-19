using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class Validation
    {
        public static bool IsEmail(string s)
        {
            if (s == null || s == "")
                return false;
            if (s[0] == '@')
                return false;
            if (s.Contains('@') != true || s.Substring(s.IndexOf("@") + 1).Contains(".") != true)
                return false;
            foreach (var item in s)
            {
                if (item != '@' && item != '.' && item != '_' && !char.IsLetter(item) && char.IsWhiteSpace(item))
                    return false;
            }
            return true;
        }

        public static bool ValidId(string id)
        {
            IDAL dal = new DalClass();
            if (dal.GetPatients(u => u.idNumber == id) != null)
                return false;
            return true;
        }

        public static bool IsId(object idn)
        {
            if (idn == null || idn.ToString() == "000000000")
                return false;
            string idnumber = "";
            if (idn is string)
                idnumber = idn.ToString();
            else if (idn is int)
                idnumber = idn.ToString();
            else
                return false;

            if (idnumber.Length != 9)
                return false;
            int[] id = new int[idnumber.Length];
            for (int i = 0; i < idnumber.Length; i++)
                id[i] = idnumber[i] - '0';
            for (int i = 1; i < 8; i = i + 2)
            {
                int a = id[i] * 2;
                if (a > 9)
                    a = (a % 10) + (a / 10);
                id[i] = a;
            }
            int sumOfDigits = 0;
            for (int i = 0; i < id.Length; i++)
                sumOfDigits += id[i];
            return sumOfDigits % 10 == 0;
        }
        public static bool IsName(string s)
        {
            if (s == null || s == "")
                return false;
            foreach (var item in s)
            {
                if (!char.IsLetter(item) && !char.IsWhiteSpace(item))
                    return false;
            }
            return true;
        }
        public static bool IsUserName(string s)
        {
            if (s == null || s == "")
                return false;
            int numbers = 0;
            foreach (var item in s)
            {
                if (!char.IsLetter(item) && !char.IsWhiteSpace(item))
                    numbers++;
            }
            if (numbers == s.Length)
                return false;
            return true;
        }

        public static bool IsPassword(string password)
        {
            if (password == "")
                return false;

            bool flag = true;
            for(int i=0; i< password.Length && flag; i++)
            {
                if (char.IsWhiteSpace(password[i]))
                    flag = false;
            }
            if (flag)
                return true;
            return false;
        }
        public static bool IsPhone(string phoneNumber)
        {
            if (phoneNumber == "")
                return false;

            string sub;
            if (phoneNumber.Length == 10)
            {
                sub = phoneNumber.Substring(2, 8);
                if (phoneNumber[0] != '0' || phoneNumber[1] != '5')
                    return false;
            }
            else if (phoneNumber.Length == 9)
            {
                sub = phoneNumber.Substring(1, 8);
                if (phoneNumber[0] != '0')
                    return false;
            }
            else
            {
                return false;
            }
            
            bool flag = true;
            for (int i = 0; i < phoneNumber.Length && flag; i++)
            {
                if (phoneNumber[i] < '0' || phoneNumber[i] > '9')
                    flag = false;
            }
            if (flag)
                return true;
            return false;
        }
    }
}
