﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;

using EliteData.DataRepository.Models;

namespace EliteData.DataRepository
{
    public class EliteRepository
    {
        public async Task<IEnumerable<SolarSystem>> GetSystemsAsync()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://eddb.io/archive/v4/systems.json");
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<SolarSystem> systemList = JsonConvert.DeserializeObject<IEnumerable<SolarSystem>>(content);

            using (var db = new EliteDataContext())
            {
                db.Systems.AddRange(systemList);
                db.SaveChanges();
            }

            return systemList;
        }

        public async Task<IEnumerable<Station>> GetStationsAsync()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://eddb.io/archive/v4/stations.json");
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<Station> stationList = JsonConvert.DeserializeObject<IEnumerable<Station>>(content);

            return stationList;
        }

        public async Task<IEnumerable<Commodity>> GetCommoditiesAsync()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://eddb.io/archive/v4/commodities.json");
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<Commodity> commodityList = JsonConvert.DeserializeObject<IEnumerable<Commodity>>(content);

            return commodityList;
        }
    }
}
