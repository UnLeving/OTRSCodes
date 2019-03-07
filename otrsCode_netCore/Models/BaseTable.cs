﻿namespace otrsCode_netCore.Models
{
    public class BaseTable
    {
        public string R;
        public string AB;
        public CodeDt[] codes = new CodeDt[10];
    }

    public class CodeDt
    {
        public int id;
        public string code;
        public string colorHEX = "#FFFFFF";
    }
}