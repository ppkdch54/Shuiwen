using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShuiwenLib;

namespace Shuiwen
{
    class Parser
    {


        public static double GetShowData(SENSOR_TYPE type, double dataOrigion,double data,double dataAvg,double dataSum)
        {
            //return (type == SENSOR_TYPE.GUANDAO||type== SENSOR_TYPE.MINGQU)?dataSum:dataAvg;
            return (type == SENSOR_TYPE.GUANDAO || type == SENSOR_TYPE.MINGQU) ? dataSum : data;
            //return d <0.0?0.0:d;
        }

        public static string GetSymbol(SENSOR_TYPE type)
        {
            switch (type)
            {
                case SENSOR_TYPE.GUANDAO:
                    return "㎥/h";
                case SENSOR_TYPE.MINGQU:
                    return "㎥/h";
                case SENSOR_TYPE.SHUIWEI:
                    return "m";
                case SENSOR_TYPE.WUWEI:
                    return "m";
                case SENSOR_TYPE.WENDU: 
                    return "℃";
                case SENSOR_TYPE.YALI:
                    return "MPa";
                case SENSOR_TYPE.DIANYA:
                    return "V";
            }
            return "";
        }

        public enum STATUS
        {
            R_EB = 0,
            R_90=1,
            R_SITE=2,
            R_SENSORNUM=3,
            R_SENSORTYPE=4,
            R_DATA1=5,
            R_DATA2=6,
            R_AA=7,
            R_UNKOWN=8,
        }
    }
}
