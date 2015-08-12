using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Nucleus
{
    public static class WebUtil
    {
        public static string GetASCIIString(byte[] data)
        {
#if WP8
            return string.Concat(data.Select(b => b <= 0x7f ? (char)b : '?'));
#else
            return ASCIIEncoding.ASCII.GetString(data);
#endif
        }

        public static void LogResponse(Exception arg1)
        {
            string ex = ReadResponse(arg1);
            Debug.WriteLine(ex);
        }

        public static string ReadResponse(Exception arg1)
        {
            if (arg1.InnerException is WebException)
            {
                WebException ex = (WebException)arg1.InnerException;
                string status = ex.Status.ToString();
                if (ex.Response == null)
                {
                    return status;
                }
                StreamReader str = new StreamReader(ex.Response.GetResponseStream());
                return status + Environment.NewLine + str.ReadToEnd();
            }
            return "";
        }
    }
}