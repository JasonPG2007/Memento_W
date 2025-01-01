using Memento_W.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Memento_W.Controllers
{
    public class HomeController : Controller
    {
        #region Variable
        private HttpClient _httpClient;
        private string APIUrl = "";
        #endregion

        #region Constructor
        public HomeController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            APIUrl = "http://localhost:8080/profile/";
        }
        #endregion

        #region GET: HomeController
        // GET: HomeController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(APIUrl);
            string data = await httpResponseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<ProfileDTO> profiles = JsonSerializer.Deserialize<List<ProfileDTO>>(data, options);
            if (profiles != null && profiles.Count == 0)
            {
                ViewBag.Profiles = "There are no profiles yet";
            }
            return View(profiles);
        }
        #endregion

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        #region GET: HomeController/Create
        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        #region POST: HomeController/Create
        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProfileDTO profile)
        {
            try
            {
                // Variable
                Random random = new Random();
                profile.ProfileId = random.Next();
                profile.CreatedAt = DateTimeOffset.Now;

                // Get API
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                };
                var data = JsonSerializer.Serialize(profile, options);
                var type = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await _httpClient.PostAsync(APIUrl + "create", type);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);
                ViewBag.Error = responseJson["msg"];
                return View();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
