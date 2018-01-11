// Code taken from https://github.com/apixu/apixu-csharp
// Modified to be non blocking and handle historical data requests

using System;

namespace APIXULib
{
    public static class RequestBuilder
    {
        public  static string PrepareRequest( MethodType methodType,string key, GetBy getBy, string value, Days ofDays)
        {
            return string.Concat(methodType.GetParameters(),"?key=",key,"&", getBy.PrepareQueryParameter(value),"&", ofDays.PrepareDays());
        }

        public static string PrepareRequest(MethodType methodType, string key, GetBy getBy, string value)
        {
            return string.Concat(methodType.GetParameters(), "?key=", key, "&", getBy.PrepareQueryParameter(value));
        }

        public static string PrepareRequest(MethodType methodType, string key, GetBy getBy, string value, DateTime date)
        {
            return string.Concat(methodType.GetParameters(), "?key=", key, "&", getBy.PrepareQueryParameter(value), "&", ReqestFor.Date(date));
        }

        public static string PrepareRequestByLatLong(MethodType methodType, string key, string latitude, string longitude, Days ofDays)
        {
            return string.Concat(methodType.GetParameters(), "?key=", key, "&", ReqestFor.LatLong(latitude,longitude) , "&", ofDays.PrepareDays());
        }

        public static string PrepareRequestByLatLong(MethodType methodType, string key, string latitude, string longitude)
        {
            return string.Concat(methodType.GetParameters(), "?key=", key, "&", ReqestFor.LatLong(latitude, longitude));
        }

        public static string PrepareRequestByLatLong(MethodType methodType, string key, string latitude, string longitude, DateTime date)
        {
            return string.Concat(methodType.GetParameters(), "?key=", key, "&", ReqestFor.LatLong(latitude, longitude), "&", ReqestFor.Date(date));
        }

        public static string PrepareRequestByAutoIP(MethodType methodType, string key, Days ofDays)
        {
            return string.Concat(methodType.GetParameters(), "?key=", key, "&", ReqestFor.AutoIP(), "&", ofDays.PrepareDays());
        }

        public static string PrepareRequestByAutoIP(MethodType methodType, string key)
        {
            return string.Concat(methodType.GetParameters(), "?key=", key, "&", ReqestFor.AutoIP());
        }

        public static string PrepareRequestByAutoIP(MethodType methodType, string key, DateTime date)
        {
            return string.Concat(methodType.GetParameters(), "?key=", key, "&", ReqestFor.AutoIP(), "&", ReqestFor.Date(date));
        }
    }
}
