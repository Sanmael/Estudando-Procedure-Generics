using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceDebbug.Keys
{
    public static class GetKeys
    {
        public static int GetIntervalTime()
        {
            try
            {
                int time = int.Parse(ConfigurationManager.AppSettings["ResetTime"]);

                return time;
            }
            catch
            {
                return 6000;
            }
        }
        public static bool GetReset()
        {
            try
            {
                bool time = bool.Parse(ConfigurationManager.AppSettings["ResetAnyTime"]);

                return time;
            }
            catch
            {
                return false;
            }
        }
        public static string GetConnections()
        {
            try
            {
                string connections = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                return connections;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
