using Newtonsoft.Json;
using RealEstateAgencyMVC.Models;
using RealEstateWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealEstateWebUI.Controllers
{
    public class RealEstateClassifiedsController : Controller
    {
        // GET: RealEstateClassifieds
        private static string apiUrl = "https://localhost:44312/";
        public async Task<ActionResult> Index()
        {
            ListResponseModel<RealEstateClassifiedDetailDtoModel> realEstateClassifieds = new ListResponseModel<RealEstateClassifiedDetailDtoModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
                HttpResponseMessage response = await client.GetAsync("api/realestateclassifieds/getallrealestateclassifieddetailsdtobyuserid?userId=" + Convert.ToInt32(Session["userId"]));
                if (response.IsSuccessStatusCode)
                {
                    var realEstateClassifiedsResponse = response.Content.ReadAsStringAsync().Result;
                    realEstateClassifieds = JsonConvert.DeserializeObject<ListResponseModel<RealEstateClassifiedDetailDtoModel>>(realEstateClassifiedsResponse);
                }

                return View(realEstateClassifieds.Data);
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                SingleResponseModel<RealEstateClassifiedModel> realEstateClassifiedModel = new SingleResponseModel<RealEstateClassifiedModel>();
                httpClient.BaseAddress = new Uri(apiUrl);
                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync("api/realestateclassifieds/getrealestateclassifiedbyid?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    realEstateClassifiedModel = JsonConvert.DeserializeObject<SingleResponseModel<RealEstateClassifiedModel>>(response.Content.ReadAsStringAsync().Result);
                }
                
                var content = new StringContent(JsonConvert.SerializeObject(realEstateClassifiedModel.Data), Encoding.UTF8, "application/json");

                HttpResponseMessage deleteResponse = await httpClient.PostAsync("api/realestateclassifieds/delete", content);
                var responseObject = JsonConvert.DeserializeObject<ResponseModel>(deleteResponse.Content.ReadAsStringAsync().Result);
                if (responseObject.Success == true)
                {
                    return RedirectToAction("Index", "RealEstateClassifieds");
                }
                return RedirectToAction("Index", "RealEstateClassifieds");
            }
        }
        public async Task<ViewResult> Update(int id)
        {

            using (var httpClient = new HttpClient())
            {
                SingleResponseModel<RealEstateClassifiedModel> realEstateClassifiedModel = new SingleResponseModel<RealEstateClassifiedModel>();
                httpClient.BaseAddress = new Uri(apiUrl);
                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync("api/realestateclassifieds/getrealestateclassifiedbyid?id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    realEstateClassifiedModel = JsonConvert.DeserializeObject<SingleResponseModel<RealEstateClassifiedModel>>(response.Content.ReadAsStringAsync().Result);
                    return View("UpdateRealEstateClassified", realEstateClassifiedModel.Data);
                }
                return View("UpdateRealEstateClassified", realEstateClassifiedModel.Data);

            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(RealEstateClassifiedModel realEstateClassifiedDetailDtoModel)
        {
            using (var httpClient = new HttpClient())
            {
                SingleResponseModel<RealEstateClassifiedModel> realEstateClassifiedModel = new SingleResponseModel<RealEstateClassifiedModel>();
                httpClient.BaseAddress = new Uri(apiUrl);
                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync("api/realestateclassifieds/getrealestateclassifiedbyid?id=" + realEstateClassifiedDetailDtoModel.Id);
                if (response.IsSuccessStatusCode)
                {
                    realEstateClassifiedModel = JsonConvert.DeserializeObject<SingleResponseModel<RealEstateClassifiedModel>>(response.Content.ReadAsStringAsync().Result);
                    realEstateClassifiedModel.Data.BuildingAge = realEstateClassifiedDetailDtoModel.BuildingAge;
                    realEstateClassifiedModel.Data.Floor = realEstateClassifiedDetailDtoModel.Floor;
                    realEstateClassifiedModel.Data.FullAddress = realEstateClassifiedDetailDtoModel.FullAddress;
                    realEstateClassifiedModel.Data.Id = realEstateClassifiedDetailDtoModel.Id;
                    realEstateClassifiedModel.Data.NumberOfRoomId = realEstateClassifiedDetailDtoModel.NumberOfRoomId;
                    realEstateClassifiedModel.Data.RealEstateClassifiedTitle = realEstateClassifiedDetailDtoModel.RealEstateClassifiedTitle;
                    realEstateClassifiedModel.Data.RealEstatePrice = Convert.ToDecimal(realEstateClassifiedDetailDtoModel.RealEstatePrice);
                    realEstateClassifiedModel.Data.SquareMeters =Convert.ToDecimal( realEstateClassifiedDetailDtoModel.SquareMeters);
                    realEstateClassifiedModel.Data.Status = realEstateClassifiedDetailDtoModel.Status;
                }

                

                var content = new StringContent(JsonConvert.SerializeObject(realEstateClassifiedModel.Data), Encoding.UTF8, "application/json");

                HttpResponseMessage deleteResponse = await httpClient.PostAsync("api/realestateclassifieds/update", content);
                var responseObject = JsonConvert.DeserializeObject<ResponseModel>(deleteResponse.Content.ReadAsStringAsync().Result);
                if (responseObject.Success == true)
                {
                    return RedirectToAction("Index", "RealEstateClassifieds");
                }
                return RedirectToAction("Index", "RealEstateClassifieds");
            }
        }
        [HttpPost]

        public async Task<ActionResult> AddRealEstateClassified(RealEstateClassifiedModel realEstateClassifiedModel)
        {
            using (var httpClient = new HttpClient())
            {
                realEstateClassifiedModel.Date = DateTime.Now;
                realEstateClassifiedModel.Status = true;
                realEstateClassifiedModel.UserId=Convert.ToInt32(realEstateClassifiedModel.UserId);
                httpClient.BaseAddress = new Uri(apiUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
                var content = new StringContent(JsonConvert.SerializeObject(realEstateClassifiedModel), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("api/realestateclassifieds/add", content);
                var responseObject = JsonConvert.DeserializeObject<SingleResponseModel<RealEstateClassifiedModel>>(response.Content.ReadAsStringAsync().Result);
                if (responseObject.Success)
                {
                    
                    return RedirectToAction("Index", "RealEstateClassifieds");
                }
                else
                {
                    
                    return RedirectToAction("Index", "RealEstateClassifieds");
                }
            }
        }
        [HttpGet]
        public async Task<ViewResult> AddRealEstateClassified()
        {
            MultipleViewModel multipleViewModel = new MultipleViewModel();
            ListResponseModel<CityViewModel> cities = new ListResponseModel<CityViewModel>();
            ListResponseModel<DistrictViewModel> districts = new ListResponseModel<DistrictViewModel>();
            ListResponseModel<NeighborhoodViewModel> neighborhoods = new ListResponseModel<NeighborhoodViewModel>();
            ListResponseModel<NumberOfRoomViewModel> numberOfRooms = new ListResponseModel<NumberOfRoomViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage cityResponse = await client.GetAsync("api/cities/getall");
                if (cityResponse.IsSuccessStatusCode)
                {
                    var cityResponseData = cityResponse.Content.ReadAsStringAsync().Result;
                    cities = JsonConvert.DeserializeObject<ListResponseModel<CityViewModel>>(cityResponseData);
                    multipleViewModel.Cities = cities.Data;
                }
                
                HttpResponseMessage numberOfRoomResponse = await client.GetAsync("api/numberofrooms/getall");
                if (cityResponse.IsSuccessStatusCode)
                {
                    var numberOfRoomResponeData = numberOfRoomResponse.Content.ReadAsStringAsync().Result;
                    numberOfRooms = JsonConvert.DeserializeObject<ListResponseModel<NumberOfRoomViewModel>>(numberOfRoomResponeData);
                    multipleViewModel.NumberOfRooms = numberOfRooms.Data;
                }

                return View(multipleViewModel);

            }

        }
        public async Task<JsonResult> GetAllDistrictByCityId(int cityId)
        {
            ListResponseModel<DistrictViewModel> districts = new ListResponseModel<DistrictViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage districtResponse = await client.GetAsync("api/districts/getalldistrictsbycityid?cityId="+cityId);
                if (districtResponse.IsSuccessStatusCode)
                {
                    var districtResponseData = districtResponse.Content.ReadAsStringAsync().Result;
                    districts = JsonConvert.DeserializeObject<ListResponseModel<DistrictViewModel>>(districtResponseData);
                }
            }
            return Json(districts.Data, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetAllNeighborhoodsByDistrictId(int districtId)
        {
            ListResponseModel<NeighborhoodViewModel> neighborhoods = new ListResponseModel<NeighborhoodViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage neighborhoodResponse = await client.GetAsync("api/neighborhoods/getallneighborhoodsbydistrictid?districtId=" + districtId);
                if (neighborhoodResponse.IsSuccessStatusCode)
                {
                    var neighborhoodResponseData = neighborhoodResponse.Content.ReadAsStringAsync().Result;
                    neighborhoods = JsonConvert.DeserializeObject<ListResponseModel<NeighborhoodViewModel>>(neighborhoodResponseData);
                }
            }
            return Json(neighborhoods.Data, JsonRequestBehavior.AllowGet);
        }




    }
}