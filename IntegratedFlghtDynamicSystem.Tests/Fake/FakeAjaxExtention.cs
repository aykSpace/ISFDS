﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IntegratedFlghtDynamicSystem.Tests.Fake
{
    public static class FakeAjaxExtention
    {
        public static bool IsAjaxRequest(this HttpRequestBase request)
        {
            return true;
        }
    }
}