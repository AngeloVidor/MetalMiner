using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;


namespace scrap;

public class CollectData
{

    public async Task<List<BandSearchResponse?>> SearchForDataAsync(string name, string? genre)
    {
        Console.WriteLine("Searching for data...");

        string baseUrl = "https://www.metal-archives.com/search/ajax-advanced/searching/bands/";

        string nameParam = Uri.EscapeDataString(name);
        string genreParam = Uri.EscapeDataString(genre);
        string url = $"{baseUrl}?bandName={nameParam}&genre={genreParam}&country=&yearCreationFrom=&yearCreationTo=&bandNotes=&status=&themes=&location=&bandLabelName=&sEcho=1&iColumns=3&sColumns=&iDisplayStart=0&iDisplayLength=200&mDataProp_0=0&mDataProp_1=1&mDataProp_2=2";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

            try
            {
                var response = await client.GetAsync(url);
                Console.WriteLine($"StatusCode: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var jsonData = JsonConvert.DeserializeObject<MetalArchivesResponse>(jsonResult);

                        if (jsonData != null && jsonData.AaData.Count > 0)
                        {
                            Console.WriteLine("Bands found:");

                            List<BandSearchResponse> bandSearchResponses = new List<BandSearchResponse>();

                            foreach (var bandData in jsonData.AaData)
                            {
                                HtmlDocument htmlDoc = new HtmlDocument();
                                htmlDoc.LoadHtml(bandData[0]);

                                string bandNameResult = htmlDoc.DocumentNode.InnerText.Trim();
                                string genreResult = bandData[1];
                                string countryResult = bandData[2];

                                var bandSearchResponse = new BandSearchResponse
                                {
                                    BandName = bandNameResult,
                                    Genre = genreResult,
                                    Country = countryResult
                                };
                                bandSearchResponses.Add(bandSearchResponse);
                                Console.WriteLine($"Band: {bandNameResult}, Genre: {genreResult}, Country: {countryResult}");
                            }
                            return bandSearchResponses;
                        }
                        else
                        {
                            Console.WriteLine("No bands found.");
                        }
                    }
                    catch (JsonReaderException)
                    {
                        Console.WriteLine("Error: The response is not a valid JSON.");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: Unable to perform search. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
        }
        return null;
    }

    public async Task GetBandDiscographyAsync(BandSearchResponse searchResponse)
    {
        throw new NotImplementedException();
    }
}

