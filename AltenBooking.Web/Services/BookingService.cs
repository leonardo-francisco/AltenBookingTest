using AltenBooking.Web.Models.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AltenBooking.Web.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _apiClient;
        IConfiguration _configuration;

        public BookingService(HttpClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        public async Task Create(BookingDTO bookingDTo)
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:BookingUrl"));
            var rentContent = new StringContent(JsonConvert.SerializeObject(bookingDTo),
                                      System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, rentContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:BookingUrl"));
            var response = _apiClient.DeleteAsync(string.Format(uri + "/{0}", id));
            var result = response.Result;
            result.EnsureSuccessStatusCode();
        }

        public async Task<List<BookingDTO>> GetAll()
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:BookingUrl"));
            var response = _apiClient.GetAsync(uri);
            var result = response.Result;
            var resultado = await result.Content.ReadAsAsync<List<BookingDTO>>();
            return resultado;
        }

        public async Task<BookingDTO> GetById(int id)
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:BookingUrl"));
            var response = _apiClient.GetAsync(string.Format(uri + "/{0}", id));
            var result = response.Result;
            var resultado = await result.Content.ReadAsAsync<BookingDTO>();
            return resultado;
        }

        public async Task<bool> GetAvailable(string dateIn, string dateOut)
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:BookingUrl"));
            var response = _apiClient.GetAsync(string.Format(uri + "/{0}/{1}", dateIn,dateOut));
            var result = response.Result;
            var resultado = await result.Content.ReadAsAsync<bool>();
            return resultado;
        }

        public async Task Update(BookingDTO bookingDTo)
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:BookingUrl"));
            var rentContent = new StringContent(JsonConvert.SerializeObject(bookingDTo),
                                      System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PutAsync(uri, rentContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<string>> BusyDates()
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:BookingUrl"));
            var response = _apiClient.GetAsync(string.Format(uri + "/BusyDates"));
            var result = response.Result;
            var resultado =  await result.Content.ReadAsAsync<List<string>>();
            return resultado;
        }

        public async Task<List<BookingDTO>> ListsBkngUser(string userId)
        {
            var uri = Convert.ToString(_configuration.GetValue<string>("API:BookingUrl"));
            var response = _apiClient.GetAsync(string.Format(uri + "/BookingByUser/{0}", userId ));
            var result = response.Result;
            var resultado = await result.Content.ReadAsAsync<List<BookingDTO>>();
            return resultado;
        }
    }
}
