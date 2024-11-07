using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Helper
{
    public  class EncryptionName
    {

        private static readonly Dictionary<char, string> ArabicToEnglishMap = new Dictionary<char, string>
        {
            {'ا', "a"}, {'ب', "b"}, {'ت', "t"}, {'ث', "th"},
            {'ج', "j"}, {'ح', "h"}, {'خ', "kh"}, {'د', "d"},
            {'ذ', "dh"}, {'ر', "r"}, {'ز', "z"}, {'س', "s"},
            {'ش', "sh"}, {'ص', "s"}, {'ض', "d"}, {'ط', "t"},
            {'ظ', "z"}, {'ع', "a"}, {'غ', "gh"}, {'ف', "f"},
            {'ق', "q"}, {'ك', "k"}, {'ل', "l"}, {'م', "m"},
            {'ن', "n"}, {'ه', "h"}, {'و', "w"}, {'ي', "y"},
            {'ء', "a"}, {'ئ', "y"}, {'ؤ', "w"}, {'ى', "a"},
            {'ة', "h"}
        };

        // دالة لتحويل الاسم من عربي إلى إنجليزي
        public static string ConvertArabicToEnglish(string arabicName)
        {
            StringBuilder englishName = new StringBuilder();

            foreach (char ch in arabicName)
            {
                if (ArabicToEnglishMap.ContainsKey(ch))
                {
                    englishName.Append(ArabicToEnglishMap[ch]);
                }
                else
                {
                    englishName.Append(ch); // إضافة الحرف كما هو إذا لم يوجد في القاموس
                }
            }

            return englishName.ToString();
        }


    }
}


