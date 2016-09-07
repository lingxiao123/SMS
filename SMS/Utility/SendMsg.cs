using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Utility
{
    public class SendMsg
    {
        public static bool SendMsgs(string cusphone,string[] arr)
        {
            bool flag = false;
            CCPRestSDK api = new CCPRestSDK();
            bool isInit = api.init("sandboxapp.cloopen.com", "8883");
            api.setAccount("8aaf070856b669b10156ba6e94bb05ab", "78880f64b08a4066bba44b603dc81ad4");
            api.setAppId("8aaf070856b669b10156ba6e951f05b2");
            Dictionary<string, object> retData = null;
            if (isInit)
            {
                retData = api.SendTemplateSMS(cusphone, "111500", arr);
            }
            foreach (var item in retData)
            {
                if (item.Key == "statusCode")
                {
                    if (item.Value.ToString() == "000000")
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }
    }
}