using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text;



Console.BackgroundColor = ConsoleColor.Blue;

Console.ForegroundColor = ConsoleColor.Red;

Console.WriteLine("Api Consume İşlemine Hoşgeldiniz.");
Console.WriteLine();
Console.WriteLine("### Yapmak İstediğiniz İşlemi Seçiniz ###");
Console.WriteLine();
Console.WriteLine("1- Şehir listesini Getirin");
Console.WriteLine("1- Şehir,ülke ve sıcaklık değerlerini Getirin");
Console.WriteLine("3- Yeni Şehir Ekleme");
Console.WriteLine("4- Şehir Silme İşlemi");
Console.WriteLine("5- Şehir Güncelleme İşlemi");
Console.WriteLine("6- ID'ye Göre Şehir Getirme");
Console.WriteLine();



string number;

Console.Write("Tercihiniz:  ");

number = Console.ReadLine();
string url = "https://localhost:7214/api/Weathers";

if (number == "1")
{
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        string responsebody = await response.Content.ReadAsStringAsync();
        JArray jArray = JArray.Parse(responsebody);
        foreach (var item in jArray)
        {
            string cityname = item["cityName"].ToString();
            Console.WriteLine($"Şehir: {cityname}");
        }
    }
}

if (number == "2")
{
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        string responsebody = await response.Content.ReadAsStringAsync();
        JArray jarray = JArray.Parse(responsebody);
        foreach (var item in jarray)
        {
            string cityname = item["cityName"].ToString();
            string temp = item["temp"].ToString();
            string country = item["country"].ToString();
            Console.WriteLine(cityname + " - " + country + "-->" + temp + " Derece ");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++");
        }
    }

}

if (number == "3")
{
    Console.WriteLine("### Yeni Veri Girişi ###");
    Console.WriteLine();
    string cityName, country, detail;
    decimal temp;

    Console.Write("Şehir Adı: ");
    cityName = Console.ReadLine();

    Console.Write("Ülke Adı: ");
    country = Console.ReadLine();

    Console.Write("Hava Durumu Detayı: ");
    detail = Console.ReadLine();

    Console.Write("Sıcaklık: ");
    temp = decimal.Parse(Console.ReadLine());



    var newWeatherCity = new
    {
        CityName = cityName,
        Country = country,
        Detail = detail,
        Temp = temp
    };

    using (HttpClient client = new HttpClient())
    {
        string json = JsonConvert.SerializeObject(newWeatherCity);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
    }
}

if (number == "4")
{
    string url2 = "https://localhost:7214/api/Weathers?id=";

    Console.Write("Silmek İstediğiniz Şehir ID Giriniz: ");
    int id = int.Parse(Console.ReadLine());


    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.DeleteAsync(url2 + id);
        response.EnsureSuccessStatusCode();
    }

}

if (number == "5")
{
    Console.WriteLine("### Veri Güncelleme İşlemi ###");
    Console.WriteLine();
    string cityName, country, detail;
    decimal temp;
    int cityid;

    Console.Write("Şehir Adı: ");
    cityName = Console.ReadLine();

    Console.Write("Ülke Adı: ");
    country = Console.ReadLine();

    Console.Write("Hava Durumu Detayı: ");
    detail = Console.ReadLine();

    Console.Write("Sıcaklık: ");
    temp = decimal.Parse(Console.ReadLine());

    Console.Write("Şehir İd giriniz: ");
    cityid = int.Parse(Console.ReadLine());

    var updateCity = new
    {
        CityName = cityName,
        Country = country,
        Temp = temp,
        CityId = cityid,
        Detail = detail,
    };

    using (HttpClient client = new HttpClient())
    {
        string json = JsonConvert.SerializeObject(updateCity);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PutAsync(url, content);
        response.EnsureSuccessStatusCode();
    }

}

if (number == "6")
{
    Console.Write("Id değerini giriniz: ");
    int id = int.Parse(Console.ReadLine());

    string url2 = "https://localhost:7214/api/Weathers/GetByIdWeatherCity?id=";

    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage responseMessage = await client.GetAsync(url2 + id);
        responseMessage.EnsureSuccessStatusCode();
        string responsebody = await responseMessage.Content.ReadAsStringAsync();
        JObject weatherCityObject = JObject.Parse(responsebody);

        string cityname = weatherCityObject["cityName"].ToString();
        string country = weatherCityObject["country"].ToString();
        string detail = weatherCityObject["detail"].ToString();
        decimal temp = decimal.Parse(weatherCityObject["temp"].ToString());

        Console.WriteLine("Girilen id değerine  ait bilgiler");
        Console.WriteLine();
        Console.WriteLine("Şehir: " + cityname + "  Ülke: " + country + "  Detay: " + detail + "  Sıcaklık: " + temp);

       
    }

}
    Console.Read();