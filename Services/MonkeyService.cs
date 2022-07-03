using monkeyfinder.Models;
using monkeyfinder.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace monkeyfinder.Services
{
    public class MonkeyService
    {
        HttpClient _httpClient;

        public MonkeyService()
        {
            _httpClient = new ();
        }

        List<Monkey> monkeyList = new ();
        public async Task<List<Monkey>> GetMonkeys()
        {
            if (monkeyList?.Count > 0)
                return monkeyList;

            var response = await _httpClient.GetAsync(URLs.MonkeyList);

            if (response.IsSuccessStatusCode)
            {
                monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
            }

            return monkeyList;
        }
    }
}
