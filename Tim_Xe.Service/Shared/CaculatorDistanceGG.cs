using Newtonsoft.Json.Linq;
using System;
using static Tim_Xe.Data.Enum.DistanceUnit;

namespace Tim_Xe.Service.Shared
{
    public class CaculatorDistanceGG
    {
        public int getDistance(string origin, string destination)
        {
            System.Threading.Thread.Sleep(1000);
            int distance = 0;
            string key = "YOUR KEY";

            string url = "https://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&key=" + key;
            url = url.Replace(" ", "+");
            string content = fileGetContents(url);
            JObject o = JObject.Parse(content);
            try
            {
                distance = (int)o.SelectToken("routes[0].legs[0].distance.value");
                return distance;
            }
            catch
            {
                return distance;
            }
        }

        protected string fileGetContents(string fileName)
        {
            string sContents = string.Empty;
            string me = string.Empty;
            try
            {
                if (fileName.ToLower().IndexOf("https:") > -1)
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] response = wc.DownloadData(fileName);
                    sContents = System.Text.Encoding.ASCII.GetString(response);

                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { sContents = "unable to connect to server "; }
            return sContents;
        }
        public double HaversineDistance(string ll1, string ll2, DistanceUnits unit)
        {
            string[] ll1str = ll1.Split(',');
            string[] ll2str = ll2.Split(',');
            double lat1 = double.Parse(ll1str[0], System.Globalization.CultureInfo.InvariantCulture);
            double lng1 = double.Parse(ll1str[1], System.Globalization.CultureInfo.InvariantCulture);
            double lat2 = double.Parse(ll2str[0], System.Globalization.CultureInfo.InvariantCulture);
            double lng2 = double.Parse(ll2str[1], System.Globalization.CultureInfo.InvariantCulture);
            double R = (unit == DistanceUnits.Miles) ? 3960 : 6371;
            var lat = (lat2 - lat1).ToRadians();
            var lng = (lng2 - lng1).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(lat1.ToRadians())
                          * Math.Cos(lat2.ToRadians()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2;
        }
    }
}
