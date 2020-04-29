using HtmlAgilityPack;
using MiriNews.Web.Areas.Admin.Models;
using System;
using System.Net;
using System.Text;

namespace MiriNews.Web.Areas.Admin.Extensions
{
    public class Auto
    {
        public AutoPostViewModel AutoPost(string url)
        {
            var model = new AutoPostViewModel();

            var uri = new Uri(url);

            var client = new WebClient();
            client.Encoding = Encoding.UTF8;

            var html = client.DownloadString(uri);

            //HtmlWeb web = new HtmlWeb();

            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(html);

            model.Title = document.DocumentNode.SelectSingleNode("/html/body/div[1]/div[3]/div[1]/div/article/div[7]/h1").ToString();

            model.Description = document.DocumentNode.SelectNodes("/html/body/div[1]/div[3]/div[1]/div/article/div[7]")[0].InnerText;


            return model;
        }
    }
}
