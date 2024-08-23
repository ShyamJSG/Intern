//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using WebApiMVC.Models;


//namespace WebApiMVC.Controllers
//{
//    public class CrudController : Controller
//    {
//        private readonly HttpClient _httpClient;

//        public CrudController()
//        {
//            _httpClient = new HttpClient();
//            _httpClient.BaseAddress = new Uri("http://localhost:7194/api/Crud");
//        }

//        public async Task<IActionResult> Index()
//        {
//            List<Crud> cruds = new List<Crud>();
//            HttpResponseMessage response = await _httpClient.GetAsync("GetAllEmployees");
//            if (response.IsSuccessStatusCode)
//            {
//                var data = await response.Content.ReadAsStringAsync();
//                var result = JsonConvert.DeserializeObject<Response>(data);
//                cruds = result.cruds;
//            }
//            return View(cruds);
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(Crud crud)
//        {
//            if (ModelState.IsValid)
//            {
//                var content = new StringContent(JsonConvert.SerializeObject(crud), Encoding.UTF8, "application/json");
//                HttpResponseMessage response = await _httpClient.PostAsync("AddEmployee", content);
//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction("Index");
//                }
//            }
//            return View(crud);
//        }

//        public async Task<IActionResult> Edit(int id)
//        {
//            Crud crud = new Crud();
//            HttpResponseMessage response = await _httpClient.GetAsync($"GetEmployeeById/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var data = await response.Content.ReadAsStringAsync();
//                var result = JsonConvert.DeserializeObject<Response>(data);
//                crud = result.crud;
//            }
//            return View(crud);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(Crud crud)
//        {
//            if (ModelState.IsValid)
//            {
//                var content = new StringContent(JsonConvert.SerializeObject(crud), Encoding.UTF8, "application/json");
//                HttpResponseMessage response = await _httpClient.PostAsync("UpdateEmployee", content);
//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction("Index");
//                }
//            }
//            return View(crud);
//        }

//        public async Task<IActionResult> Delete(int id)
//        {
//            HttpResponseMessage response = await _httpClient.PostAsync($"DeleteEmployee/{id}", null);
//            if (response.IsSuccessStatusCode)
//            {
//                return RedirectToAction("Index");
//            }
//            return View();
//        }
//    }
//}
