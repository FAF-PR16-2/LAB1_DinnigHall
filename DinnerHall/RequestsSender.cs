using System;
using System.IO;
using System.Net;
using System.Text.Json;




namespace DinnerHall
{
    public static class RequestsSender
    {
        // public static void SendOrderRequest(OrderData orderData)
        // {
        //     var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:81/order");
        //     httpWebRequest.ContentType = "application/json";
        //     httpWebRequest.Method = "POST";
        //
        //     using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //     {
        //         string json = JsonSerializer.Serialize(orderData);
        //
        //         streamWriter.Write(json);
        //     }
        //
        //     var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //     using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //     {
        //         var result = streamReader.ReadToEnd();
        //     }
        // }
        
        public static void SendOrderRequest(OrderData orderData)
        {
            using (var client = new WebClient())
            {
                var dataString = JsonSerializer.Serialize(orderData);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.UploadString(new Uri("http://host.docker.internal:81/order"), "POST", dataString);
            }
        }
    }
}