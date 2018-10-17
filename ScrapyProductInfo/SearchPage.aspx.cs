using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ScrapyProductInfo.Model.Darunfa;
using ScrapyProductInfo.Model;

namespace ScrapyProductInfo
{
    public partial class SearchPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKeyWord.Text.Trim()))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "info", " <script>alert('请输入搜索关键词！'); </script>");
                return;
            }

            DealDataDaRunFaInfo();
            //var body = new
            //{
            //    key = txtKeyWord.Text.Trim(),
            //    sortType = "1",
            //    storeId = "11756973",
            //    page = 1,
            //    pageSize = 30,
            //    cls = "2",
            //    ref1 = "active",
            //    ctp = "search"
            //};

            //string strBody = Newtonsoft.Json.JsonConvert.SerializeObject(body).Replace("ref1", "ref");
            //string url = mUrl + strBody;
            //string content = Utility.GetResponse(url);
            //if (string.IsNullOrEmpty(content))
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "info", " <script>alert('访问出错！'); </script>");
            //    return;

            //}


            //var searchResult = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonResult<Searchresult>>(content);
            //if (searchResult.code == "0")
            //{
            //    List<ProductInfo> lst = searchResult.result.searchResultVOList;
            //    if (lst != null && lst.Count > 0)
            //    {
            //        rpProduct.DataSource = lst;
            //        rpProduct.DataBind();
            //    }
            //}

            string content = GetDataDaRunFaInfo();
            if (string.IsNullOrEmpty(content))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "info", " <script>alert('访问出错！'); </script>");
                return;
            }
        }

        /// <summary>
        /// 搜索沃尔玛信息
        /// </summary>
        /// <returns></returns>
        private string GetDataWoerma()
        {
            string content = "";
            string url = "https://daojia.jd.com/client?city_id=1315&lat=24.48372&lng=118.18095&lat_pos=24.48372&lng_pos=118.18095&deviceId=a6cf29be-4b57-40ba-959c-02df3b1c9c97&deviceToken=a6cf29be-4b57-40ba-959c-02df3b1c9c97&appVersion=5.0.0&platCode=H5&appName=paidaojia&deviceModel=appmodel&channel=wx_xcx&functionId=productsearch/search&body=";
            try
            {
                var body = new
                {
                    key = txtKeyWord.Text.Trim(),
                    sortType = "1",
                    storeId = "11671342",
                    page = 1,
                    pageSize = 30,
                    cls = "2",
                    ref1 = "active",
                    ctp = "search"
                };

                string strBody = Newtonsoft.Json.JsonConvert.SerializeObject(body).Replace("ref1", "ref");
                url = url + strBody;
                content = Utility.GetResponse(url);
            }
            catch (Exception)
            {

            }
            return content;
        }

        /// <summary>
        /// 搜索大润发信息
        /// </summary>
        /// <returns></returns>
        private string GetDataDaRunFaInfo()
        {
            string url = "https://yx.feiniu.com/search-yxapp/merchandise/searchByKey/t108";
            string content = "";
            try
            {
                var data = new
                {
                    apiVersion = "t108",
                    areaCode = "CS000016",
                    channel = "online",
                    clientid = "a7ea53059fc868e2e3e2dd7c04027035",
                    device_id = "CBVEzQspqInwBk3n8Wgvu6RJXAaphQzKVhXR",
                    time = Utility.ConvertDateTimeToInt(DateTime.Now),
                    rerule = "4",
                    token = "6660bff13de74adde85aa766c1558941",
                    view_size = "720x1184",
                    osType = "4",
                    body = new
                    {
                        keywords = txtKeyWord.Text.Trim(),
                        one_page_size = 10,
                        only_camp = 0,
                        page_index = 1,
                        store_id = 5053,
                        sort_type = 1,
                        sort_order = 0
                    }
                };

                string postData = "data=" + Newtonsoft.Json.JsonConvert.SerializeObject(data) + "&h5=yx_touch&paramsMD5=DJf0JruF6YSlDCOgakqHOxfp762mOVk/4FCdNCG+8iA=";
                content = Utility.PostWebRequest(url, postData, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {

            }
            return content;
        }

        public static string GetDaRunFaDetails(string goodsNo)
        {
            string content = "";
            string url = "https://yx.feiniu.com/www-yxapp/goods/detail/t108";
            try
            {
                var data = new
                {
                    apiVersion = "t108",
                    areaCode = "CS000016",
                    channel = "online",
                    clientid = "a7ea53059fc868e2e3e2dd7c04027035",
                    device_id = "CBVEzQspqInwBk3n8Wgvu6RJXAaphQzKVhXR",
                    time = Utility.ConvertDateTimeToInt(DateTime.Now),
                    rerule = "4",
                    token = "6660bff13de74adde85aa766c1558941",
                    view_size = "720x1184",
                    osType = "4",
                    body = new
                    {
                        goodsNo = goodsNo,
                        storeCode = "5053"
                    }
                };

                string postData = "data=" + Newtonsoft.Json.JsonConvert.SerializeObject(data) + "&h5=yx_touch&paramsMD5=DJf0JruF6YSlDCOgakqHOxfp762mOVk/4FCdNCG+8iA=";
                content = Utility.PostWebRequest(url, postData, System.Text.Encoding.UTF8);
            }
            catch (Exception)
            {

            }
            return content;
        }

        private void DealDataDaRunFaInfo()
        {
            try
            {
                string content = GetDataDaRunFaInfo();
                if (string.IsNullOrEmpty(content))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "info", " <script>alert('未搜索到！'); </script>");
                    return;
                }
                //string details = GetDaRunFaDetails("P0818050000103781");
                //var detailResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Darunfa_SearchResult<Darunfa_ProductInfo_Detail_Body>>(details);



                var searchResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Darunfa_SearchResult<ScrapyProductInfo.Model.Darunfa.Darunfa_SearchResultBody>>(content);
                if (searchResult.errorCode == 0)
                {
                    List<Darunfa_ProductInfo> lstProduct = searchResult.body.MerchandiseList;
                    List<ShowProductInfo> lstShowProduct = new List<ShowProductInfo>();
                    foreach (var item in lstProduct)
                    {
                        ShowProductInfo showInfo = new ShowProductInfo();
                        showInfo.name = item.sm_name;
                        showInfo.imgUrl = item.sm_pic;
                        showInfo.goodsNo = item.sku_id;
                        lstShowProduct.Add(showInfo);
                    }

                    if (lstShowProduct.Count > 0)
                    {
                        rpProduct.DataSource = lstShowProduct;
                        rpProduct.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static string DownloadDetail(string folder, string goodsNo)
        {
            string iResult = "0";
            try
            {
                if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(goodsNo))
                {
                    return "-1";
                }
                string strDetail = GetDaRunFaDetails(goodsNo);
                string details = GetDaRunFaDetails(goodsNo);
                var detailResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Darunfa_SearchResult<Darunfa_ProductInfo_Detail_Body>>(details);
                if (detailResult.errorCode == 0)
                {
                    var lstImg = detailResult.body.productDetail.sm_pic_list;
                    foreach (string item in lstImg)
                    {
                        Utility.DownloadFile(folder, item);
                    }

                }
                iResult = "1";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return iResult;
        }


    }
}