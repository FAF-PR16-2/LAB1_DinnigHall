using System;
using System.IO;
using System.Net;
using System.Text.Json;




namespace DinnerHall
{
    public static class RequestsSender
    {
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