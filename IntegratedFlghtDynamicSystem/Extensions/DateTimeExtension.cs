using System;

namespace IntegratedFlghtDynamicSystem.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Перевод из Hour Minute Second.. в Second
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Second in hours, minutes, seconds, milliseconds</returns>
        public static double GetTimeSecond(this DateTime t )
        {
            return  t.Hour*3600 + t.Minute*60 + t.Second + t.Millisecond/1000;
        }
    }
}
